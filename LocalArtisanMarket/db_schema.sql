IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'LocalArtisanMarket')
BEGIN
    CREATE DATABASE LocalArtisanMarket;
END
GO

USE LocalArtisanMarket;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        UserID INT IDENTITY(1,1) PRIMARY KEY,
        Username VARCHAR(50) NOT NULL UNIQUE,
        Password VARCHAR(255) NOT NULL
    );
END
GO