CREATE TABLE [dbo].[Ticket_Message] (
    [Id]        INT  IDENTITY (1, 1) NOT NULL,
    [Ticket_Id] INT  NULL,
    [Text]      TEXT NULL,
    [OwnerId]   INT  NULL,
    [Date]      DATE NULL,
    CONSTRAINT [PK__Ticket_M__3214EC079464AE2E] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Message_Ticket_FK] FOREIGN KEY ([Ticket_Id]) REFERENCES [dbo].[Ticket] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Ticket_Message_Ticket_Id]
    ON [dbo].[Ticket_Message]([Ticket_Id] ASC);

