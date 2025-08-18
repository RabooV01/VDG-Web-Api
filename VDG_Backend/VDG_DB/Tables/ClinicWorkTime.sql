CREATE TABLE [dbo].[ClinicWorkTime]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[VirtualClinic_Id] INT NOT NULL,
	[Start_WorkHours] DATETIME2,
	[End_WorkHours] DATETIME2
)
