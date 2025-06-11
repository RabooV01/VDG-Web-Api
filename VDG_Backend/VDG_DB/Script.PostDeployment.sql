GO

IF NOT EXISTS (SELECT 1 FROM Person)
BEGIN
	INSERT INTO Person (First_Name, Last_Name, Gender, Phone, Personal_Id, Birthdate) VALUES
	('John', 'Doe', 'Male', '123-456-7890', 'P12345', '1990-05-15'),
	('Jane', 'Smith', 'Female', '234-567-8901', 'P12346', '1988-08-20'),
	('Alice', 'Johnson', 'Female', '345-678-9012', 'P12347', '1992-12-01'),
	('Bob', 'Brown', 'Male', '456-789-0123', 'P12348', '1985-03-10'),
	('Charlie', 'Davis', 'Male', '567-890-1234', 'P12349', '1995-07-25'),
	('Diana', 'Wilson', 'Female', '678-901-2345', 'P12350', '1993-11-30');
END
GO

IF NOT EXISTS (SELECT 1 FROM [User])
BEGIN
	INSERT INTO [User] (Id, Email, [Password_Hash], Person_Id, [Role]) VALUES
	(1, 'john.doe@example.com', 'hashed_password_for_John', 1, 'User'),
	(2, 'jane.smith@example.com', 'hashed_password_for_Jane', 2, 'User'),
	(3, 'alice.johnson@example.com', 'hashed_password_for_Alice', 3, 'Doctor'),
	(4, 'bob.brown@example.com', 'hashed_password_for_Bob', 4, 'User'),
	(5, 'charlie.davis@example.com', 'hashed_password_for_Charlie', 5, 'User'),
	(6, 'diana.wilson@example.com', 'hashed_password_for_Diana', 6, 'Doctor');
END
GO

IF NOT EXISTS (SELECT 1 FROM Speciality)
BEGIN
	INSERT INTO Speciality (Specialty) VALUES
	('General'),
	('Dentist');
END
GO

IF NOT EXISTS (SELECT 1 FROM Doctor)
BEGIN
	INSERT INTO [Doctor] (Syndicate_Id, Speciality_Id, [User_Id]) VALUES
	('A499KH', 1, 3),
	('A459ZH', 2, 6);
END
GO

IF NOT EXISTS(SELECT 1 FROM Virtual_Clinic)
BEGIN
INSERT INTO Virtual_Clinic (Doctor_Id, Avg_Service, Start_Work_Hours, End_Work_Hours, Preview_Const, Ticket_Const, Ticket_Status, [Status], [Location]) VALUES
	('A499KH', 30, '09:00:00', '17:00:00', 50, 20, 'Open', 'Active', 'City Hospital'),
	('A459ZH', 45, '10:00:00', '18:00:00', 70, 25, 'Request', 'Active', 'Downtown Clinic');
END

IF NOT EXISTS (SELECT 1 FROM Reservation)
BEGIN
	INSERT INTO [Reservation] (User_Id, Virtual_Id, ScheduledAt, Text, Type) VALUES
	(1, 'A499KH', '2023-10-03 09:00:00', 'Annual Check-up with Dr. John', 'Preview'),
	(2, 'A499KH', '2023-10-03 10:00:00', 'Follow-up appointment with Dr. John', 'Revision'),
	(1, 'A459ZH', '2023-10-04 14:00:00', 'Initial Consultation with Dr. Smith', 'Preview'),
	(3, 'A459ZH', '2023-10-05 16:00:00', 'Routine Check-up with Dr. Smith', 'Revision'),
	(2, 'A499KH', '2023-10-06 11:00:00', 'Specialist Consultation with Dr. John', 'Preview');
END
GO
