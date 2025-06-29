CREATE TABLE [dbo].[Post] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [Doctor_Id] INT,
    [Content]   TEXT         NULL,
    CONSTRAINT [PK__Post__3214EC07FAD031E1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Post_Doctor_FK] FOREIGN KEY ([Doctor_Id]) REFERENCES [dbo].[Doctor]([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Post_Doctor_Id]
    ON [dbo].[Post]([Doctor_Id] ASC);

