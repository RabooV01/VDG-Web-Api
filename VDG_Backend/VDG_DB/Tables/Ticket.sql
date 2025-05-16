
CREATE TABLE [dbo].[Ticket] 
(
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [User_Id] INT,
    [Doctor_Id] VARCHAR(16),
    [Status] VARCHAR(16),
    [Close_Date] DATE,
    CONSTRAINT Ticket_User_FK FOREIGN KEY([User_Id]) REFERENCES [User](Id),
    CONSTRAINT Ticket_Doctor_FK FOREIGN KEY([Doctor_Id]) REFERENCES Doctor(Syndicate_Id),
    CONSTRAINT Ticket_Status_Enum CHECK ([Status] in( 'open', 'closed','pending'))
)
