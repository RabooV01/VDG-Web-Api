
CREATE TABLE [dbo].[Post] 
(
    [Id] int primary key identity(1,1) ,
    [Doctor_Id] varchar(16) foreign key references [Doctor](Syndicate_Id),
    [Content] TEXT
)
