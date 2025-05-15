CREATE TABLE [dbo].[Booking] 
(
    [Id] int primary key identity(1,1) ,
    [User_Id] int foreign key references [User](Id),
    [Vritual_Id] int foreign key references Virtual_Clinic(Id),
    [Time] TIME ,
    [Type] VARCHAR(255) check ([Type] in ('preview','revision')),
)
