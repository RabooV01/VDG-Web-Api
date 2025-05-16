CREATE TABLE [dbo].[Reservation] 
(
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [User_Id] INT,
    [Vritual_Id] INT,
    [Time] TIME,
    [Type] VARCHAR(255),
    CONSTRAINT Reservation_User_FK FOREIGN KEY([User_Id]) REFERENCES [User](Id),
    CONSTRAINT Reservation_Virtual_FK FOREIGN KEY(Vritual_Id) REFERENCES Virtual_Clinic(Id),
    CONSTRAINT Reservation_type_Enum CHECK ([Type] in ('preview','revision'))
)
