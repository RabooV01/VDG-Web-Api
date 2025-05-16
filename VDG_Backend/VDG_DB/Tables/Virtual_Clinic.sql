CREATE TABLE [dbo].[Virtual_Clinic]
(
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Doctor_Id] VARCHAR(16),
    [Start_Work_Hours] TIME,
    [End_Work_Hours] TIME,
    [Status] VARCHAR(255),
    [Avg_Service] FLOAT,
    [Ticket_Status] VARCHAR(255) CHECK ([Ticket_Status] in ('any', 'request','none')),
    [Preview_Const] FLOAT,
    [Ticket_Const] FLOAT,
    CONSTRAINT Clinic_Doctor_FK FOREIGN KEY([Doctor_Id]) REFERENCES Doctor(Syndicate_Id),
    CONSTRAINT Clinic_Status_Enum CHECK ([Status] in ('available', 'unavailable'))
)