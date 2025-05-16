CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY,
	Person_Id INT,
	Email VARCHAR(128) not null,
	Password_Hash VARCHAR(128) not null,
	Role VARCHAR(32),
	CONSTRAINT User_Person_FK FOREIGN KEY(Person_Id) REFERENCES Person(Id) ON DELETE CASCADE,
	CONSTRAINT Role_Enum CHECK(Role in ('user','admin','doctor'))
)
