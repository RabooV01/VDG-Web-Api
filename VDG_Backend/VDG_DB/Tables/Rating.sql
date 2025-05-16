CREATE TABLE [dbo].[Rating] 
(
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [User_Id] INT,
    [Doctor_Id] VARCHAR(16),
    [Avg_Wait] FLOAT,
    [Avg_Service] FLOAT,
    [Act] FLOAT,
    CONSTRAINT Rating_User_FK FOREIGN KEY([User_Id]) REFERENCES [User](Id),
    CONSTRAINT Rating_Doctor_FK FOREIGN KEY(Doctor_Id) REFERENCES Doctor(Syndicate_Id)
)