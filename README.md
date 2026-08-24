# TravelEaseDB — Travel Marketplace Schema and WinForms Client

SQL Server database **TravelEaseDB** for a multi-role travel platform (traveler, admin, tour operator, service provider), with seeded Pakistan-centric data, seven report query packs, a booking-status trigger, and a C# WinForms client that inserts bookings and opens cancellation/payment UI.

**Author:** Mohammad Rohaan · **Roll:** 22I-2327 · **GitHub:** [rohaan2802](https://github.com/rohaan2802) · **Course:** Database Final Project (Spring 2025)

---

## Table of contents

1. [Problem and context](#problem-and-context)
2. [Features](#features)
3. [Architecture](#architecture)
4. [Schema, keys, and procedures](#schema-keys-and-procedures)
5. [File-by-file reference](#file-by-file-reference)
6. [I/O formats](#io-formats)
7. [Tech stack](#tech-stack)
8. [Repository structure](#repository-structure)
9. [Build and run](#build-and-run)
10. [Usage](#usage)
11. [Constants](#constants)
12. [Results](#results)
13. [Limitations](#limitations)
14. [How to extend](#how-to-extend)
15. [Author](#author)

---

## Problem and context

TravelEase is a **booking marketplace**: travelers book trips sold by tour operators; hotels, guides, and transport are **service providers** assigned to trips; admins oversee users. The SQL script creates the database, CHECKs, FKs, demo `INSERT`s, analytical `SELECT`s (reports 1–7), and `CREATE TRIGGER UpdateBookingStatus`.

The WinForms app in this extract (`Form1.cs`, `Program.cs`) targets catalog **`Travelease`** on `DESKTOP-NO8G99V\SQLEXPRESS` — **not** the same name as `CREATE DATABASE TravelEaseDB`. Column names on `Form1` (`Bookings.BookingDate, TravelerID, Status, GroupSize, TotalAmount`) also **differ** from `schema.sql` table `BOOKINGS` (`BookDate, UserID, BookingStatus, TripID`). Treat Form1 as a **lab form** against a possibly evolved Excel/CSV mapping; align names before a joint demo.

The original GitHub tree is large: EERD PNGs, `*_50_Entries.xlsx`, CSVs, RDLC reports, role UIs (`AdminInterface`, `TravelerInterface`, `TourOperatorInterface`, `ServiceProviderInterface`), and a second solution `TravelEaseForm.sln`.

---

## Features

- Role-checked `AppUsers` (`Traveler`, `Admin`, `TourOperator`, `ServiceProvider`) with unique `(UserName, UserRole)`.
- Subtype tables sharing `UserID` PK/FK with `ON DELETE CASCADE`.
- `LOCATION` unique `(City, Country)`; hotels/guides/transport hang off `SERVICE_PROVIDER.LicenseNo`.
- Trips priced `DECIMAL(10,2)`, categorized, dated; bookings → tickets (15-day default expiry) → optional review (1–5).
- Cancellation + refund + payment (`Paid`/`Pending`/`Failed`).
- `TRIP_SERVICE_ASSIGNMENT` links trip, operator, and provider with status/response date.
- Seed: 10 categories, 10 Pakistani cities, 10 app users (IDENTITY 100,105,…), two trips, two bookings.
- Reports: revenue by category, cancellation rate, operator ratings, hotel bookings, abandonment, platform growth.
- Trigger: **after INSERT** on `BOOKINGS`, set `BookingStatus = 1` if it was 0 (auto-confirm).
- WinForms: validate traveler id / status / amount; parameterized `INSERT`; open `CancellationsForm` as modal.

---

## Architecture

```
SQL Server TravelEaseDB
        │  schema.sql  (tables → seed → reports → trigger)
        ▼
 AppUsers ─┬─ Traveler
           ├─ ADMIN
           ├─ TOUR_OPERATOR ── TRIP ── BOOKINGS ─┬─ TICKET
           └─ SERVICE_PROVIDER                   ├─ REVIEW
                 ├─ HOTEL                        ├─ CANCELLATION
                 ├─ GUIDE                        ├─ REFUND
                 └─ TRANSPORT                    └─ PAYMENT
 LOCATION ── SERVICE_PROVIDER
 CATEGORY ── TRIP
 TRIP + OPERATOR + PROVIDER ── TRIP_SERVICE_ASSIGNMENT

WinForms TravelEaseForm
        Program.Main → Application.Run(new Form1())
        Form1 → SqlConnection(connectionString) → INSERT Bookings
              → CancellationsForm.ShowDialog()
              → btnOpenPayment: MessageBox only
```

**IDENTITY steps:** `AppUsers` `(100,5)` so ids are 100,105,110,… matching the seed. `LOCATION` `(1000,1)`. `HOTEL`/`GUIDE`/`TRANSPORT` `(500,1)`. `TRIP` `(6000,1)`. `BOOKINGS` `(1276,1)`. `CATEGORY` `(201,5)` → 201,206,211,… (trip seed uses 201 and 206). `TRIP_SERVICE_ASSIGNMENT` `(100,1)`. `PAYMENT`/`REVIEW` `(1,1)`.

---

## Schema, keys, and procedures

There are **no stored procedures** in `schema.sql`. Logic is tables + CHECKs + one **trigger**.

### Tables (create order in file)

| Table | PK | Notable columns / CHECKs | FKs |
|-------|----|---------------------------|-----|
| `AppUsers` | `UserID IDENTITY(100,5)` | `UserName` 20, `UserPassword` 100, `ContactNumber` 11 digits, `Email LIKE '%@%.%'`, `UserRole` IN 4 roles, `UserStatus` default 0, `CreatedAt` default `GETDATE()`, UNIQUE `(UserName, UserRole)` | — |
| `Traveler` | `UserID` | `CNIC` UNIQUE, `TravelerName`, `Preference` | `AppUsers` CASCADE |
| `ADMIN` | `UserID` | `AdminName` | `AppUsers` CASCADE |
| `LOCATION` | `LocationID IDENTITY(1000,1)` | `City`/`Country` len≥2, UNIQUE pair | — |
| `SERVICE_PROVIDER` | `UserID` | `LicenseNo` UNIQUE len≥5, `ProviderName` len≥3, `ProviderType`, `LocationID` | users CASCADE, location CASCADE |
| `TOUR_OPERATOR` | `UserID` | `LicenseNo` UNIQUE, `CompanyName` UNIQUE | users CASCADE |
| `HOTEL` | `H_Id IDENTITY(500,1)` | `HotelName` len≥3, UNIQUE `(LicenseNo, HotelName)` | `SERVICE_PROVIDER(LicenseNo)` CASCADE |
| `GUIDE` | `G_Id IDENTITY(500,1)` | `Specialization`, `Language` len≥2, `LicenseNo` UNIQUE | license CASCADE |
| `TRANSPORT` | `T_Id IDENTITY(500,1)` | `VehicleType`, `Capacity > 0`, `LicenseNo` UNIQUE | license CASCADE |
| `TRIP` | `TripID IDENTITY(6000,1)` | `Title`, `Price >= 0`, `TripDate`, `CategoryID`, `TourOperatorID` | operator CASCADE, **CATEGORY** CASCADE |
| `CATEGORY` | `CategoryID IDENTITY(201,5)` | `CategoryName` UNIQUE | — |
| `BOOKINGS` | `BookingID IDENTITY(1276,1)` | `TripID`, `UserID`, `BookDate` default today, `BookingStatus` default 0 | trip CASCADE, `TRAVELER(UserID)` **no CASCADE** |
| `TICKET` | `BookingID` | `IssueDate` default today, `ExpiryDate` default +15 days, `ExpiryDate > IssueDate` | booking CASCADE |
| `REVIEW` | `ReviewID IDENTITY` | `BookingID` UNIQUE, `Rating` 1–5, `ApproveStatus` default 0 | booking CASCADE |
| `CANCELLATION` | `BookingID` | `TourOperatorID`, `CancelDate`, `Reason TEXT` | booking, operator CASCADE |
| `REFUND` | `BookingID` | `TourOperatorID`, `RefundDate`, `Amount` | booking, operator CASCADE |
| `TRIP_SERVICE_ASSIGNMENT` | `AssignmentID IDENTITY(100,1)` | `AssignedDate`, `AssignmentStatus` default 0, `ResponseDate` | trip, operator, provider CASCADE |
| `PAYMENT` | `PaymentID IDENTITY` | `Amount >= 0`, `PaymentStatus` IN (`Paid`,`Pending`,`Failed`) | booking, operator CASCADE |

**Script bug:** `CREATE TABLE TRIP` appears **before** `CREATE TABLE CATEGORY`, but `TRIP` references `CATEGORY(CategoryID)`. On a fresh instance this batch can fail; create `CATEGORY` first (or split batches). A later `UPDATE BOOKINGS SET BookingStatus = 1 WHERE BookingStatus = 0` then the trigger also forces new inserts to 1.

### Trigger `UpdateBookingStatus`

```sql
CREATE TRIGGER UpdateBookingStatus ON BOOKINGS
AFTER INSERT AS
BEGIN
    UPDATE BOOKINGS
    SET BookingStatus = 1
    WHERE BookingID IN (SELECT BookingID FROM inserted) AND BookingStatus = 0;
END;
```

So “pending” default 0 is immediately flipped to 1 on insert.

### Report packs (in `schema.sql`)

**Report 1 — bookings/revenue:** `COUNT` confirmed (`BookingStatus = 1`); `SUM(T.Price)` by `CATEGORY`; cancellation rate `COUNT(C)/COUNT(B)*100` (`LEFT JOIN CANCELLATION`); peak month `DATENAME(MONTH, BookDate)`; `AVG(T.Price)` confirmed.

**Report 2 — demographics:** nationality via `Traveler ⋈ SERVICE_PROVIDER ⋈ LOCATION` (this join is **semantically odd** — travelers are not providers); bookings by category; bookings by city/country through assignments; avg spend per `UserID`; overall avg price.

**Report 3 — operators:** `AVG(REVIEW.Rating)` per operator; `SUM(Price)` confirmed bookings; `AVG(DATEDIFF(MINUTE, AssignedDate, ResponseDate))` per `TOUR_OPERATOR_ID`.

**Report 4 — providers:** hotel booking counts via assignment chain; guide average rating.

**Report 5 — destinations:** bookings by city; monthly volume (`GROUP BY DATENAME` + `MONTH` for sort); avg satisfaction by city; yearly bookings by city.

**Report 6 — abandonment:** `%` of bookings with status 0; cancel reasons for status 0; recovery rate (same user+trip, later confirmed after abandoned); `SUM(Price)` potential loss on status 0.

**Report 7 — growth:** new users by `yyyy-MM` and role; active travelers/operators by booking month; new operators by join month; `FORMAT(LocationID, 'yyyy-MM')` as “new destinations” — **LocationID is INT**, so this format is not a date (likely a leftover bug).

---

## File-by-file reference

| File | Role |
|------|------|
| `schema.sql` | Full DDL, seed, reports 1–7, trigger (`TravelEaseDB_i222327.sql` on GitHub) |
| `Program.cs` | `[STAThread] Main` → `EnableVisualStyles`, `Run(new Form1())`, namespace `TravelEaseForm` |
| `Form1.cs` | Booking form: ComboBox status, DateTimePicker, parameterized INSERT, navigation buttons |
| `code_files.txt` | Inventory of `.cs` files in the original project (forms, designer, DataSet, signup UIs) |
| `TREE.txt` | Full GitHub tree: Excel/CSV seeds, EERD, zips, RDLC, `AdminInterface.cs`, etc. |

**GitHub WinForms (not in this extract):** `PaymentForm.cs`, `CancellationsForm.cs`, `TransportForm.cs`, plus `DB FinalProject/TravelEaseDB/` role dashboards, `SignINForm`, `*SignUP*`, `TravelEaseDBDataSet.xsd`, reports `Ticket.rdlc`, `PlatformGrowthReport.rdlc`, `AbandonedBookingAnalysisReport.rdlc`, etc.

**Form1 methods:**

- `Form1_Load`: `cmbStatus` ← `Confirmed`, `Pending`, `Cancelled`; `dtBookingDate = Now`; `txtBookingID.Enabled = false`.
- `btnSaveBooking_Click`: require traveler id, status, total; `decimal.TryParse` amount; `INSERT INTO Bookings (BookingDate, TravelerID, Status, GroupSize, TotalAmount) VALUES (@…)`.
- `btnOpenPayment_Click`: `MessageBox("Opening Payment Form")` — **does not** `new PaymentForm()`.
- `btnOpenCancellation_Click`: `new CancellationsForm().ShowDialog()`.

---

## I/O formats

### Connection string (`Form1.cs`)

```
Data Source=DESKTOP-NO8G99V\SQLEXPRESS;Initial Catalog=Travelease;Integrated Security=True;
```

Windows authentication; machine-specific instance name.

### Seed user example

```
UserName traveler1 / pass123 / 03001234567 / traveler1@example.com / Traveler
```

Contact CHECK: eleven `[0-9]` characters (Pakistan mobile style).

### CSV folder (TREE)

`ADMIN.csv`, `AppUsers.csv`, `BOOKINGS.csv`, `CANCELLATION.csv`, `CATEGORY.csv`, `GUIDE.csv`, `HOTEL.csv`, `LOCATION.csv`, `REFUND.csv`, `REVIEW.csv`, `SERVICE_PROVIDER.csv`, `TICKET.csv`, `TOUR_OPERATOR.csv`, `TRANSPORT.csv`, `TRIP.csv`, `TRIP_SERVICE_ASSIGNMENT.csv`, `Traveler.csv`.

Excel workbooks `*_50_Entries.xlsx` for ~50-row demos.

### Form insert parameters

| Parameter | Control |
|-----------|---------|
| `@BookingDate` | `dtBookingDate.Value.Date` |
| `@TravelerID` | `txtTravelerID.Text` (string) |
| `@Status` | `cmbStatus.Text` |
| `@GroupSize` | `numGroupSize.Value` |
| `@TotalAmount` | parsed decimal |

---

## Tech stack

T-SQL (SQL Server), C# WinForms, **.NET Framework 4.7.2** (`TravelEaseForm.csproj` artifacts), `System.Data.SqlClient`, Visual Studio, optional RDLC (`Microsoft.SqlServer.Types.dll`, VSIX in `New folder`).

---

## Repository structure

```
TravelEaseDB/
├── schema.sql
├── Form1.cs
├── Program.cs
├── code_files.txt
└── DB FinalProject/                          # GitHub
    ├── TravelEaseDB/                         # full role UI + RDLC
    └── DataBase FinalProject_ALL DATA/
        └── TravelEaseDB/
            ├── TravelEaseDB_i222327.sql
            ├── *_50_Entries.xlsx
            ├── New folder/*.csv
            └── TravelEaseForms/TravelEaseForm/{Form1,PaymentForm,CancellationsForm,TransportForm,Program.cs}
```

---

## Build and run

1. Install SQL Server Express + SSMS.
2. Open `schema.sql`. **Reorder** so `CATEGORY` (and `LOCATION`) exist before `TRIP` / `SERVICE_PROVIDER` if the script errors.
3. Execute. Confirm:

```sql
USE TravelEaseDB;
SELECT * FROM AppUsers;
SELECT * FROM TRIP;
SELECT * FROM BOOKINGS;
```

4. Open `TravelEaseForm.sln` (GitHub path) or compile this `Form1`/`Program.cs` in a WinForms project targeting 4.7.2.
5. Set `connectionString` to your instance and catalog (`TravelEaseDB` vs `Travelease`).
6. Map `INSERT` columns to the real table if you use `schema.sql` as-is (`TripID`, `UserID`, `BookDate`, `BookingStatus`).

Import CSVs with SSMS Import Flat File if you skip the script’s small `INSERT`s.

---

## Usage

- Run Form1 → fill traveler id (must exist as `Traveler.UserID` if FKs apply), status, group size, total → Save Booking.
- Cancellation button opens `CancellationsForm` (source on GitHub).
- Payment button is a stub MessageBox.
- For admin/operator workflows use `AdminInterface.cs` / `TourOperatorInterface.cs` in the larger project; reports bind to RDLC using the SELECT packs in `schema.sql`.

---

## Constants

| Item | Value |
|------|--------|
| `AppUsers` identity | start 100, increment 5 |
| Phone | 11 digits |
| Ticket expiry | `DATEADD(day, 15, GETDATE())` |
| Review rating | 1–5 |
| Payment statuses | Paid, Pending, Failed |
| Seed passwords | `pass123`, `adminpass`, `oppass`, `provpass` |
| Sample trip prices | 15000.00 Northern Adventure; 8000.00 Cultural Lahore |
| Framework | .NET 4.7.2 |

---

## Results

Script includes a post-create `UPDATE` that confirms all pending bookings, then seed rows (booking 1276 confirmed, 1277 pending — but trigger/update may both set 1). Report queries are meant to be executed after a richer 50-row load from Excel. EERD: `TravelEaseDB_EERD.drawio.png`, `i222327_FinalProject_EERD.drawio.png`. Written reports: `TravelEase Project Report.pdf`, `Assumptions.docx`.

---

## Limitations

- `TRIP` created before `CATEGORY` — deploy order hazard.
- Form1 catalog/columns **do not match** `schema.sql` `BOOKINGS`.
- Machine-specific `Data Source=DESKTOP-NO8G99V\SQLEXPRESS`.
- Seed puts a GUIDE on hotel license `LIC-SP-003` and TRANSPORT on the same license — unique `LicenseNo` on GUIDE/TRANSPORT can conflict (`GUIDE` UNIQUE LicenseNo vs `TRANSPORT` UNIQUE LicenseNo sharing `LIC-SP-003`).
- Report 2 “nationality” joins travelers to service providers by `UserID` (disjoint roles).
- Report 7 destination month uses `FORMAT(LocationID, 'yyyy-MM')`.
- Passwords stored in plaintext VARCHAR.
- No stored procedures / transactions around booking+payment+ticket.
- `ON DELETE CASCADE` from `AppUsers` can wipe large subgraphs.

---

## How to extend

- Unify Form1 with `BOOKINGS` DDL; store connection in `App.config`.
- Wrap booking + ticket + payment in a stored procedure with `BEGIN TRAN`.
- Hash passwords; enforce `UserStatus` before login in `SignINForm`.
- Fix CATEGORY/TRIP create order; add `TripStops` / `Offers` if present in Excel but missing from this SQL (TREE has `TripStops_50_Entries.xlsx`, `Offers_50_Entries.xlsx` — extra entities not in this `schema.sql`).

---

## Author

**Mohammad Rohaan**  
Student ID: **22I-2327**  
GitHub: **rohaan2802**  
Database final project — TravelEase.

Do not commit production connection strings. SQL Server student edition / Express is sufficient.
