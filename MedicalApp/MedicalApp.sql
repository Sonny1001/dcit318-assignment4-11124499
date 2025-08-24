CREATE DATABASE MedicalDB;
GO
USE MedicalDB;

CREATE TABLE Doctors (
    DoctorID INT PRIMARY KEY IDENTITY(1,1),
    FullName VARCHAR(100),
    Specialty VARCHAR(100),
    Availability BIT
);

CREATE TABLE Patients (
    PatientID INT PRIMARY KEY IDENTITY(1,1),
    FullName VARCHAR(100),
    Email VARCHAR(100)
);

CREATE TABLE Appointments (
    AppointmentID INT PRIMARY KEY IDENTITY(1,1),
    DoctorID INT FOREIGN KEY REFERENCES Doctors(DoctorID),
    PatientID INT FOREIGN KEY REFERENCES Patients(PatientID),
    AppointmentDate DATETIME,
    Notes VARCHAR(200)
);

-- Insert sample data
INSERT INTO Doctors (FullName, Specialty, Availability) VALUES
('Dr. John Asante', 'Cardiologist', 1),
('Dr. Jane Boakye', 'Dermatologist', 1);

INSERT INTO Patients (FullName, Email) VALUES
('Dzifa Gomarshie', 'dzifagoma112e@mail.com'),
('Seidu Keita', 'keita5@mail.com');
