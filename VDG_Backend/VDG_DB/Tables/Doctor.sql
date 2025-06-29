CREATE TABLE [dbo].[Doctor] (
    [Id]            INT IDENTITY,
    [Syndicate_Id]  VARCHAR (16) NOT NULL,
    [User_Id]       INT          NULL,
    [Speciality_Id] INT          NULL,
    [Ticket_Status] NVARCHAR(64),
    [Ticket_Cost] INT
    CONSTRAINT [PK__Doctor__B5BD6B27701103BA] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Doctor_Speciality_FK] FOREIGN KEY ([Speciality_Id]) REFERENCES [dbo].[Speciality] ([Id]),
    CONSTRAINT [Doctor_User_FK] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Doctor_Speciality_Id]
    ON [dbo].[Doctor]([Speciality_Id] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Doctor_User_Id]
    ON [dbo].[Doctor]([User_Id] ASC) WHERE ([User_Id] IS NOT NULL);

