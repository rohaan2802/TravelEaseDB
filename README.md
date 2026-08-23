# TravelEaseDB

End-to-end **travel booking database system**: SQL Server schema (`TravelEaseDB`), EERD diagrams, seeded CSV/XLSX data, and **C# WinForms** clients for travelers, admins, tour operators, and service providers (bookings, payments, cancellations, transport).

**Course:** Database Final Project · **Roll referenced:** i222327

---

## Overview

TravelEase models a multi-role travel marketplace:

- **AppUsers** with roles: Traveler, Admin, TourOperator, ServiceProvider
- Domain entities: trips, hotels, guides, transport, tickets, bookings, payments, refunds, reviews, cancellations, offers, locations, categories
- Desktop UI for sign-in/sign-up per role and operational forms (booking, payment, cancellation, transport)
- Sample datasets (~50-row Excel workbooks + CSV folder) for demos and grading

---

## Features

- Rich T-SQL schema with IDENTITY keys, CHECKs, and cascading FKs (`TravelEaseDB_i222327.sql`)
- EERD exports (`TravelEaseDB_EERD.drawio.png`, `i222327_FinalProject_EERD.drawio.png`)
- WinForms solution under `TravelEaseForms/TravelEaseForm` (Form1 bookings, PaymentForm, CancellationsForm, TransportForm)
- Broader UI project under `DB FinalProject/TravelEaseDB/` (Admin/Traveler/TourOperator/ServiceProvider interfaces + sign-up flows)
- Project report, rubric, and assumptions documents

---

## Repository structure

```text
TravelEaseDB/
└── DB FinalProject/
    ├── TravelEaseDB/                    # fuller WinForms role UIs + DataSet
    └── DataBase FinalProject_ALL DATA/
        ├── New folder/                  # rubrics, reports, EERD PNG, zips, VSIX helpers
        └── TravelEaseDB/
            ├── TravelEaseDB_i222327.sql
            ├── TravelEaseDB_EERD.drawio.png
            ├── *_50_Entries.xlsx
            ├── New folder/*.csv         # ADMIN, BOOKINGS, TRIP, HOTEL, ...
            ├── Assumptions.docx
            └── TravelEaseForms/TravelEaseForm/
                ├── TravelEaseForm.sln
                ├── Program.cs
                ├── Form1.cs             # bookings
                ├── PaymentForm.cs
                ├── CancellationsForm.cs
                └── TransportForm.cs
```

---

## Build / run

### 1. Database

1. Install SQL Server Express + SSMS.
2. Run `TravelEaseDB_i222327.sql` to create `TravelEaseDB` and tables.
3. Import CSVs/XLSX (SSMS Import Flat File / wizard) or scripts matched to your paths.
4. Confirm with `SELECT` smoke tests on `AppUsers`, `TRIP`, `BOOKINGS`.

### 2. WinForms (TravelEaseForm)

1. Open `TravelEaseForms/TravelEaseForm/TravelEaseForm.sln` in Visual Studio.
2. Target **.NET Framework 4.7.2** (as in project artifacts).
3. Update the connection string in forms (example from `Form1.cs`):

```csharp
Data Source=YOUR_HOST\\SQLEXPRESS;Initial Catalog=Travelease;Integrated Security=True;
```

Align catalog name with your SQL script (`TravelEaseDB` vs `Travelease`).

4. Build & run (`Program.cs` starts `Form1`).

Optional: install RDLC/reporting VSIX packages from `New folder/` if you use the report designer tooling bundled there.

---

## Usage

- **Bookings (`Form1`):** enter traveler ID, status, group size, total; save via parameterized `INSERT`.
- Open **Payment**, **Cancellation**, and **Transport** forms from the booking UI navigation.
- Use role-specific projects under `DB FinalProject/TravelEaseDB/` for admin approvals and operator workflows.
- Seed data Excel/CSV files illustrate cardinality for demos and screenshots.

---

## Extending

- Centralize connection strings in `App.config` / user secrets (avoid machine-specific `DESKTOP-...` sources in commits).
- Add stored procedures for booking+payment transactions.
- Enforce role-based UI enablement from `AppUsers.UserRole`.
- Generate typed DataSet refresh after schema changes.

---

## License

University final-project materials - do not publish real user credentials or production connection strings.
