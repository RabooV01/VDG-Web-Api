CREATE TABLE [dbo].[Doctor]
(
	Syndicate_Id varchar(16) NOT NULL PRIMARY KEY,
	User_Id int,
	Speciality_Id int,
	constraint Doctor_User_FK foreign key(User_Id) references [User](Id),
	constraint Doctor_Speciality_FK foreign key([Speciality_Id]) references [Speciality]([Id])
)
