
CREATE TABLE [dbo].[Ticket_Message] 
(
    [Id] int primary key identity(1,1) ,
    [Ticket_Id] INT FOREIGN KEY REFERENCES [Ticket]([Id]),
    [Text] TEXT ,
    [Owner] VARCHAR(255),
    [Date] DATE 
)

