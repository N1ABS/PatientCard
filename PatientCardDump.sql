USE [master]
GO
/****** Object:  Database [PatientCardDb]    Script Date: 11/16/2019 12:42:59 AM ******/
CREATE DATABASE [PatientCardDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PatientCardDb', FILENAME = N'C:\Users\Nurbolat\PatientCardDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PatientCardDb_log', FILENAME = N'C:\Users\Nurbolat\PatientCardDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PatientCardDb] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PatientCardDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PatientCardDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PatientCardDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PatientCardDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PatientCardDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PatientCardDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [PatientCardDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PatientCardDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PatientCardDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PatientCardDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PatientCardDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PatientCardDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PatientCardDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PatientCardDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PatientCardDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PatientCardDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PatientCardDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PatientCardDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PatientCardDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PatientCardDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PatientCardDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PatientCardDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PatientCardDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PatientCardDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PatientCardDb] SET  MULTI_USER 
GO
ALTER DATABASE [PatientCardDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PatientCardDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PatientCardDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PatientCardDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PatientCardDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PatientCardDb] SET QUERY_STORE = OFF
GO
USE [PatientCardDb]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [PatientCardDb]
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 11/16/2019 12:43:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[SpecialtyId] [int] NOT NULL,
 CONSTRAINT [PK_Doctor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 11/16/2019 12:43:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Iin] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialty]    Script Date: 11/16/2019 12:43:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialty](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Specialty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VisitLog]    Script Date: 11/16/2019 12:43:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DoctorId] [int] NOT NULL,
	[PatientId] [int] NOT NULL,
	[Diagnosis] [nvarchar](max) NULL,
	[Complaint] [nvarchar](max) NULL,
	[VisitDate] [datetime] NULL,
 CONSTRAINT [PK_VisitLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Doctor] ON 

INSERT [dbo].[Doctor] ([Id], [Name], [SpecialtyId]) VALUES (1, N'Name 1', 2)
INSERT [dbo].[Doctor] ([Id], [Name], [SpecialtyId]) VALUES (2, N'Иванов док', 1)
INSERT [dbo].[Doctor] ([Id], [Name], [SpecialtyId]) VALUES (4, N'Name 4', 3)
INSERT [dbo].[Doctor] ([Id], [Name], [SpecialtyId]) VALUES (6, N'Name 6', 3)
INSERT [dbo].[Doctor] ([Id], [Name], [SpecialtyId]) VALUES (9, N'Врач 1', 3)
INSERT [dbo].[Doctor] ([Id], [Name], [SpecialtyId]) VALUES (10, N'Некоторый док', 2)
SET IDENTITY_INSERT [dbo].[Doctor] OFF
SET IDENTITY_INSERT [dbo].[Patient] ON 

INSERT [dbo].[Patient] ([Id], [Name], [Iin], [Address], [Phone]) VALUES (1, N'Patient 1', N'879619832500', N'Address 11', N'(123) 456-7890')
INSERT [dbo].[Patient] ([Id], [Name], [Iin], [Address], [Phone]) VALUES (2, N'Patient 3', N'917849187328', N'Address 2', N'(889) 671-9828')
INSERT [dbo].[Patient] ([Id], [Name], [Iin], [Address], [Phone]) VALUES (4, N'Нурболат Абсагат', N'930323350600', N'Астана, Мангилик ел, 8', N'(747) 232-2303')
SET IDENTITY_INSERT [dbo].[Patient] OFF
SET IDENTITY_INSERT [dbo].[Specialty] ON 

INSERT [dbo].[Specialty] ([Id], [Name]) VALUES (1, N'Лор')
INSERT [dbo].[Specialty] ([Id], [Name]) VALUES (2, N'Окулист')
INSERT [dbo].[Specialty] ([Id], [Name]) VALUES (3, N'Терапевт')
SET IDENTITY_INSERT [dbo].[Specialty] OFF
SET IDENTITY_INSERT [dbo].[VisitLog] ON 

INSERT [dbo].[VisitLog] ([Id], [DoctorId], [PatientId], [Diagnosis], [Complaint], [VisitDate]) VALUES (1, 1, 2, N'diagnosis 1', N'complaint 121212', CAST(N'2019-11-20T12:01:00.000' AS DateTime))
INSERT [dbo].[VisitLog] ([Id], [DoctorId], [PatientId], [Diagnosis], [Complaint], [VisitDate]) VALUES (2, 2, 1, N'астма', N'боли в горле', CAST(N'2019-11-15T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[VisitLog] OFF
ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_Specialty] FOREIGN KEY([SpecialtyId])
REFERENCES [dbo].[Specialty] ([Id])
GO
ALTER TABLE [dbo].[Doctor] CHECK CONSTRAINT [FK_Doctor_Specialty]
GO
ALTER TABLE [dbo].[VisitLog]  WITH CHECK ADD  CONSTRAINT [FK_VisitLog_Doctor] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctor] ([Id])
GO
ALTER TABLE [dbo].[VisitLog] CHECK CONSTRAINT [FK_VisitLog_Doctor]
GO
ALTER TABLE [dbo].[VisitLog]  WITH CHECK ADD  CONSTRAINT [FK_VisitLog_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO
ALTER TABLE [dbo].[VisitLog] CHECK CONSTRAINT [FK_VisitLog_Patient]
GO
USE [master]
GO
ALTER DATABASE [PatientCardDb] SET  READ_WRITE 
GO
