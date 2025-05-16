
CREATE TABLE [dbo].[Post] 
(
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Doctor_Id] VARCHAR(16),
    [Content] TEXT,
    CONSTRAINT Post_Doctor_FK FOREIGN KEY(Doctor_Id) REFERENCES [Doctor](Syndicate_Id)
)
