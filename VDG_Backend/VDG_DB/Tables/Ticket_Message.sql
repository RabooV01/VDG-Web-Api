
CREATE TABLE [dbo].[Ticket_Message] 
(
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Ticket_Id] INT,
    [Text] TEXT,
    [Owner] VARCHAR(255),
    [Date] DATE,
    CONSTRAINT Message_Ticket_FK FOREIGN KEY([Ticket_Id]) REFERENCES [Ticket]([Id])
)

