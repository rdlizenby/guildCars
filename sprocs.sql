USE GuildCars

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetBodyStyles')
		DROP PROCEDURE GetBodyStyles
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetCarById')
		DROP PROCEDURE GetCarById
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetExteriorColors')
		DROP PROCEDURE GetExteriorColors
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetInteriorColors')
		DROP PROCEDURE GetInteriorColors
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetSpecials')
		DROP PROCEDURE GetSpecials
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetStates')
		DROP PROCEDURE GetStates
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetTransmissions')
		DROP PROCEDURE GetTransmissions
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetMakes')
		DROP PROCEDURE GetMakes
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetModels')
		DROP PROCEDURE GetModels
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetPurchaseTypes')
		DROP PROCEDURE GetPurchaseTypes
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetRoles')
		DROP PROCEDURE GetRoles
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetFeaturedShorts')
		DROP PROCEDURE GetFeaturedShorts
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetAllCars')
		DROP PROCEDURE GetAllCars
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetCarInteriorColorBridges')
		DROP PROCEDURE GetCarInteriorColorBridges
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetCarExteriorColorBridges')
		DROP PROCEDURE GetCarExteriorColorBridges
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'AddCar')
		DROP PROCEDURE AddCar
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetUsers')
		DROP PROCEDURE GetUsers
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CreateCarExteriorBridgeEntry')
		DROP PROCEDURE CreateCarExteriorBridgeEntry
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CreateCarInteriorBridgeEntry')
		DROP PROCEDURE CreateCarInteriorBridgeEntry
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DeleteCar')
		DROP PROCEDURE DeleteCar
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'EditCar')
		DROP PROCEDURE EditCar
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DeleteCarInteriorBridgeEntries')
		DROP PROCEDURE DeleteCarInteriorBridgeEntries
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DeleteCarExteriorBridgeEntries')
		DROP PROCEDURE DeleteCarExteriorBridgeEntries
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PostContactRequest')
		DROP PROCEDURE PostContactRequest
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SaveBuyer')
		DROP PROCEDURE SaveBuyer
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SaveCar')
		DROP PROCEDURE SaveCar
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SaveCar')
		DROP PROCEDURE SaveCar
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetUsers')
		DROP PROCEDURE GetUsers
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'UpdateUser')
		DROP PROCEDURE UpdateUser
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CreateUser')
		DROP PROCEDURE CreateUser
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CreateNewMake')
		DROP PROCEDURE CreateNewMake
GO	

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CreateNewModel')
		DROP PROCEDURE CreateNewModel
GO	

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'AddSpecial')
		DROP PROCEDURE AddSpecial
GO	

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DeleteSpecial')
		DROP PROCEDURE DeleteSpecial
GO	

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SaveSpecialImageFileName')
		DROP PROCEDURE SaveSpecialImageFileName
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SaveCarImageFileName')
		DROP PROCEDURE SaveCarImageFileName
GO	

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetSalesReport')
		DROP PROCEDURE GetSalesReport
GO	

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InventoryQuery')
		DROP PROCEDURE InventoryQuery
GO	

CREATE PROCEDURE InventoryQuery (@IsNew bit) AS
BEGIN
	SELECT c.[Year], k.MakeName, m.ModelName, Count(m.ModelName) AS [Count], SUM(c.SalePrice) AS StockValue
	FROM Car c
	JOIN Model m on c.ModelId = m.ModelId
	JOIN Make k on m.MakeId = k.MakeId
	WHERE c.IsNew = @IsNew AND c.IsSold = 0
	GROUP BY m.ModelName, k.MakeName, c.[Year]
END
GO	

CREATE PROCEDURE GetSalesReport(@StartDate date, @EndDate date) AS
BEGIN
	SELECT a.Id AS UserId, a.FirstName, a.LastName, sum(c.PurchasePrice) AS TotalSales, count(a.Id) AS TotalVehicles
	FROM AspNetUsers a
	JOIN Car c on a.Id = c.SoldBy
	WHERE c.SaleDate>@StartDate AND c.SaleDate<@EndDate
	Group by a.Id, a.FirstName, a.LastName;
END
GO

CREATE PROCEDURE GetUsers AS
BEGIN
	SELECT  a.Id AS UserId, a.FirstName, a.LastName, a.Email, b.RoleId, r.[Name] AS RoleName
	FROM AspNetUsers a 
	JOIN AspNetUserRoles b on a.Id = b.UserId
	JOIN AspNetRoles r on b.RoleId = r.Id
END
GO

CREATE PROCEDURE GetCarInteriorColorBridges AS
BEGIN
	SELECT CarId, InteriorColorId 
	FROM CarInteriorColorBridge
END
GO

CREATE PROCEDURE GetCarExteriorColorBridges AS
BEGIN
	SELECT CarId, ExteriorColorId 
	FROM CarExteriorColorBridge
END
GO

CREATE PROCEDURE GetAllCars AS
BEGIN
	SELECT c.CarId, c.ModelId, c.IsNew, c.TransmissionId, c.[Year], c.Milage, c.Vin, c.Msrp, c.SalePrice, c.[Description], c.ImageFileName, c.IsSold, c.IsFeatured, c.BuyerId, c.PurchasePrice, c.SoldBy, c.SaleDate, c.AddedDate, c.AddedBy, c.PurchaseTypeId
	FROM Car c
END
GO

CREATE PROCEDURE GetFeaturedShorts AS
BEGIN
	SELECT CarId, [Year], MakeName, ModelName, SalePrice, ImageFileName
	FROM Car
	Join Model on Car.ModelId = Model.ModelId
	Join Make on Model.MakeId = Make.MakeId
	WHERE car.IsFeatured = 1
END
GO

CREATE PROCEDURE GetRoles AS
BEGIN
	SELECT Id, [Name]
	FROM AspNetRoles
END
GO

CREATE PROCEDURE GetPurchaseTypes AS
BEGIN
	SELECT PurchaseTypeId, PurchaseTypeName
	FROM PurchaseType
END
GO

CREATE PROCEDURE GetMakes AS
BEGIN
	SELECT MakeId, MakeName, DateAdded, AddedBy
	FROM Make
END
GO

CREATE PROCEDURE GetModels AS
BEGIN
	SELECT ModelId, ModelName, MakeId, BodyStyleId, DateAdded, AddedBy
	FROM Model
END
GO

CREATE PROCEDURE GetTransmissions AS
BEGIN
	SELECT TransmissionId, TransmissionType
	FROM Transmission
END
GO

CREATE PROCEDURE GetStates AS
BEGIN
	SELECT StateId, StateAbbreviation, StateName
	FROM [State]
END
GO

CREATE PROCEDURE GetExteriorColors AS
BEGIN
	SELECT ExteriorColorId, ExteriorColorName
	FROM ExteriorColor
END
GO

CREATE PROCEDURE GetInteriorColors AS
BEGIN
	SELECT InteriorColorId, InteriorColorName
	FROM InteriorColor
END
GO

CREATE PROCEDURE GetSpecials AS
BEGIN
	SELECT SpecialId, SpecialName, SpecialDescription, ImageFileName, StartDate, EndDate
	FROM Special
END
GO

CREATE PROCEDURE GetCarById (@CarId int) AS
BEGIN
	SELECT c.CarId, c.ModelId, c.IsNew, c.TransmissionId, c.[Year], c.Milage, c.Vin, c.Msrp, c.SalePrice, c.[Description], c.ImageFileName, c.IsSold, c.IsFeatured, c.BuyerId, c.PurchasePrice, c.SoldBy, c.SaleDate, c.AddedDate, c.AddedBy, c.PurchaseTypeId
	FROM Car c
	WHERE c.CarId = @CarId
END
GO

CREATE PROCEDURE GetBodyStyles AS
BEGIN
	SELECT BodyStyleId, BodyStyleName
	FROM BodyStyle
END
GO


CREATE PROCEDURE CreateUser (@UserId nvarchar(256), @FirstName nvarchar(max), @LastName nvarchar(max), @Email nvarchar(256), @RoleId nvarchar(256)) AS
BEGIN 
INSERT INTO AspNetUsers (Id, FirstName, LastName, Email, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
VALUES (@UserId, @FirstName, @LastName, @Email, 0, 0, 0, 0, 0, @Email)

INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES (@UserId, @RoleId)
END
GO

Create Procedure AddCar (@CarId int OUTPUT, @ModelId int, @IsNew bit, @TransmissionId int, @Year int, @Milage int, @Vin varchar(20), @Msrp int, @SalePrice int, @Description varchar(500), @IsFeatured bit, @IsSold bit) AS
Begin
INSERT INTO Car (ModelId, IsNew, TransmissionId, [Year], Milage, Vin, Msrp, SalePrice, [Description], IsFeatured, IsSold)
VALUES (@ModelId, @IsNew, @TransmissionId, @Year, @Milage, @Vin, @Msrp, @SalePrice, @Description, @IsFeatured, @IsSold)
SET @CarId=SCOPE_IDENTITY();
END
GO

Create Procedure AddSpecial (@SpecialId int OUTPUT, @SpecialName varchar(260), @SpecialDescription varchar(2000), @StartDate date, @EndDate date ) AS
Begin
INSERT INTO Special (SpecialName, SpecialDescription, StartDate, EndDate, ImageFileName)
VALUES (@SpecialName, @SpecialDescription, @StartDate, @EndDate, 'placeholder')
SET @SpecialId=SCOPE_IDENTITY();
END
GO

Create Procedure CreateNewMake (@MakeName nvarchar(30), @DateAdded date, @AddedBy nvarchar(128)) AS
BEGIN
INSERT INTO Make (MakeName, DateAdded, AddedBy)
VALUES (@MakeName, @DateAdded, @AddedBy)
END
GO

Create Procedure CreateNewModel (@ModelName nvarchar(30), @DateAdded date, @AddedBy nvarchar(128), @MakeId int, @BodystyleId int) AS
BEGIN
INSERT INTO Model (ModelName, DateAdded, AddedBy, MakeId, BodyStyleId)
VALUES (@ModelName, @DateAdded, @AddedBy, @MakeId, @BodystyleId)
END
GO

Create Procedure SaveBuyer (@BuyerId int OUTPUT, @Name nvarchar(50), @Street1 varchar(50), @Street2 varchar(50), @Phone varchar(15), @City varchar(20), @Email nvarchar(30), @StateId int, @ZipCode int) AS
BEGIN
INSERT INTO Buyer ([Name], Street1, Street2, Phone, City, Email, StateId, ZipCode)
VALUES (@Name, @Street1, @Street2, @Phone, @City, @Email, @StateId, @ZipCode)
SET @BuyerId=SCOPE_IDENTITY();
END
GO

Create Procedure EditCar (@CarId int, @ModelId int, @IsNew bit, @TransmissionId int, @Year int, @Milage int, @Vin varchar(20), @Msrp int, @SalePrice int, @Description varchar(500), @IsFeatured bit, @IsSold bit) AS
Begin
UPDATE Car SET ModelId = @ModelId, IsNew = @IsNew, TransmissionId = @TransmissionId, [Year] = @Year, Milage = @Milage, Vin = @Vin, Msrp = @Msrp, SalePrice = @SalePrice, [Description] = @Description, IsFeatured = @IsFeatured, IsSold = @IsSold
WHERE Car.CarId = @CarId
END
GO

Create Procedure SaveSpecialImageFileName (@SpecialId int, @ImageFileName varchar(30)) AS
BEGIN
UPDATE Special SET ImageFileName = @ImageFileName
WHERE Special.SpecialId = @SpecialId
END
GO

Create Procedure SaveCarImageFileName (@CarId int, @ImageFileName varchar(30)) AS
BEGIN
UPDATE Car SET ImageFileName = @ImageFileName
WHERE Car.CarId = @CarId
END
GO

Create Procedure SaveCar (@CarId int, @IsSold bit, @BuyerId int, @PurchasePrice int, @PurchaseTypeId int, @SoldBy nvarchar(128), @SaleDate date) AS
BEGIN
UPDATE Car SET IsSold = @IsSold, BuyerId = @BuyerId, PurchasePrice = @PurchasePrice, PurchaseTypeId = @PurchaseTypeId, SoldBy = @SoldBy, SaleDate = @SaleDate
WHERE Car.CarId = @CarId
END
GO

CREATE PROCEDURE UpdateUser (@UserId nvarchar(256), @FirstName nvarchar(max), @LastName nvarchar(max), @Email nvarchar(256), @RoleId nvarchar(256)) AS
BEGIN
	DELETE FROM AspNetUserRoles  WHERE UserId = @UserId
	UPDATE AspNetUsers SET Email = @Email, FirstName = @FirstName, LastName = @LastName, UserName=@Email
	WHERE AspNetUsers.Id = @UserId
	INSERT INTO AspNetUserRoles(UserId, RoleId)
	VALUES (@UserId, @RoleId) 
END
GO

Create Procedure CreateCarExteriorBridgeEntry (@CarId int, @ExteriorColorId int) AS
Begin
INSERT INTO CarExteriorColorBridge(CarId, ExteriorColorId)
VALUES (@CarId, @ExteriorColorId)
SET @CarId=SCOPE_IDENTITY();
END
GO

Create Procedure PostContactRequest (@Name nvarchar(50), @Phone nvarchar(20), @Email nvarchar(30), @Message nvarchar(1000)) AS
Begin
INSERT INTO ContactRequest([Name], Phone, Email, [Message])
VALUES (@Name, @Phone, @Email, @Message)
END
GO

Create Procedure CreateCarInteriorBridgeEntry (@CarId int, @InteriorColorId int) AS
Begin
INSERT INTO CarInteriorColorBridge(CarId, InteriorColorId)
VALUES (@CarId, @InteriorColorId)
SET @CarId=SCOPE_IDENTITY();
END
GO

CREATE PROCEDURE DeleteCar (@CarId int)AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM CarExteriorColorBridge WHERE CarExteriorColorBridge.CarId = @CarId
	DELETE FROM CarInteriorColorBridge WHERE CarInteriorColorBridge.CarId = @CarId
	DELETE FROM Car WHERE Car.CarId = @CarId

	COMMIT TRANSACTION
END
GO

CREATE PROCEDURE DeleteCarExteriorBridgeEntries (@CarId int)AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM CarExteriorColorBridge WHERE CarExteriorColorBridge.CarId = @CarId

	COMMIT TRANSACTION
END
GO

CREATE PROCEDURE DeleteSpecial (@SpecialId int)AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM Special WHERE Special.SpecialId = @SpecialId

	COMMIT TRANSACTION
END
GO

CREATE PROCEDURE DeleteCarInteriorBridgeEntries (@CarId int)AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM CarInteriorColorBridge WHERE CarInteriorColorBridge.CarId = @CarId

	COMMIT TRANSACTION
END
GO
