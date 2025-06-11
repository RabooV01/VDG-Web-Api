CREATE TABLE [dbo].[Reservation] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [User_Id]     INT            NULL,
    [Virtual_Id]  INT            NULL,
    [ScheduledAt] DATETIME2 (7)  NOT NULL,
    [Text]        NVARCHAR (512) NOT NULL,
    [Type]        NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK__tmp_ms_x__3214EC07765A5547] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Reservation_User_FK] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [Reservation_Virtual_FK] FOREIGN KEY ([Virtual_Id]) REFERENCES [dbo].[Virtual_Clinic] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Reservation_User_Id]
    ON [dbo].[Reservation]([User_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Reservation_Virtual_Id]
    ON [dbo].[Reservation]([Virtual_Id] ASC);

