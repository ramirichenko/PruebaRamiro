
IF  NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'testDB')
    BEGIN
        CREATE DATABASE [testDB]
    END;


USE [testDB]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Inventory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[outputDate] [datetime] NULL,
	[registrationDate] [datetime] NULL,
	[type] [varchar](255) NULL,
	[expirationDate] [date] NULL,
	[registerUser] [varchar](255) NULL,
	[outputUser] [varchar](255) NULL,
 CONSTRAINT [PK_Inventory_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


