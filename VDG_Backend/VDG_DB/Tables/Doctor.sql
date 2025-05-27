CREATE TABLE [dbo].[Doctor]
(
	Syndicate_Id VARCHAR(16) PRIMARY KEY,
	[User_Id] INT,
	Speciality_Id INT,
	CONSTRAINT Doctor_User_FK FOREIGN KEY(User_Id) REFERENCES [User](Id),
	CONSTRAINT Doctor_Speciality_FK FOREIGN KEY([Speciality_Id]) REFERENCES [Speciality]([Id])
)
