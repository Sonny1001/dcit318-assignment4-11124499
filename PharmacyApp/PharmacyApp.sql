CREATE DATABASE PharmacyDB;
GO

USE PharmacyDB;
GO

-- Medicines table
CREATE TABLE Medicines (
    MedicineID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    Category VARCHAR(50),
    Price DECIMAL(10,2),
    Quantity INT
);
GO

-- Sales table with proper FK syntax
CREATE TABLE Sales (
    SaleID INT PRIMARY KEY IDENTITY(1,1),
    MedicineID INT NOT NULL,
    QuantitySold INT,
    SaleDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Sales_Medicines FOREIGN KEY (MedicineID) REFERENCES Medicines(MedicineID)
);
GO

-- Stored Procedures
CREATE PROCEDURE AddMedicine
    @Name VARCHAR(100), 
    @Category VARCHAR(50), 
    @Price DECIMAL(10,2), 
    @Quantity INT
AS
BEGIN
    INSERT INTO Medicines (Name, Category, Price, Quantity) 
    VALUES (@Name, @Category, @Price, @Quantity);
END;
GO

CREATE PROCEDURE SearchMedicine
    @SearchTerm VARCHAR(100)
AS
BEGIN
    SELECT * 
    FROM Medicines 
    WHERE Name LIKE '%' + @SearchTerm + '%' 
       OR Category LIKE '%' + @SearchTerm + '%';
END;
GO

CREATE PROCEDURE UpdateStock
    @MedicineID INT, 
    @Quantity INT
AS
BEGIN
    UPDATE Medicines 
    SET Quantity = Quantity + @Quantity 
    WHERE MedicineID = @MedicineID;
END;
GO

CREATE PROCEDURE RecordSale
    @MedicineID INT, 
    @QuantitySold INT
AS
BEGIN
    INSERT INTO Sales (MedicineID, QuantitySold) 
    VALUES (@MedicineID, @QuantitySold);

    UPDATE Medicines 
    SET Quantity = Quantity - @QuantitySold 
    WHERE MedicineID = @MedicineID;
END;
GO

CREATE PROCEDURE GetAllMedicines
AS
BEGIN
    SELECT * FROM Medicines;
END;
GO
