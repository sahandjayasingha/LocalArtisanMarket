IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        UserID INT IDENTITY(1,1) PRIMARY KEY,
        Username VARCHAR(50) NOT NULL UNIQUE,
        Password VARCHAR(255) NOT NULL
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