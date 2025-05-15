CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY,
	Person_Id int foreign key references Person(Id) on delete cascade,
	Email varchar(128) not null,
	Password_Hash varchar(128) not null,
	Role varchar(32),
	constraint Role_Enum check(Role in ('user','admin','doctor'))
)
