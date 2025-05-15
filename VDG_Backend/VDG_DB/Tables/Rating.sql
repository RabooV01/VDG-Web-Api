CREATE TABLE [dbo].[Rating] 
(
    [Id] int primary key identity(1,1) ,
    [User_Id] int foreign key references [User](Id),
    [Doctor_Id] varchar(16) foreign key references Doctor(Syndicate_Id),
    [Avg_Wait] FLOAT ,
    [Avg_Service] FLOAT ,
    [Act] FLOAT
)