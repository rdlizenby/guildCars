IF EXISTS(SELECT * FROM sys.tables WHERE name='CarExteriorColorBridge')
	DROP TABLE CarExteriorColorBridge
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='CarInteriorColorBridge')
	DROP TABLE CarInteriorColorBridge
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Car')
	DROP TABLE Car
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Buyer')
	DROP TABLE Buyer
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='State')
	DROP TABLE State
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Special')
	DROP TABLE Special
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='ContactRequest')
	DROP TABLE ContactRequest
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='PurchaseType')
	DROP TABLE PurchaseType
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Model')
	DROP TABLE Model
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='BodyStyle')
	DROP TABLE BodyStyle
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Make')
	DROP TABLE Make
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='ExteriorColor')
	DROP TABLE ExteriorColor
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='InteriorColor')
	DROP TABLE InteriorColor
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Transmission')
	DROP TABLE Transmission
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='User')
	DROP TABLE [User]
GO

CREATE TABLE [User] (
	PKId int identity(1,1) primary key,
	Id nvarchar(128) not null foreign key references AspNetUsers,
	FirstName nvarchar(64) not null,
	LastName nvarchar(64) not null,
	RoleId nvarchar(128) foreign key references AspNetRoles(Id)
)

CREATE TABLE Transmission (
	TransmissionId int identity(1,1) primary key,
	TransmissionType varchar(12) not null
)

CREATE TABLE InteriorColor (
	InteriorColorId int identity(1,1) primary key,
	InteriorColorName varchar(30) not null
)

CREATE TABLE ExteriorColor (
	ExteriorColorId int identity(1,1) primary key,
	ExteriorColorName varchar(30) not null
)

CREATE TABLE Make (
	MakeId int identity(1,1) primary key,
	MakeName nvarchar(30) not null,
	DateAdded date default(getdate()),
	AddedBy nvarchar(128) default Current_User
)

CREATE TABLE BodyStyle (
	BodyStyleId int identity(1,1) primary key,
	BodyStyleName varchar(20) not null
)

CREATE TABLE Model (
	ModelId int identity(1,1) primary key,
	ModelName nvarchar(30) not null,
	MakeId int not null foreign key references Make,
	BodyStyleId int not null foreign key references BodyStyle,
	DateAdded date default (getdate()),
	AddedBy nvarchar(128) default Current_User
)

CREATE TABLE PurchaseType (
	PurchaseTypeId int identity(1,1) primary key,
	PurchaseTypeName varchar(20) not null
)

CREATE TABLE Special (
	SpecialId int identity(1,1) primary key,
	SpecialName varchar(260) not null,
	SpecialDescription varchar(2000) not null,
	ImageFileName varchar(30) not null,
	StartDate date not null,
	EndDate date not null 
)

CREATE TABLE [State] (
	StateId int identity(1,1) primary key,
	StateAbbreviation char(2) not null,
	StateName varchar(15) not null
)

CREATE TABLE Buyer (
	BuyerId int identity(1,1) primary key,
	[Name] nvarchar(50) not null,
	Street1 varchar(50) not null,
	Street2 varchar(50),
	City varchar(20) not null,
	Phone varchar(15),
	Email nvarchar(30),
	StateId int not null foreign key references [State],
	ZipCode int not null
)

CREATE TABLE ContactRequest (
	ContactRequestId int identity(1,1) primary key,
	[Name] nvarchar(50) not null,
	Phone nvarchar(20),
	Email nvarchar(30),
	[Message] varchar(1000) not null,
	RespondedBy nvarchar(128),
	RespondedOn datetime,
	Response varchar(1000) 
)

CREATE TABLE Car (
	CarId int identity(1,1) primary key,
	ModelId int not null foreign key references Model,
	IsNew bit not null,
	TransmissionId int not null foreign key references Transmission,
	[Year] int not null,
	Milage int not null,
	Vin varchar(20) not null,
	Msrp int not null,
	SalePrice int not null,
	[Description] varchar(500) not null,
	ImageFileName varchar(30),
	IsFeatured bit not null default 0,
	IsSold bit not null default 0,
	BuyerId int foreign key references Buyer,
	PurchasePrice int,
	SoldBy nvarchar(128) foreign key references AspNetUsers,
	SaleDate date,
	AddedDate date default (getdate()),
	AddedBy nvarchar(128) default Current_User,
	PurchaseTypeId int foreign key references PurchaseType
)

CREATE TABLE CarExteriorColorBridge (
	CarExteriorColorBridge int identity(1,1) primary key,
	CarId int not null foreign key references Car,
	ExteriorColorId int not null foreign key references ExteriorColor
)

CREATE TABLE CarInteriorColorBridge (
	CarInteriorColorBridge int identity(1,1) primary key,
	CarId int not null foreign key references Car,
	InteriorColorId int not null foreign key references InteriorColor
)
