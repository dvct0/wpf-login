USE [master]
GO
/****** Object:  Database [dbUsers]    Script Date: 28.04.2021 22:38:03 ******/
CREATE DATABASE [dbUsers]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbUsers_Data', FILENAME = N'c:\dzsqls\dbUsers.mdf' , SIZE = 8192KB , MAXSIZE = 30720KB , FILEGROWTH = 22528KB )
 LOG ON 
( NAME = N'dbUsers_Logs', FILENAME = N'c:\dzsqls\dbUsers.ldf' , SIZE = 8192KB , MAXSIZE = 30720KB , FILEGROWTH = 22528KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [dbUsers] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbUsers].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbUsers] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbUsers] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbUsers] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbUsers] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbUsers] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbUsers] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbUsers] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbUsers] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbUsers] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbUsers] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbUsers] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbUsers] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbUsers] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbUsers] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbUsers] SET  ENABLE_BROKER 
GO
ALTER DATABASE [dbUsers] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbUsers] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbUsers] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbUsers] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbUsers] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbUsers] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbUsers] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbUsers] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dbUsers] SET  MULTI_USER 
GO
ALTER DATABASE [dbUsers] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbUsers] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbUsers] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbUsers] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbUsers] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbUsers] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [dbUsers] SET QUERY_STORE = OFF
GO
USE [dbUsers]
GO
/****** Object:  User [artshaga_SQLLogin_1]    Script Date: 28.04.2021 22:38:07 ******/
CREATE USER [artshaga_SQLLogin_1] FOR LOGIN [artshaga_SQLLogin_1] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [artshaga_SQLLogin_1]
GO
/****** Object:  Schema [artshaga_SQLLogin_1]    Script Date: 28.04.2021 22:38:07 ******/
CREATE SCHEMA [artshaga_SQLLogin_1]
GO
/****** Object:  Table [dbo].[logsData]    Script Date: 28.04.2021 22:38:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[logsData](
	[id_session] [int] IDENTITY(100,1) NOT NULL,
	[code_person] [int] NULL,
	[date_session] [datetime] NULL,
	[time] [varchar](10) NULL,
 CONSTRAINT [PK_logsData] PRIMARY KEY CLUSTERED 
(
	[id_session] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[person]    Script Date: 28.04.2021 22:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[person](
	[code_person] [int] IDENTITY(1,1) NOT NULL,
	[login_user] [nvarchar](50) NULL,
	[phone] [nvarchar](50) NULL,
	[name] [nvarchar](50) NULL,
 CONSTRAINT [PK_person] PRIMARY KEY CLUSTERED 
(
	[code_person] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 28.04.2021 22:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[code_role] [int] IDENTITY(1,1) NOT NULL,
	[role] [nvarchar](50) NULL,
 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[code_role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 28.04.2021 22:38:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[login] [nvarchar](50) NOT NULL,
	[pass] [nvarchar](50) NULL,
	[role] [int] NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[logsData] ON 

INSERT [dbo].[logsData] ([id_session], [code_person], [date_session], [time]) VALUES (116, 12, CAST(N'2021-04-28T00:00:00.000' AS DateTime), N'0:0:36')
INSERT [dbo].[logsData] ([id_session], [code_person], [date_session], [time]) VALUES (117, 12, CAST(N'2021-04-28T00:00:00.000' AS DateTime), N'0:0:9')
INSERT [dbo].[logsData] ([id_session], [code_person], [date_session], [time]) VALUES (118, 13, CAST(N'2021-04-28T00:00:00.000' AS DateTime), N'0:0:57')
INSERT [dbo].[logsData] ([id_session], [code_person], [date_session], [time]) VALUES (119, 13, CAST(N'2021-04-28T00:00:00.000' AS DateTime), N'0:0:8')
SET IDENTITY_INSERT [dbo].[logsData] OFF
GO
SET IDENTITY_INSERT [dbo].[person] ON 

INSERT [dbo].[person] ([code_person], [login_user], [phone], [name]) VALUES (12, N'Admin', N'000000', N'Админ')
INSERT [dbo].[person] ([code_person], [login_user], [phone], [name]) VALUES (13, N'Viktor', N'111222', N'Виктор')
SET IDENTITY_INSERT [dbo].[person] OFF
GO
SET IDENTITY_INSERT [dbo].[role] ON 

INSERT [dbo].[role] ([code_role], [role]) VALUES (1, N'user')
INSERT [dbo].[role] ([code_role], [role]) VALUES (2, N'admin')
SET IDENTITY_INSERT [dbo].[role] OFF
GO
INSERT [dbo].[users] ([login], [pass], [role]) VALUES (N'Admin', N'Admin1234', 2)
INSERT [dbo].[users] ([login], [pass], [role]) VALUES (N'Viktor', N'Viktor1234', 1)
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_role]  DEFAULT ((1)) FOR [role]
GO
ALTER TABLE [dbo].[logsData]  WITH CHECK ADD  CONSTRAINT [FK_logsData_person] FOREIGN KEY([code_person])
REFERENCES [dbo].[person] ([code_person])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[logsData] CHECK CONSTRAINT [FK_logsData_person]
GO
ALTER TABLE [dbo].[person]  WITH CHECK ADD  CONSTRAINT [FK_person_users] FOREIGN KEY([login_user])
REFERENCES [dbo].[users] ([login])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[person] CHECK CONSTRAINT [FK_person_users]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_role] FOREIGN KEY([role])
REFERENCES [dbo].[role] ([code_role])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_role]
GO
USE [master]
GO
ALTER DATABASE [dbUsers] SET  READ_WRITE 
GO
