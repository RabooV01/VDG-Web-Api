CREATE TABLE [dbo].[Virtual_Clinic] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Doctor_Id]        VARCHAR (16)  NULL,
    [Start_Work_Hours] TIME (7)      NULL,
    [End_Work_Hours]   TIME (7)      NULL,
    [Location]         VARCHAR (255) NULL,
    [Status]           VARCHAR (255) NULL,
    [Avg_Service]      FLOAT (53)    NULL,
    [Ticket_Status]    VARCHAR (255) NULL,
    [Preview_Const]    FLOAT (53)    NULL,
    [Ticket_Const]     FLOAT (53)    NULL,
    CONSTRAINT [PK__Virtual___3214EC078277E8B9] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Clinic_Doctor_FK] FOREIGN KEY ([Doctor_Id]) REFERENCES [dbo].[Doctor] ([Syndicate_Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Virtual_Clinic_Doctor_Id]
    ON [dbo].[Virtual_Clinic]([Doctor_Id] ASC);

