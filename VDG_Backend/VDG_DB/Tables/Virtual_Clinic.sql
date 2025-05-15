CREATE TABLE [dbo].[Virtual_Clinic]
(
    [Id] int primary key identity(1,1) ,
    [Doctor_Id] varchar(16) foreign key references Doctor(Syndicate_Id),
    [Start_Work_Hours] TIME ,
    [End_Work_Hours] TIME ,
    [Status] VARCHAR(255) CHECK ([Status] in ('available' , 'unavailable')) ,
    [Avg_Service] FLOAT ,
    [Ticket_Status] VARCHAR(255) CHECK ([Ticket_Status] in ('any' , 'request','none')) ,
    [Preview_Const] FLOAT ,
    [Ticket_Const] FLOAT
)