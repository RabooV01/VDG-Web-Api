CREATE TABLE [dbo].[Person] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [First_Name]  NVARCHAR (64) NOT NULL,
    [Last_Name]   NVARCHAR (64) NOT NULL,
    [Birthdate]   DATE          NULL,
    [Gender]      NVARCHAR (12) NULL,
    [Personal_Id] NVARCHAR (32) NULL,
    [Phone]       VARCHAR (16)  NULL,
    CONSTRAINT [PK__Person__3214EC07A0A03B3C] PRIMARY KEY CLUSTERED ([Id] ASC)
);

