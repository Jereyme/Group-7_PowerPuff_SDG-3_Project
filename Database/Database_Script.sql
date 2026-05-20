-- =========================================
-- HEALTH-TRACKER SYSTEM DATABASE
-- =========================================

CREATE DATABASE HealthTrackerDataBase;
GO

USE HealthTrackerDataBase;
GO

-- =========================================
-- TABLE: Users (With Salt Column)
-- =========================================
CREATE TABLE [dbo].[users] (
    [user_id]       INT            IDENTITY(1,1) NOT NULL,
    [full_name]     NVARCHAR(255)  NOT NULL,
    [username]      NVARCHAR(255)  NOT NULL UNIQUE,
    [password_hash] NVARCHAR(255)  NOT NULL,
    [salt]          NVARCHAR(255)  NOT NULL, -- Added for extra password security
    [role]          NVARCHAR(50)   DEFAULT 'USER' NOT NULL,
    [created_at]    DATETIME       DEFAULT GETDATE() NOT NULL,
    PRIMARY KEY CLUSTERED ([user_id] ASC)
);
GO

-- =========================================
-- TABLE: Health Records
-- =========================================
CREATE TABLE [dbo].[health_records] (
    [record_id]  INT            IDENTITY(1,1) NOT NULL,
    [user_id]    INT            NOT NULL,
    [bmi]        FLOAT(53)          NOT NULL,
    [symptoms]   NVARCHAR(255)  NOT NULL,
    [assessment] NVARCHAR(255)  NOT NULL,
    [created_at] DATETIME       DEFAULT GETDATE() NOT NULL,
    PRIMARY KEY CLUSTERED ([record_id] ASC),
    CONSTRAINT [fk_healthrecords_user] 
        FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([user_id]) 
        
);
GO

-- =========================================
-- TABLE: Logs 
-- =========================================
CREATE TABLE [dbo].[logs] (
    [log_id]   INT            IDENTITY (1, 1) NOT NULL,
    [user_id]  INT            NOT NULL,
    [action]   NVARCHAR (100) NOT NULL,
    [log_time] DATETIME       DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([log_id] ASC),
        FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([user_id]) 
);
GO