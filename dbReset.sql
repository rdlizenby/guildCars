IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DbReset')
		DROP PROCEDURE DbReset
GO

CREATE PROCEDURE DbReset AS
BEGIN
	DELETE FROM CarExteriorColorBridge;
	DELETE FROM CarInteriorColorBridge;
	DELETE FROM Car;
	DELETE FROM Model;
	DELETE FROM Buyer;
	DELETE FROM [State];
	DELETE FROM [Make];
	DELETE FROM BodyStyle;
	DELETE FROM ExteriorColor;
	DELETE FROM InteriorColor;
	DELETE FROM Transmission;
	DELETE FROM Special;
	DELETE FROM ContactRequest;
	DELETE FROM PurchaseType;
	DELETE FROM [User];

	DBCC CHECKIDENT (Car,reseed, 0)
	DBCC CHECKIDENT (Model,reseed, 0)
	DBCC CHECKIDENT (Buyer,reseed, 0)
	DBCC CHECKIDENT ([State],reseed, 0)
	DBCC CHECKIDENT ([Make],reseed, 0)
	DBCC CHECKIDENT (BodyStyle,reseed, 0)
	DBCC CHECKIDENT (ExteriorColor,reseed, 0)
	DBCC CHECKIDENT (InteriorColor,reseed, 0)
	DBCC CHECKIDENT (Transmission,reseed, 0)
	DBCC CHECKIDENT (Special ,reseed, 0)
	DBCC CHECKIDENT (ContactRequest,reseed, 0)
	DBCC CHECKIDENT (PurchaseType,reseed, 0)
	DBCC CHECKIDENT (CarExteriorColorBridge,reseed, 0)
	DBCC CHECKIDENT ([User],reseed, 0)
	DBCC CHECKIDENT (CarInteriorColorBridge,reseed, 0)

	INSERT INTO [User] (Id, FirstName, LastName, RoleId)
	VALUES ('1d52e2f5-2b40-4e2a-bbe5-58b7d80932ed', 'Simon', 'Hudson', '0df6979a-34d9-4417-ab29-71fc1504e7f0'),
	('9248ee20-2032-4c74-8455-14f69ab4a384', 'Sullivan', 'Lizenby', '9248ee20-2032-4c74-8455-14f69ab4a384')

	Set Identity_Insert PurchaseType On
	INSERT INTO PurchaseType (PurchaseTypeId, PurchaseTypeName)
	VALUES (1, 'Bank Finance'),
	(2, 'Cash'),
	(3, 'Dealer Finance')
	Set Identity_Insert PurchaseType Off

	Set Identity_Insert ContactRequest On
	INSERT INTO ContactRequest (ContactRequestId, [Name], Phone, Email, [Message], RespondedBy, RespondedOn, Response)
	VALUES (1, 'Joe Smith', '123-456-7890','prospectiveBuyer1@gmail.com', 'I like fast cars...', '1d52e2f5-2b40-4e2a-bbe5-58b7d80932ed', '2017-10-02', 'So happy for you'),
	(2, 'Bob Evans', '223-456-7890', 'prospectiveBuyer2@gmail.com', 'Can I get $15,000 trade-in on my 1983 Toyota Corolla?', '1d52e2f5-2b40-4e2a-bbe5-58b7d80932ed', '2017-10-04', 'That seems a little unreasonable'),
	(3, 'Joe Evans', '323-456-7890', 'prospectiveBuyer3@gmail.com', 'I''m really good at Call of Duty.  You think that''s close enough to being in the military to get the discount?', null, null, null),
	(4, 'Bob Smith', '423-456-7890', 'prospectiveBuyer4@gmail.com',  'What''s the deal with dropping your Uber partnership and going with Lyft?', null, null, null)
	Set Identity_Insert ContactRequest Off
	
	Set Identity_Insert Special On
	INSERT INTO Special (SpecialId, SpecialName, ImageFileName, SpecialDescription, StartDate, EndDate)
	VALUES (1, '0% APR, 60 Months', 'special1.jpg', '0% Annual Percentage Rate (APR) up to 36 months. 0% Annual Percentage Rate (APR) up to 60 months. 0.9% Annual Percentage Rate (APR) up to 66 months. 1.9% Annual Percentage Rate (APR) up to 72 months. Plus $500 Cash Back APR financing available, subject to credit approval..', '2017-06-01', '2017-12-31'),
	(2, 'Military Discount', 'special2.jpg', '$400 Military Bonus For Qualified Customers.  Customers eligible for the Military Specialty Incentive Program must be an active member of, honorably discharged from, retired from, or on disability with the United States Armed Forces or Reserves (includes those that have “national” status from another country and are serving in the United States military) or the spouse of the eligible participant.', '2017-04-01', '2017-10-31'),
	(3, 'Student Program', 'special3.jpg', '$400 College Student & Graduate Bonus for Qualified Buyers. We are helping recent graduates make the transition from school to their careers by making it easy and affordable to buy a new Kia vehicle by offering a $400 discount incentive in addition to certain other incentives. Not all incentive programs are compatible.', '2017-01-01','2017-12-31'),
	(4, 'Uber Driver Program', 'special4.jpg', '$1,000 Uber Driver Partner Incentive for Qualfied Buyers. Customers eligible for the Uber Driver Partner Specialty Incentive program must be an active Uber Driver Partner and the primary buyer of the vehicle at the time of purchase.', '2017-04-01', '2017-08-31')
	Set Identity_Insert Special Off

	Set Identity_Insert Transmission On
	INSERT INTO Transmission (TransmissionId, TransmissionType)
	VALUES (1, 'Automatic'),
	(2, 'Manual')
	Set Identity_Insert Transmission Off

	Set Identity_Insert InteriorColor On
	INSERT INTO InteriorColor (InteriorColorId, InteriorColorName)
	VALUES (1, 'Grey'),
	(2, 'Black'),
	(3, 'White'),
	(4, 'Tan'),
	(5, 'Beige'),
	(6, 'Maroon')
	Set Identity_Insert InteriorColor Off

	Set Identity_Insert ExteriorColor On
	INSERT INTO ExteriorColor (ExteriorColorId, ExteriorColorName)
	VALUES (1, 'White'),
	(2, 'Black'),
	(3, 'Tan'),
	(4, 'Gold'),
	(5, 'Red'),
	(6, 'Blue'),
	(7, 'Green'),
	(8, 'Orange'),
	(9, 'Silver'),
	(10, 'Brown')
	Set Identity_Insert ExteriorColor Off
	
	Set Identity_Insert BodyStyle On
	INSERT INTO BodyStyle (BodyStyleId, BodyStyleName)
	VALUES (1, 'Crossover'),
	(2, 'Sedan'),
	(3, 'Truck'),
	(4, 'Van'),
	(5, 'Sports Car'),
	(6, 'Station Wagon'),
	(7, 'SUV')
	Set Identity_Insert BodyStyle Off

	Set Identity_Insert Make On
	INSERT INTO Make (MakeId, MakeName, DateAdded, AddedBy)
	VALUES (1, 'Honda', '2017-10-1', '9248ee20-2032-4c74-8455-14f69ab4a384'),
	(2, 'Toyota', '2017-10-2', '9248ee20-2032-4c74-8455-14f69ab4a384'),
	(3, 'Lexus', '2017-10-3', '9248ee20-2032-4c74-8455-14f69ab4a384'),
	(4, 'Chevrolet', '2017-10-4', '9248ee20-2032-4c74-8455-14f69ab4a384')
	Set Identity_Insert Make Off

	Set Identity_Insert [State] On
	INSERT INTO [State] (StateId, StateAbbreviation, StateName)
	VALUES 	(1, 'AL','Alabama'),
	(2, 'CT','Connecticut'),
	(3, 'IL','Illinois'),
	(4, 'IN', 'Indiana'),
	(5, 'KY', 'Kentucky'),
	(6, 'MI','Michigan'),
	(7, 'MN', 'Minnesota'),
	(8, 'MS','Mississippi'),
	(9, 'NY','New York'),
	(10, 'OH', 'Ohio'),
	(11, 'TN','Tennessee'),
	(12, 'WA','Washington')
	Set Identity_Insert [State] Off

	Set Identity_Insert Model On
	INSERT INTO Model (ModelId, ModelName, MakeId, BodyStyleId, DateAdded, AddedBy)
	VALUES (1, 'CRV', 1, 1, '2017-10-01', '9248ee20-2032-4c74-8455-14f69ab4a384'),
	(2, 'Accord', 1, 2, '2017-10-01', '9248ee20-2032-4c74-8455-14f69ab4a384'),
	(3, 'Tacoma', 2, 3, '2017-10-02', '9248ee20-2032-4c74-8455-14f69ab4a384'),
	(4, 'Sienna', 2, 4, '2017-10-02', '9248ee20-2032-4c74-8455-14f69ab4a384'),
	(5, 'LS', 3, 2, '2017-10-03', '9248ee20-2032-4c74-8455-14f69ab4a384'),
	(6, 'RC', 3, 5, '2017-10-03', '9248ee20-2032-4c74-8455-14f69ab4a384'),
	(7, 'Malibu', 4, 6, '2017-10-04', '9248ee20-2032-4c74-8455-14f69ab4a384'),
	(8, 'Traverse', 4, 7, '2017-10-05', '9248ee20-2032-4c74-8455-14f69ab4a384')
	Set Identity_Insert Model Off

	Set Identity_Insert Buyer On
	INSERT INTO Buyer (BuyerId, [Name], Street1, Street2, City, Phone, Email, StateId, ZipCode)
	VALUES (1, 'Cameran Cochran', '387-7170 Nibh St.', 'Apt 3', 'Biloxi', '111-222-3333', 'cCochran@gmail.com', 10, 38506),
	(2, 'Sheila Marsh', 'Ap #665-3410 Feugiat Rd.',null, 'Hamme', '222-333-4444','sMarsh@gmail.com', 11, 70916),
	(3, 'Brent Haynes', '2393 Nulla. St.',null , 'Opheylissem', '333-444-5555','bHaynes@gmail.com', 4, 67677)
	Set Identity_Insert Buyer Off

	Set Identity_Insert Car On
	INSERT INTO Car (CarId, ModelId, IsNew, TransmissionId, [Year], Milage, Vin, Msrp, SalePrice, [Description], ImageFileName, IsFeatured, IsSold, BuyerId, PurchasePrice, SoldBy, SaleDate, AddedDate, AddedBy, PurchaseTypeId) 
	VALUES 
	(1, 1, 1, 1, 2017, 180, '1G4GB5EG7AF110257', 24000, 23500 ,'This is a new Honda CRV', 'images/crv2017.jpg', 1, 0, null, null, null, null, '2017-10-01', '9248ee20-2032-4c74-8455-14f69ab4a384', null ),
	(2, 2, 1, 2, 2017, 220, '1FTPX28Z3XKC90533', 22500, 22000, 'This is a brand new Honda Accord', 'images/accord2017.jpg', 0, 1, 1, 22000, '1d52e2f5-2b40-4e2a-bbe5-58b7d80932ed', '2017-10-02', '2017-10-02', '9248ee20-2032-4c74-8455-14f69ab4a384', 1),
    (3, 3, 1, 1, 2017, 498, 'JF1AF21B4DA191572', 24550, 24500, 'This is a brand new Toyota Tacoma', 'images/tacoma2017.jpg', 1, 0, null, null, null, null, '2017-10-03', '9248ee20-2032-4c74-8455-14f69ab4a384', null),
    (4, 4, 1, 1, 2018,  15, '1M2P286C5TM063936', 29750, 29500, 'This is a brand new Toyota Sienna', 'images/sienna2017.jpg', 0, 0, null, null, null, null, '2017-10-04', '9248ee20-2032-4c74-8455-14f69ab4a384',null),
	(5, 5, 1, 1, 2018,   0, '1C6RD7MT0CS225625', 72500, 69500, 'This is a brand new Lexus LS', 'images/ls2017.jpg', 1, 0, null, null, null, null,'2017-10-05', '9248ee20-2032-4c74-8455-14f69ab4a384',null),
	(6, 6, 0, 2, 2015, 54288, 'KMHCU4AE3CU005446', 38000, 21700,  'This is a used Lexus RC', 'images/rc2017.jpg', 1, 0, null, null, null, null, '2017-10-06', '9248ee20-2032-4c74-8455-14f69ab4a384',null),
	(7, 7, 0, 1, 2013, 73560, '1GKET12P146124900', 28500, 14300, 'This is a used Chevy Malibu',  'images/malibu2013.jpg', 0, 0, null, null, null, null, '2017-10-07', '9248ee20-2032-4c74-8455-14f69ab4a384',null),
	(8, 8, 0, 1, 2012, 68234, 'JH2RC44392K602727', 45500, 28700,  'This is a used Chevy Traverse', 'images/traverse2012.jpg', 0, 1, 2, 28000, '1d52e2f5-2b40-4e2a-bbe5-58b7d80932ed', '2017-09-25', '2017-10-08', '9248ee20-2032-4c74-8455-14f69ab4a384', 3),
	(9, 1, 0, 1, 2015, 27001, 'WVWVD63B84E006972', 21500, 21500, 'This is a used Honda CRV', 'images/crv2016.jpg', 1, 0, null, null, null, null, '2017-10-09', '9248ee20-2032-4c74-8455-14f69ab4a384', null),
	(10, 2, 0, 2, 2006, 117392, '1FTMF1E89AKE99651', 17500, 9500, 'This is a used Honda Accord', 'images/accord2006.jpg', 0, 1, 3, 9250, '1d52e2f5-2b40-4e2a-bbe5-58b7d80932ed', '2017-10-04', '2017-10-10', '9248ee20-2032-4c74-8455-14f69ab4a384', 2)
	Set Identity_Insert Car Off

	Set Identity_Insert CarInteriorColorBridge On
	INSERT INTO CarInteriorColorBridge (CarInteriorColorBridge, CarId, InteriorColorId)
	VALUES
	(1, 1, 1),
	(2, 2, 2),
	(3, 2, 3),
	(4, 3, 4),
	(5, 4, 5),
	(6, 5, 6),
	(7, 6, 1),
	(8, 6, 2),
	(9, 7, 3),
	(10, 8, 4),
	(11, 9, 5),
	(12, 10, 6)
	Set Identity_Insert CarInteriorColorBridge Off

	Set Identity_Insert CarExteriorColorBridge On
	INSERT INTO CarExteriorColorBridge (CarExteriorColorBridge, CarId, ExteriorColorId)
	VALUES
	(1, 1, 1),
	(2, 2, 2),
	(3, 3, 3),
	(4, 4, 4),
	(5, 5, 5),
	(6, 6, 6),
	(7, 6, 7),
	(8, 7, 8),
	(9, 8, 9),
	(10, 9, 10),
	(11, 9, 1),
	(12, 10, 2)
	Set Identity_Insert CarExteriorColorBridge Off

END