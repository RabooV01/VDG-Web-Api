CREATE TABLE [dbo].[Person]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1),
	First_Name NVARCHAR(64) not null,
	Last_Name NVARCHAR(64) not null,
	Birthdate Date,
	Gender NVARCHAR(12),
	Personal_Id NVARCHAR(32),
	Phone VARCHAR(16),
)
