-- Create a new database named 'TravelEaseDB'
CREATE DATABASE TravelEaseDB;
GO  -- 'GO' is a batch separator used in SQL Server Management Studio (SSMS)

-- Switch the context to the newly created database
USE TravelEaseDB;
GO

-- USER Table
CREATE TABLE AppUsers
(
    UserID INT IDENTITY(100,5) PRIMARY KEY,
    UserName VARCHAR(20) NOT NULL CHECK (LEN(LTRIM(RTRIM(UserName))) > 0),
    UserPassword VARCHAR(100) NOT NULL CHECK (LEN(LTRIM(RTRIM(UserPassword))) > 0),
    ContactNumber VARCHAR(11) NOT NULL CHECK (ContactNumber LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' AND LEN(ContactNumber) = 11),
    Email VARCHAR(50) NOT NULL CHECK (Email LIKE '%@%.%'),
    UserRole VARCHAR(20) NOT NULL CHECK (UserRole IN ('Traveler', 'Admin', 'TourOperator', 'ServiceProvider')),
    CreatedAt DATETIME DEFAULT GETDATE() NOT NULL,
    CONSTRAINT UK_UserName_UserRole UNIQUE (UserName, UserRole),
	UserStatus INT DEFAULT 0
);
GO



-- TRAVELER Table
CREATE TABLE Traveler 
(
    UserID INT PRIMARY KEY,  
	CNIC VARCHAR(20) NOT NULL UNIQUE,
    TravelerName VARCHAR(25) NOT NULL CHECK (LEN(LTRIM(RTRIM(TravelerName))) > 0),
    Preference VARCHAR(100) NOT NULL,
    CONSTRAINT FK_Traveler_User FOREIGN KEY (UserID) REFERENCES AppUsers(UserID) ON DELETE CASCADE
);
GO


-- ADMIN Table
CREATE TABLE ADMIN
(
    UserID INT PRIMARY KEY FOREIGN KEY REFERENCES AppUsers(UserID) ON DELETE CASCADE,
	AdminName VARCHAR(25) NOT NULL CHECK (LEN(LTRIM(RTRIM(AdminName))) > 0)
);
GO

--LOCATION Table
CREATE TABLE LOCATION 
(
    LocationID INT IDENTITY(1000,1) PRIMARY KEY,
    City VARCHAR(100) NOT NULL CHECK (LEN(City) >= 2),
    Country VARCHAR(100) NOT NULL CHECK (LEN(Country) >= 2),
    Description TEXT NULL,
    CONSTRAINT UQ_City_Country UNIQUE (City, Country)
);
GO


-- SERVICE_PROVIDER Table
CREATE TABLE SERVICE_PROVIDER 
(
    LicenseNo VARCHAR(30) UNIQUE CHECK (LEN(LicenseNo) >= 5),
    ProviderName VARCHAR(25) NOT NULL CHECK (LEN(ProviderName) >= 3),
    ProviderType VARCHAR(50) NOT NULL,
    UserID INT PRIMARY KEY,
    LocationID INT NOT NULL,
    FOREIGN KEY (UserID) REFERENCES AppUsers(UserID) ON DELETE CASCADE,
    FOREIGN KEY (LocationID) REFERENCES LOCATION(LocationID) ON DELETE CASCADE
);
GO

-- TOUR_OPERATOR
CREATE TABLE TOUR_OPERATOR 
(
	UserID INT PRIMARY KEY,
	FOREIGN KEY (UserID) REFERENCES AppUsers(UserID) ON DELETE CASCADE,
    LicenseNo VARCHAR(30) UNIQUE NOT NULL,
    CompanyName VARCHAR(100) UNIQUE NOT NULL
);
GO

-- HOTEL Table
CREATE TABLE HOTEL 
(
    H_Id INT IDENTITY(500,1) PRIMARY KEY,
    HotelName VARCHAR(25) NOT NULL CHECK (LEN(HotelName) >= 3),
    LicenseNo VARCHAR(30) NOT NULL,
    CONSTRAINT UQ_License_Hotel UNIQUE (LicenseNo, HotelName),
    FOREIGN KEY (LicenseNo) REFERENCES SERVICE_PROVIDER(LicenseNo) ON DELETE CASCADE
);

GO

-- GUIDE Table
CREATE TABLE GUIDE 
(
    G_Id INT IDENTITY(500,1) PRIMARY KEY,
    Specialization VARCHAR(100) NOT NULL,
	Language VARCHAR(500) CHECK (LEN(Language) >= 2) NOT NULL,
    LicenseNo VARCHAR(30) NOT NULL UNIQUE,
    FOREIGN KEY (LicenseNo) REFERENCES SERVICE_PROVIDER(LicenseNo) ON DELETE CASCADE
);
GO

-- TRANSPORT Table
CREATE TABLE TRANSPORT 
(
    T_Id INT IDENTITY(500,1) PRIMARY KEY,
	VehicleType VARCHAR(50) NOT NULL,
    Capacity INT NOT NULL CHECK (Capacity > 0),
    LicenseNo VARCHAR(30) NOT NULL UNIQUE,
    FOREIGN KEY (LicenseNo) REFERENCES SERVICE_PROVIDER(LicenseNo) ON DELETE CASCADE
);
GO


-- TRIP Table
CREATE TABLE TRIP 
(
    TripID INT IDENTITY(6000,1) PRIMARY KEY,
    Title VARCHAR(100) NOT NULL CHECK (LEN(LTRIM(RTRIM(Title))) > 0),
    Price DECIMAL(10,2) NOT NULL CHECK (Price >= 0),
    TripDate DATE NOT NULL,
    CategoryID INT NOT NULL,
	TourOperatorID INT NOT NULL,
	FOREIGN KEY (TourOperatorID) REFERENCES TOUR_OPERATOR(UserID) ON DELETE CASCADE,
    FOREIGN KEY (CategoryID) REFERENCES CATEGORY(CategoryID) ON DELETE CASCADE
);
GO


-- CATEGORY Table
CREATE TABLE CATEGORY 
(
    CategoryID INT IDENTITY(201,5) PRIMARY KEY,
    CategoryName VARCHAR(100) UNIQUE NOT NULL CHECK (LEN(LTRIM(RTRIM(CategoryName))) > 0),
);
GO

-- BOOKINGS Table
CREATE TABLE BOOKINGS 
(
    BookingID INT IDENTITY (1276,1) PRIMARY KEY,
    TripID INT NOT NULL,
    UserID INT NOT NULL,
    BookDate DATE NOT NULL DEFAULT GETDATE(),
    BookingStatus INT DEFAULT 0,
    FOREIGN KEY (TripID) REFERENCES TRIP(TripID) ON DELETE CASCADE,
    FOREIGN KEY (UserID) REFERENCES TRAVELER(UserID)
);
GO
-- This will work if there's only one default constraint on the column
-- Update all pending bookings (status 0) to confirmed (status 1)
UPDATE BOOKINGS 
SET BookingStatus = 1 
WHERE BookingStatus = 0;

-- TICKET Table
CREATE TABLE TICKET 
(
    BookingID INT PRIMARY KEY,
    IssueDate DATE NOT NULL DEFAULT GETDATE(),
    ExpiryDate DATE NOT NULL DEFAULT DATEADD(day, 15, GETDATE()),
    FOREIGN KEY (BookingID) REFERENCES BOOKINGS(BookingID) ON DELETE CASCADE,
    CHECK (ExpiryDate > IssueDate)
);
GO

-- REVIEW
CREATE TABLE REVIEW 
(
    ReviewID INT IDENTITY(1,1) PRIMARY KEY,
    BookingID INT UNIQUE, -- One review per booking
    Rating INT CHECK(Rating BETWEEN 1 AND 5),
    Comment TEXT,
    ReviewDate DATE DEFAULT GETDATE(),
    ApproveStatus INT DEFAULT 0,
    FOREIGN KEY (BookingID) REFERENCES BOOKINGS(BookingID) ON DELETE CASCADE
);
GO

-- CANCELLATION
CREATE TABLE CANCELLATION 
(
    BookingID  INT PRIMARY KEY,
	FOREIGN KEY (BookingID) REFERENCES BOOKINGS(BookingID),
	TourOperatorID INT NOT NULL,
	FOREIGN KEY (TourOperatorID) REFERENCES TOUR_OPERATOR(UserID) ON DELETE CASCADE,
    CancelDate DATE,
    Reason TEXT,
);
GO

-- REFUND
CREATE TABLE REFUND 
(
    BookingID  INT PRIMARY KEY,
	FOREIGN KEY (BookingID) REFERENCES BOOKINGS(BookingID),
	TourOperatorID INT NOT NULL,
	FOREIGN KEY (TourOperatorID) REFERENCES TOUR_OPERATOR(UserID) ON DELETE CASCADE,
    RefundDate DATE ,
    Amount DECIMAL(10,2)
);
GO

-- TRIP_SERVICE_ASSIGNMENT
CREATE TABLE TRIP_SERVICE_ASSIGNMENT 
(
    AssignmentID INT IDENTITY(100,1) PRIMARY KEY,
    TripID INT FOREIGN KEY REFERENCES TRIP(TripID) ,
    TOUR_OPERATOR_ID INT FOREIGN KEY REFERENCES TOUR_OPERATOR(UserID),
    SERVICE_PROVIDER_ID INT FOREIGN KEY REFERENCES SERVICE_PROVIDER(UserID) ON DELETE CASCADE,
    AssignedDate DATE,
    AssignmentStatus INT DEFAULT 0,
    ResponseDate DATE
);
GO

CREATE TABLE PAYMENT
(
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    BookingID INT NOT NULL,
    TourOperatorID INT NOT NULL,
    PaymentDate DATE DEFAULT GETDATE(),
    Amount DECIMAL(10,2) NOT NULL CHECK (Amount >= 0),
    PaymentStatus VARCHAR(20) NOT NULL CHECK (PaymentStatus IN ('Paid', 'Pending', 'Failed')),
    FOREIGN KEY (BookingID) REFERENCES BOOKINGS(BookingID),
    FOREIGN KEY (TourOperatorID) REFERENCES TOUR_OPERATOR(UserID) ON DELETE CASCADE
);



INSERT INTO CATEGORY (CategoryName) VALUES
('Adventure'),
('Cultural'),
('Leisure'),
('Wildlife'),
('Historical'),
('Beach'),
('Mountain'),
('Desert'),
('City Tour'),
('Cruise');


INSERT INTO LOCATION (City, Country, Description) VALUES
('Islamabad', 'Pakistan', 'Capital city of Pakistan'),
('Lahore', 'Pakistan', 'Cultural heart of Pakistan'),
('Karachi', 'Pakistan', 'Largest city in Pakistan'),
('Peshawar', 'Pakistan', 'Historic city in Khyber Pakhtunkhwa'),
('Quetta', 'Pakistan', 'Capital of Balochistan'),
('Multan', 'Pakistan', 'City of Saints'),
('Faisalabad', 'Pakistan', 'Industrial city'),
('Rawalpindi', 'Pakistan', 'Twin city of Islamabad'),
('Murree', 'Pakistan', 'Famous hill station'),
('Skardu', 'Pakistan', 'Gateway to the Karakoram');



INSERT INTO AppUsers (UserName, UserPassword, ContactNumber, Email, UserRole) VALUES
('traveler1', 'pass123', '03001234567', 'traveler1@example.com', 'Traveler'),
('traveler2', 'pass123', '03011234567', 'traveler2@example.com', 'Traveler'),
('traveler3', 'pass123', '03021234567', 'traveler3@example.com', 'Traveler'),
('admin1', 'adminpass', '03101234567', 'admin1@example.com', 'Admin'),
('admin2', 'adminpass', '03111234567', 'admin2@example.com', 'Admin'),
('operator1', 'oppass', '03201234567', 'operator1@example.com', 'TourOperator'),
('operator2', 'oppass', '03211234567', 'operator2@example.com', 'TourOperator'),
('provider1', 'provpass', '03301234567', 'provider1@example.com', 'ServiceProvider'),
('provider2', 'provpass', '03311234567', 'provider2@example.com', 'ServiceProvider'),
('provider3', 'provpass', '03321234567', 'provider3@example.com', 'ServiceProvider');


INSERT INTO Traveler (UserID, CNIC, TravelerName, Preference) VALUES
(100, '61101-1234567-1', 'Ali Khan', 'Adventure'),
(105, '61101-1234567-2', 'Sara Ahmed', 'Cultural'),
(110, '61101-1234567-3', 'Usman Tariq', 'Leisure');

INSERT INTO ADMIN (UserID, AdminName) VALUES
(115, 'Admin One'),
(120, 'Admin Two');


INSERT INTO TOUR_OPERATOR (UserID, LicenseNo, CompanyName) VALUES
(125, 'LIC-OP-001', 'Explore Pakistan'),
(130, 'LIC-OP-002', 'Heritage Tours');



INSERT INTO SERVICE_PROVIDER (LicenseNo, ProviderName, ProviderType, UserID, LocationID) VALUES
('LIC-SP-001', 'Comfort Travels', 'Transport', 135, 1000),
('LIC-SP-002', 'Mountain Guides', 'Guide', 140, 1001),
('LIC-SP-003', 'City Hotels', 'Hotel', 145, 1002);


INSERT INTO HOTEL (HotelName, LicenseNo) VALUES
('Serene Inn', 'LIC-SP-003'),
('Hilltop Resort', 'LIC-SP-003');


INSERT INTO GUIDE (Specialization, Language, LicenseNo) VALUES
('Mountain Trekking', 'English, Urdu', 'LIC-SP-002'),
('Historical Sites', 'English, Punjabi', 'LIC-SP-003');

INSERT INTO TRANSPORT (VehicleType, Capacity, LicenseNo) VALUES
('Van', 12, 'LIC-SP-001'),
('Bus', 40, 'LIC-SP-003');

INSERT INTO TRIP (Title, Price, TripDate, CategoryID, TourOperatorID) VALUES
('Northern Adventure', 15000.00, '2025-06-15', 201, 125),
('Cultural Lahore', 8000.00, '2025-07-10', 206, 130);

INSERT INTO BOOKINGS (TripID, UserID, BookDate, BookingStatus) VALUES
(6000, 100, '2025-05-01', 1),
(6001, 105, '2025-05-02', 0);

INSERT INTO TICKET (BookingID, IssueDate, ExpiryDate) VALUES
(1276, '2025-05-01', '2025-05-16'),
(1277, '2025-05-02', '2025-05-17');


INSERT INTO REVIEW (BookingID, Rating, Comment, ReviewDate, ApproveStatus) VALUES
(1276, 5, 'Amazing experience!', '2025-05-20', 1),
(1277, 4, 'Very good service.', '2025-05-21', 0);

INSERT INTO CANCELLATION (BookingID, TourOperatorID, CancelDate, Reason) VALUES
(1276, 125, '2025-05-05', 'Personal reasons'),
(1277, 130, '2025-05-06', 'Health issues');

INSERT INTO REFUND (BookingID, TourOperatorID, RefundDate, Amount) VALUES
(1276, 125, '2025-05-10', 5000.00),
(1277, 130, '2025-05-11', 3000.00);

INSERT INTO TRIP_SERVICE_ASSIGNMENT (TripID, TOUR_OPERATOR_ID, SERVICE_PROVIDER_ID, AssignedDate, AssignmentStatus, ResponseDate) VALUES
(6000, 125, 135, '2025-05-01', 1, '2025-05-02'),
(6001, 130, 140, '2025-05-03', 0, NULL);



-- REPORT 1

-- Total Bookings
SELECT COUNT(*) AS TotalBookings FROM BOOKINGS WHERE BookingStatus = 1;

-- Revenue by Category
SELECT C.CategoryName, SUM(T.Price) AS TotalRevenue
FROM BOOKINGS B
JOIN TRIP T ON B.TripID = T.TripID
JOIN CATEGORY C ON T.CategoryID = C.CategoryID
WHERE B.BookingStatus = 1
GROUP BY C.CategoryName;

-- Cancellation Rate
SELECT 
    CAST(COUNT(C.BookingID) AS FLOAT) / NULLIF(COUNT(B.BookingID), 0) * 100 AS CancellationRate
FROM BOOKINGS B
LEFT JOIN CANCELLATION C ON B.BookingID = C.BookingID;

-- Peak Booking Periods
SELECT DATENAME(MONTH, BookDate) AS Month, COUNT(*) AS Bookings
FROM BOOKINGS
WHERE BookingStatus = 1
GROUP BY DATENAME(MONTH, BookDate)
ORDER BY Bookings DESC;

-- Average Booking Value
SELECT 
    AVG(T.Price) AS AverageBookingValue
FROM BOOKINGS B
JOIN TRIP T ON B.TripID = T.TripID
WHERE B.BookingStatus = 1;



-- REPORT2

-- Nationality Distribution (joining with Location table)
SELECT 
    L.Country AS Nationality,
    COUNT(*) AS TravelerCount
FROM Traveler T
JOIN SERVICE_PROVIDER SP ON T.UserID = SP.UserID
JOIN LOCATION L ON SP.LocationID = L.LocationID
GROUP BY L.Country
ORDER BY TravelerCount DESC;


SELECT 
    C.CategoryName,
    COUNT(*) AS BookingCount
FROM BOOKINGS B
JOIN TRIP T ON B.TripID = T.TripID
JOIN CATEGORY C ON T.CategoryID = C.CategoryID
GROUP BY C.CategoryName
ORDER BY BookingCount DESC;


SELECT 
    L.City,
    L.Country,
    COUNT(*) AS BookingCount
FROM BOOKINGS B
JOIN TRIP T ON B.TripID = T.TripID
JOIN TRIP_SERVICE_ASSIGNMENT TSA ON T.TripID = TSA.TripID
JOIN SERVICE_PROVIDER SP ON TSA.SERVICE_PROVIDER_ID = SP.UserID
JOIN LOCATION L ON SP.LocationID = L.LocationID
GROUP BY L.City, L.Country
ORDER BY BookingCount DESC;


SELECT 
    B.UserID,
    AVG(T.Price) AS AvgSpending
FROM BOOKINGS B
JOIN TRIP T ON B.TripID = T.TripID
GROUP BY B.UserID
ORDER BY AvgSpending DESC;


SELECT 
    AVG(T.Price) AS OverallAvgSpending
FROM BOOKINGS B
JOIN TRIP T ON B.TripID = T.TripID;


--REPORT 3

SELECT 
    TOU.UserID AS TourOperatorID,
    AVG(R.Rating) AS AverageRating
FROM TOUR_OPERATOR TOU
JOIN TRIP T ON TOU.UserID = T.TourOperatorID
JOIN BOOKINGS B ON T.TripID = B.TripID
JOIN REVIEW R ON R.BookingID = B.BookingID
GROUP BY TOU.UserID
ORDER BY AverageRating DESC;



SELECT 
    TOU.UserID AS TourOperatorID,
    SUM(T.Price) AS TotalRevenue
FROM TOUR_OPERATOR TOU
JOIN TRIP T ON TOU.UserID = T.TourOperatorID
JOIN BOOKINGS B ON T.TripID = B.TripID
WHERE B.BookingStatus = 1  -- Assuming 1 = confirmed
GROUP BY TOU.UserID
ORDER BY TotalRevenue DESC;


SELECT 
    TOUR_OPERATOR_ID,
    AVG(DATEDIFF(MINUTE, AssignedDate, ResponseDate)) AS AverageResponseTime_Minutes
FROM TRIP_SERVICE_ASSIGNMENT
WHERE ResponseDate IS NOT NULL
GROUP BY TOUR_OPERATOR_ID
ORDER BY AverageResponseTime_Minutes;


--REPORT 4
SELECT 
    H.HotelName,
    COUNT(B.BookingID) AS TotalBookings
FROM HOTEL H
JOIN SERVICE_PROVIDER SP ON H.LicenseNo = SP.LicenseNo
JOIN TRIP_SERVICE_ASSIGNMENT TSA ON TSA.SERVICE_PROVIDER_ID = SP.UserID
JOIN TRIP T ON T.TripID = TSA.TripID
JOIN BOOKINGS B ON B.TripID = T.TripID
GROUP BY H.HotelName
ORDER BY TotalBookings DESC;


SELECT 
    G.G_Id,
    G.LicenseNo,
    AVG(R.Rating) AS AverageRating
FROM GUIDE G
JOIN SERVICE_PROVIDER SP ON G.LicenseNo = SP.LicenseNo
JOIN TRIP_SERVICE_ASSIGNMENT TSA ON TSA.SERVICE_PROVIDER_ID = SP.UserID
JOIN TRIP T ON T.TripID = TSA.TripID
JOIN BOOKINGS B ON B.TripID = T.TripID
JOIN REVIEW R ON R.BookingID = B.BookingID
GROUP BY G.G_Id, G.LicenseNo
ORDER BY AverageRating DESC;



--REPORT 5

SELECT 
    L.City,
    L.Country,
    COUNT(B.BookingID) AS TotalBookings
FROM BOOKINGS B
JOIN TRIP T ON B.TripID = T.TripID
JOIN TRIP_SERVICE_ASSIGNMENT TSA ON T.TripID = TSA.TripID
JOIN SERVICE_PROVIDER SP ON TSA.SERVICE_PROVIDER_ID = SP.UserID
JOIN LOCATION L ON SP.LocationID = L.LocationID
GROUP BY L.City, L.Country
ORDER BY TotalBookings DESC;



SELECT 
    DATENAME(MONTH, B.BookDate) AS BookingMonth,
    COUNT(*) AS TotalBookings
FROM BOOKINGS B
GROUP BY DATENAME(MONTH, B.BookDate), MONTH(B.BookDate)
ORDER BY MONTH(B.BookDate);


SELECT 
    L.City,
    L.Country,
    AVG(R.Rating) AS AverageSatisfactionScore
FROM REVIEW R
JOIN BOOKINGS B ON R.BookingID = B.BookingID
JOIN TRIP T ON B.TripID = T.TripID
JOIN TRIP_SERVICE_ASSIGNMENT TSA ON T.TripID = TSA.TripID
JOIN SERVICE_PROVIDER SP ON TSA.SERVICE_PROVIDER_ID = SP.UserID
JOIN LOCATION L ON SP.LocationID = L.LocationID
GROUP BY L.City, L.Country
ORDER BY AverageSatisfactionScore DESC;


SELECT 
    L.City,
    L.Country,
    YEAR(B.BookDate) AS BookingYear,
    COUNT(*) AS TotalBookings
FROM BOOKINGS B
JOIN TRIP T ON B.TripID = T.TripID
JOIN TRIP_SERVICE_ASSIGNMENT TSA ON T.TripID = TSA.TripID
JOIN SERVICE_PROVIDER SP ON TSA.SERVICE_PROVIDER_ID = SP.UserID
JOIN LOCATION L ON SP.LocationID = L.LocationID
GROUP BY L.City, L.Country, YEAR(B.BookDate)
ORDER BY L.City, BookingYear;



--REPORT 6

SELECT 
    (CAST(SUM(CASE WHEN BookingStatus = 0 THEN 1 ELSE 0 END) AS FLOAT) / COUNT(*)) * 100 AS AbandonmentRate
FROM BOOKINGS;



SELECT 
    CAST(Reason AS NVARCHAR(MAX)) AS Reason,
    COUNT(*) AS Count
FROM CANCELLATION C
JOIN BOOKINGS B ON C.BookingID = B.BookingID
WHERE B.BookingStatus = 0
GROUP BY CAST(Reason AS NVARCHAR(MAX))
ORDER BY Count DESC;




SELECT 
    (CAST(COUNT(DISTINCT Recovered.BookingID) AS FLOAT) / NULLIF(COUNT(DISTINCT Abandoned.BookingID), 0)) * 100 AS RecoveryRate
FROM BOOKINGS Abandoned
JOIN BOOKINGS Recovered 
    ON Abandoned.UserID = Recovered.UserID 
    AND Abandoned.TripID = Recovered.TripID
    AND Abandoned.BookingStatus = 0
    AND Recovered.BookingStatus = 1
    AND Abandoned.BookingID < Recovered.BookingID;



	SELECT 
    SUM(T.Price) AS PotentialRevenueLoss
FROM BOOKINGS B
JOIN TRIP T ON B.TripID = T.TripID
WHERE B.BookingStatus = 0;



--REPORT 7

SELECT 
    FORMAT(CreatedAt, 'yyyy-MM') AS RegistrationMonth,
    UserRole,
    COUNT(*) AS NewUsers
FROM AppUsers
GROUP BY FORMAT(CreatedAt, 'yyyy-MM'), UserRole
ORDER BY RegistrationMonth;


SELECT 
    FORMAT(BookDate, 'yyyy-MM') AS ActiveMonth,
    AU.UserRole,
    COUNT(DISTINCT AU.UserID) AS ActiveUsers
FROM BOOKINGS B
JOIN AppUsers AU ON AU.UserID = B.UserID
WHERE AU.UserRole IN ('Traveler', 'TourOperator')
GROUP BY FORMAT(BookDate, 'yyyy-MM'), AU.UserRole
ORDER BY ActiveMonth;



-- Tour Operators
SELECT 
    FORMAT(AU.CreatedAt, 'yyyy-MM') AS JoinMonth,
    COUNT(*) AS NewTourOperators
FROM TOUR_OPERATOR TOU
JOIN AppUsers AU ON AU.UserID = TOU.UserID
GROUP BY FORMAT(AU.CreatedAt, 'yyyy-MM');


SELECT 
    FORMAT(LocationID, 'yyyy-MM') AS AddedMonth,
    COUNT(*) AS NewDestinations
FROM LOCATION
GROUP BY FORMAT(LocationID, 'yyyy-MM')
ORDER BY AddedMonth;


CREATE TRIGGER UpdateBookingStatus
ON BOOKINGS
AFTER INSERT
AS
BEGIN
    UPDATE BOOKINGS
    SET BookingStatus = 1
    WHERE BookingID IN (SELECT BookingID FROM inserted) AND BookingStatus = 0;
END;