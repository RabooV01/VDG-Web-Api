CREATE TABLE [dbo].[Person]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1),
	First_Name nvarchar(64) not null,
	Last_Name nvarchar(64) not null,
	Birthdate Date,
	Gender nvarchar(12),
	Personal_Id nvarchar(32),
	Phone varchar(16),
)
