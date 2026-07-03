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
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
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
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
BEGIN
    CREATE TABLE Products (
        ProductID INT IDENTITY(1,1) PRIMARY KEY,
        ProductName VARCHAR(100) NOT NULL,
        Price DECIMAL(10,2) NOT NULL,
        Quantity INT NOT NULL
    );
END
GO
-- ============================================================================
-- DATABASE ENGINEERING PHASE 2: SCHEMA EXPANSION & INTEGRITY CONTROLS
-- ============================================================================

-- 1. Adding Foreign Key Constraint to Products Table (ArtisanID Context)
ALTER TABLE Products
ADD CONSTRAINT FK_Products_Users 
FOREIGN KEY (ArtisanID) REFERENCES Users(UserID)
ON UPDATE CASCADE 
ON DELETE NO ACTION;

-- 2. Architecting the MaterialTracking Table for Telemetry & Lifecycle Metrics
CREATE TABLE MaterialTracking (
    LogID INT IDENTITY(1,1) NOT NULL,
    ProductID INT NOT NULL,
    MoistureLevel DECIMAL(5,2) NOT NULL,
    ProcessingStage NVARCHAR(100) NOT NULL,
    SupplierDetails NVARCHAR(MAX) NULL,
    CONSTRAINT PK_MaterialTracking PRIMARY KEY CLUSTERED (LogID),
    CONSTRAINT FK_MaterialTracking_Products FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

-- 3. High-Fidelity Data Seeding Matrix (Phase 2 Vector)
INSERT INTO MaterialTracking (ProductID, MoistureLevel, ProcessingStage, SupplierDetails)
VALUES 
(1, 14.50, N'Raw', N'Supplier A - Southern Province Clay Repository'),
(2, 22.15, N'Steaming', N'Supplier B - Central Highlands Cane Distributors'),
(1, 08.75, N'Mottling', N'Supplier A - Batch #4 High Density Profiles'),
(3, 05.20, N'Finished', N'Supplier C - Coastal Eco-Material Logistics'),
(2, 18.90, N'Raw', N'Supplier B - Standard Quality Cane Shipments');