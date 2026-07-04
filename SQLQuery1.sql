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
        [ProductID] INT IDENTITY(1,1) PRIMARY KEY,
        [ProductName] NVARCHAR(150) NOT NULL,
        [Description] NVARCHAR(MAX),
        [Price] DECIMAL(18,2) NOT NULL,
        [StockQuantity] INT NOT NULL DEFAULT 0,
        [ArtisanID] INT,
        [CreatedAt] DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (ArtisanID) REFERENCES Users(UserID)
    );
END
GO