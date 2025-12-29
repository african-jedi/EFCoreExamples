# Setting Up the Database Schema

## Prerequisites

- SQL Server Express or SQL Server installed
- SQL Server Management Studio (SSMS) or another SQL client

## Create Database

1. Open SQL Server Management Studio
2. Connect to your SQL Server instance (SQL Express or SQL Server)
3. In Object Explorer, right-click on **Databases** and select **New Database**
4. Enter the database name: `DatabaseFirstDB`
5. Click **OK** to create the database

## Create Tables

Execute the following SQL script in SQL Server Management Studio against the `DatabaseFirstDB` database:

```sql
USE [DatabaseFirstDB]
GO

-- Create Categories table
CREATE TABLE [dbo].[Categories](
	[RowId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[Modified] [datetime2](7) NOT NULL,
	CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([RowId] ASC)
) ON [PRIMARY]
GO

-- Create Products table
CREATE TABLE [dbo].[Products](
	[RowId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[CategoryRowId] [int] NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[Modified] [datetime2](7) NOT NULL,
	CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([RowId] ASC)
) ON [PRIMARY]
GO

-- Add foreign key relationship
ALTER TABLE [dbo].[Products] WITH CHECK ADD 
	CONSTRAINT [FK_Products_Categories_CategoryRowId] 
	FOREIGN KEY([CategoryRowId]) REFERENCES [dbo].[Categories] ([RowId])
	ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories_CategoryRowId]
GO
```

## Table Structure

### Categories Table
- **RowId** (int): Primary key, auto-increment
- **CategoryName** (nvarchar): Category name
- **Created** (datetime2): Record creation timestamp
- **Modified** (datetime2): Record last modification timestamp

### Products Table
- **RowId** (int): Primary key, auto-increment
- **Name** (nvarchar): Product name
- **Price** (decimal): Product price with 2 decimal places
- **CategoryRowId** (int): Foreign key to Categories table
- **Created** (datetime2): Record creation timestamp
- **Modified** (datetime2): Record last modification timestamp

## Next Steps

After creating the database and tables, proceed with scaffolding the EF Core models using the database-first approach. See [readme.md](readme.md) for detailed scaffolding instructions.