CREATE TABLE [dbo].[Rating] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [User_Id]     INT          NULL,
    [Doctor_Id]   INT,
    [Avg_Wait]    FLOAT (53)   NULL,
    [Avg_Service] FLOAT (53)   NULL,
    [Act]         FLOAT (53)   NULL,
    CONSTRAINT [PK__Rating__3214EC07B32B9B16] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Rating_Doctor_FK] FOREIGN KEY ([Doctor_Id]) REFERENCES [dbo].[Doctor] ([Id]),
    CONSTRAINT [Rating_User_FK] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Rating_Doctor_Id]
    ON [dbo].[Rating]([Doctor_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Rating_User_Id]
    ON [dbo].[Rating]([User_Id] ASC);

