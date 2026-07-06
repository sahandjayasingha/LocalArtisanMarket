

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'LocalArtisanMarketDB')
BEGIN
    CREATE DATABASE LocalArtisanMarketDB;
END
GO

USE LocalArtisanMarketDB;
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Users] (
        [UserID] INT IDENTITY(1,1) PRIMARY KEY,
        [FullName] NVARCHAR(100) NOT NULL,
        [Email] NVARCHAR(100) UNIQUE NOT NULL,
        [PasswordHash] NVARCHAR(255) NOT NULL,
        [Role] NVARCHAR(20) NOT NULL DEFAULT 'Customer',
        [CreatedAt] DATETIME DEFAULT GETDATE()
    );
END
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Products] (
        ProductID INT IDENTITY(1,1) PRIMARY KEY,
        ProductName VARCHAR(100) NOT NULL,
        Price DECIMAL(10,2) NOT NULL,
        Description NVARCHAR(MAX) NULL,     
        StockQuantity INT NOT NULL,         
        ArtisanID INT NOT NULL, 
        CONSTRAINT FK_Products_Users FOREIGN KEY (ArtisanID) REFERENCES Users(UserID) 
        ON UPDATE CASCADE ON DELETE NO ACTION
    );
END
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MaterialTracking]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[MaterialTracking] (
        LogID INT IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
        ProductID INT NOT NULL,
        MoistureLevel DECIMAL(5,2) NOT NULL,
        ProcessingStage NVARCHAR(100) NOT NULL,
        SupplierDetails NVARCHAR(MAX) NULL,
        CONSTRAINT FK_MaterialTracking_Products FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
        ON UPDATE CASCADE ON DELETE CASCADE
    );
END
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Orders] (
        OrderID INT IDENTITY(1,1) PRIMARY KEY,
        OrderToken NVARCHAR(50) NOT NULL,
        OrderDate DATETIME DEFAULT GETDATE(),
        TotalAmount DECIMAL(18,2) NOT NULL
    );
END
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderItems]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[OrderItems] (
        OrderItemID INT IDENTITY(1,1) PRIMARY KEY,
        OrderID INT FOREIGN KEY REFERENCES Orders(OrderID) ON DELETE CASCADE,
        ProductID INT FOREIGN KEY REFERENCES Products(ProductID),
        Quantity INT NOT NULL,
        PriceAtPurchase DECIMAL(18,2) NOT NULL
    );
END
GO