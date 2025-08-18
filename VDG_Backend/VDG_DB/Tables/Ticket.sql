CREATE TABLE [dbo].[Ticket] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [User_Id]    INT          NULL,
    [Doctor_Id]  INT,
    [Status]     VARCHAR (16) NULL,
    [Close_Date] DATE         NULL,
    CONSTRAINT [PK__Ticket__3214EC076F6CA0F8] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Ticket_Doctor_FK] FOREIGN KEY ([Doctor_Id]) REFERENCES [dbo].[Doctor] ([Id]),
    CONSTRAINT [Ticket_User_FK] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Ticket_Doctor_Id]
    ON [dbo].[Ticket]([Doctor_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Ticket_User_Id]
    ON [dbo].[Ticket]([User_Id] ASC);

