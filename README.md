# Group 7_Powerpuff_SDG_Project
# Health Tracker System

## Overview
The Health Tracker System is a VB.NET Windows Forms Application designed to help users monitor and manage their health information digitally. The system allows users to register accounts, calculate BMI, record daily health activities, monitor symptoms, and review health history records.

This project supports Sustainable Development Goal 3 (SDG 3) – Good Health and Well-Being by promoting health awareness and organized digital health monitoring.

---

# Developers

Group 7 – Powerpuff

Members:
- Arcales, Lad Anthonyel D.
- De Guia, Christine Jade M.
- Diaz, Jereyme James B.
- Kismundo, Kimberly A.
- Marasigan, Mark Jethro M.

Instructor:
- Neypes, Justin Louise R.

---

# Technologies Used

| Technology | Purpose |
|---|---|
| VB.NET | Application Development |
| Windows Forms | User Interface |
| ADO.NET SqlClient | Database Connectivity |
| SQL Server LocalDB | Database Storage |
| Visual Studio Community | Development Environment |

---

# System Features

## User Registration
Allows users to create a personal health tracking account.

Features:
- Full name input
- Username and password creation
- Account validation
- Database storage

---

## User Login
Provides secure access for registered users.

Features:
- Username and password authentication
- Database validation
- Dashboard access

---

## BMI Calculator
Calculates the user's Body Mass Index using height and weight.

BMI Formula:

:contentReference[oaicite:0]{index=0}

Features:
- Height and weight input
- BMI calculation
- BMI category result
- Record saving

---

## Daily Assessment
Allows users to record daily wellness activities.

Features:
- Cups of water tracking
- Hours of sleep tracking
- Active minutes recording
- Assessment saving

---

## Symptoms Monitoring
Allows users to monitor symptoms related to health conditions.

Features:
- Symptom checking
- Dropdown-based symptom input
- Instant symptom result
- Health monitoring support

---

## Health History
Displays previous health records and activities.

Features:
- BMI history
- Symptoms history
- Daily assessment history
- Search and refresh functionality

---

# Database

The system uses SQL Server LocalDB connected through ADO.NET SqlClient.

Database Files:
- HealthTrackerDataBase.mdf
- HealthTrackerDataBase.ldf

The database stores:
- User accounts
- BMI records
- Daily assessments
- Symptoms information
- Health history

---

# System Architecture

The Health Tracker System follows a Forms-Centric Architecture with modularized code organization.

Main Components:
- Windows Forms Interface
- HealthLogic Class
- SQL Server LocalDB Database
- ADO.NET SqlClient Connectivity

---

# Scope

The system includes:
- User Registration and Login
- BMI Calculation
- Symptoms Monitoring
- Daily Health Recording
- Health History Viewing
- SQL Server LocalDB Storage
- CRUD-related database operations

---

# Limitations

- Windows devices only
- Offline system
- Educational purposes only
- Does not provide medical diagnosis
- Limited to authorized users

---

# Validation Features

The system implements validation to ensure data integrity.

Validation includes:
- Required field checking
- Numeric validation
- Empty field checking
- Success and error messages

---

# Database Operations

The system performs:
- Create Operations
- Read Operations
- Update Operations

through ADO.NET SqlClient integration.

---

# Project Structure

