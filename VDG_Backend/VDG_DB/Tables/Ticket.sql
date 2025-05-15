
CREATE TABLE [dbo].[Ticket] 
(
    [Id] int primary key identity(1,1) ,
    [User_Id] int foreign key references [User](Id),
    [Doctor_Id] varchar(16) foreign key references Doctor(Syndicate_Id),
    [Status] varchar CHECK ([Status] in( 'open' , 'closed','pending')) ,
    [Close_Date] DATE 
)
