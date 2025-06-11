CREATE TABLE [dbo].[User] (
    [Id]            INT           NOT NULL,
    [Person_Id]     INT           NULL,
    [Email]         VARCHAR (128) NOT NULL,
    [Password_Hash] VARCHAR (128) NOT NULL,
    [Role]          VARCHAR (32)  NULL,
    CONSTRAINT [PK__User__3214EC071AAE95BF] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [User_Person_FK] FOREIGN KEY ([Person_Id]) REFERENCES [dbo].[Person] ([Id]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_Person_Id]
    ON [dbo].[User]([Person_Id] ASC) WHERE ([Person_Id] IS NOT NULL);

