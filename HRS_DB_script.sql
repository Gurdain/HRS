USE [master]
GO
/****** Object:  Database [HRS_DB]    Script Date: 21-08-2019 11:44:03 ******/
CREATE DATABASE [HRS_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HRS_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\HRS_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HRS_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\HRS_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [HRS_DB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HRS_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HRS_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HRS_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HRS_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HRS_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HRS_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [HRS_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HRS_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HRS_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HRS_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HRS_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HRS_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HRS_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HRS_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HRS_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HRS_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HRS_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HRS_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HRS_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HRS_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HRS_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HRS_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HRS_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HRS_DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HRS_DB] SET  MULTI_USER 
GO
ALTER DATABASE [HRS_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HRS_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HRS_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HRS_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HRS_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HRS_DB] SET QUERY_STORE = OFF
GO
USE [HRS_DB]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 21-08-2019 11:44:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[HotelId] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailQueue]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailQueue](
	[EmailId] [int] IDENTITY(1,1) NOT NULL,
	[ToAddress] [varchar](50) NOT NULL,
	[FromAddress] [varchar](50) NOT NULL,
	[Subject] [varchar](50) NOT NULL,
	[Body] [varchar](max) NOT NULL,
	[Tries] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_EmailQueue] PRIMARY KEY CLUSTERED 
(
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Exception]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exception](
	[ExceptionId] [int] IDENTITY(1,1) NOT NULL,
	[ExceptionNumber] [varchar](50) NOT NULL,
	[ExceptionMessage] [varchar](max) NOT NULL,
	[ExceptionUrl] [varchar](50) NOT NULL,
	[ExceptionMethod] [varchar](50) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Exception] PRIMARY KEY CLUSTERED 
(
	[ExceptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hotels]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hotels](
	[HotelId] [int] IDENTITY(1,1) NOT NULL,
	[HotelName] [varchar](80) NOT NULL,
	[Address] [varchar](100) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[Locality] [varchar](50) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[HotelTypeId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Rooms] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Hotels] PRIMARY KEY CLUSTERED 
(
	[HotelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MailLogs]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailLogs](
	[MailId] [int] IDENTITY(1,1) NOT NULL,
	[ToAddress] [varchar](50) NOT NULL,
	[FromAddress] [varchar](50) NOT NULL,
	[Subject] [varchar](50) NOT NULL,
	[Body] [varchar](max) NOT NULL,
	[EmailStatus] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_MailLogs] PRIMARY KEY CLUSTERED 
(
	[MailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[role] [varchar](20) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[RoomId] [int] IDENTITY(1,1) NOT NULL,
	[HotelId] [int] NOT NULL,
	[IsAvailable] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[Booked] [bit] NOT NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Type]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[HotelTypeId] [int] IDENTITY(1,1) NOT NULL,
	[HotelType] [varchar](80) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[HotelTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](80) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[RoleId] [int] NOT NULL,
	[Phone] [varchar](15) NOT NULL,
	[UserGuid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[DeletedOn] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[EmailQueue] ADD  CONSTRAINT [DF_EmailQueue_Tries]  DEFAULT ((0)) FOR [Tries]
GO
ALTER TABLE [dbo].[EmailQueue] ADD  CONSTRAINT [DF_EmailQueue_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Exception] ADD  CONSTRAINT [DF_Exception_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Hotels] ADD  CONSTRAINT [DF_Hotels_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[MailLogs] ADD  CONSTRAINT [DF_MailLogs_Status]  DEFAULT ((0)) FOR [EmailStatus]
GO
ALTER TABLE [dbo].[MailLogs] ADD  CONSTRAINT [DF_MailLogs_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_IsAvailable]  DEFAULT ((1)) FOR [IsAvailable]
GO
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_Book]  DEFAULT ((0)) FOR [Booked]
GO
ALTER TABLE [dbo].[Type] ADD  CONSTRAINT [DF_Type_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_UserGuid]  DEFAULT (newid()) FOR [UserGuid]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Hotels] FOREIGN KEY([HotelId])
REFERENCES [dbo].[Hotels] ([HotelId])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Hotels]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Room]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Users]
GO
ALTER TABLE [dbo].[Hotels]  WITH CHECK ADD  CONSTRAINT [FK_Hotels_Type] FOREIGN KEY([HotelTypeId])
REFERENCES [dbo].[Type] ([HotelTypeId])
GO
ALTER TABLE [dbo].[Hotels] CHECK CONSTRAINT [FK_Hotels_Type]
GO
ALTER TABLE [dbo].[Hotels]  WITH CHECK ADD  CONSTRAINT [FK_Hotels_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Hotels] CHECK CONSTRAINT [FK_Hotels_Users]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Hotels] FOREIGN KEY([HotelId])
REFERENCES [dbo].[Hotels] ([HotelId])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Hotels]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Role]
GO
/****** Object:  StoredProcedure [dbo].[Book_Delete]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Book_Delete](
@BookId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Book
					SET IsDeleted = 1, DeletedOn = GETUTCDATE()
					WHERE BookId = @BookId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Book_Insert]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Book_Insert](
@BookId int out,
@UserId int,
@HotelId int,
@RoomId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					INSERT INTO Book
									(UserId, HotelId, RoomId, CreatedOn, ModifiedOn)
					VALUES
									(@UserId, @HotelId, @RoomId, GETUTCDATE(), GETUTCDATE())
					SET @BookId = SCOPE_IDENTITY()
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Book_Read]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Book_Read]
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Book
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Book_ReadById]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Book_ReadById](
@BookId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Book
					WHERE BookId = @BookId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Book_Room]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Book_Room](
@BookId int out,
@UserId int,
@HotelId int,
@RoomId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					INSERT INTO Book
									(UserId, HotelId, RoomId, CreatedOn, ModifiedOn)
					VALUES
									(@UserId, @HotelId, @RoomId, GETUTCDATE(), GETUTCDATE())
					SET @BookId = SCOPE_IDENTITY()
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Book_Update]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Book_Update](
@BookId int,
@UserId int,
@HotelId int,
@RoomId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Book
					SET UserId = @UserId, HotelId = @HotelId, RoomId = @RoomId, ModifiedOn = GETUTCDATE()
					WHERE BookId = @BookId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Email_Delete]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Email_Delete](
@EmailId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE EmailQueue
					SET IsDeleted = 1, DeletedOn = GETUTCDATE()
					WHERE EmailId = @EmailId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Email_Insert]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Email_Insert](
@EmailId int out,
@ToAddress varchar(50),
@FromAddress varchar(50),
@Subject varchar(50),
@Body varchar(max)
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					INSERT INTO EmailQueue
									(ToAddress, FromAddress, Subject, Body, CreatedOn, ModifiedOn)
					VALUES
									(@ToAddress, @FromAddress, @Subject, @Body, GETUTCDATE(), GETUTCDATE())
					SET @EmailId = SCOPE_IDENTITY()
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Email_Read]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Email_Read]
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM EmailQueue
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Email_ReadById]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Email_ReadById](
@EmailId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM EmailQueue
					WHERE EmailId = @EmailId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Email_Undelete]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Email_Undelete](
@EmailId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE EmailQueue
					SET IsDeleted = 0, DeletedOn = NULL
					WHERE EmailId = @EmailId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Email_Update]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Email_Update](
@EmailId int,
@ToAddress varchar(50),
@FromAddress varchar(50),
@Subject varchar(50),
@Body varchar(max),
@Tries int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE EmailQueue
					SET ToAddress = @ToAddress, FromAddress = @FromAddress, Subject = @Subject, Body = @Body, Tries = @Tries, ModifiedOn = GETUTCDATE()
					WHERE EmailId = @EmailId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Exception_Insert]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Exception_Insert](
@ExceptionNumber varchar(50),
@ExceptionMessage varchar(max),
@ExceptionUrl varchar(50),
@ExceptionMethod varchar(50)
)  
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					INSERT INTO Exception
									(ExceptionNumber, ExceptionMessage, ExceptionUrl, ExceptionMethod, createdOn, modifiedOn) 
					VALUES
									(@ExceptionNumber, @ExceptionMessage, @ExceptionUrl, @ExceptionMethod, GETUTCDATE(), GETUTCDATE())
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Exception_Read]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Exception_Read]
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Exception
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Exception_ReadById]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Exception_ReadById](
@ExceptionId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Exception
					WHERE ExceptionId = @ExceptionId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Hotels_Delete]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Hotels_Delete](
@HotelId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Hotel
					SET IsDeleted = 1
					WHERE HotelId = @HotelId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Hotels_Insert]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Hotels_Insert](
@HotelId int out,
@HotelName varchar(80),
@Address varchar(100),
@City varchar(50),
@Locality varchar(50),
@Description varchar(MAX),
@HotelTypeId int,
@UserId int,
@Rooms int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					INSERT INTO Hotels
									(HotelName, Address, City, Locality, Description, HotelTypeId, UserId, Rooms, CreatedOn, ModifiedOn)
					VALUES
									(@HotelName, @Address, @City, @Locality, @Description, @HotelTypeId, @UserId, @Rooms, GETUTCDATE(), GETUTCDATE())
					SET @HotelId = SCOPE_IDENTITY()
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Hotels_Read]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Hotels_Read]
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Hotels
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Hotels_ReadById]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Hotels_ReadById](
@HotelId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Hotels
					WHERE HotelId = @HotelId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Hotels_Update]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Hotels_Update](
@HotelId int,
@HotelName varchar(80),
@Address varchar(100),
@City varchar(50),
@Locality varchar(50),
@Description varchar(MAX),
@HotelTypeId int,
@UserId int,
@Rooms int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Hotels
					SET HotelName = @HotelName, Address = @Address, City = @City, Locality = @Locality, Description = @Description, HotelTypeId = @HotelTypeId, UserId = @UserId, Rooms = @Rooms, ModifiedOn = GETUTCDATE()
					WHERE HotelId = @HotelId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Mail_Delete]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Mail_Delete](
@MailId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE MailLogs
					SET IsDeleted = 1, DeletedOn = GETUTCDATE()
					WHERE MailId = @MailId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Mail_Insert]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Mail_Insert](
@MailId int out,
@ToAddress varchar(50),
@FromAddress varchar(50),
@Subject varchar(50),
@Body varchar(max),
@EmailStatus bit
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					INSERT INTO MailLogs
									(ToAddress, FromAddress, Subject, Body, EmailStatus, CreatedOn, ModifiedOn)
					VALUES
									(@ToAddress, @FromAddress, @Subject, @Body, @EmailStatus, GETUTCDATE(), GETUTCDATE())
					SET @MailId = SCOPE_IDENTITY()
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Mail_Read]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Mail_Read]
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM MailLogs
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Mail_ReadById]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Mail_ReadById](
@MailId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM MailLogs
					WHERE MailId = @MailId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Mail_Update]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Mail_Update](
@MailId int,
@ToAddress varchar(50),
@FromAddress varchar(50),
@Subject varchar(50),
@Body varchar(max),
@EmailStatus bit
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE MailLogs
					SET ToAddress = @ToAddress, FromAddress = @FromAddress, Subject = @Subject, Body = @Body, EmailStatus = @EmailStatus, ModifiedOn = GETUTCDATE()
					WHERE MailId = @MailId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Role_Delete]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Role_Delete](
@RoleId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Role
					SET IsDeleted = 1
					WHERE RoleId = @RoleId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Role_Insert]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Role_Insert](
@RoleId int out,
@role varchar(20)
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					INSERT INTO Role
									(role, CreatedOn, ModifiedOn)
					VALUES
									(@role, GETUTCDATE(), GETUTCDATE())
					SET @RoleId = SCOPE_IDENTITY()
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Role_Read]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Role_Read]
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Role
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Role_ReadById]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Role_ReadById](
@RoleId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Role
					WHERE RoleId = @RoleId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Role_Update]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Role_Update](
@RoleId int,
@role varchar(20)
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Role
					SET role = @role, ModifiedOn = GETUTCDATE()
					WHERE RoleId = @RoleId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Room_Delete]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Room_Delete](
@RoomId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Room
					SET IsDeleted = 1, DeletedOn = GETUTCDATE()
					WHERE RoomId = @RoomId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Room_Insert]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Room_Insert](
@RoomId int out,
@HotelId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					INSERT INTO Room
									(HotelId, CreatedOn, ModifiedOn)
					VALUES
									(@HotelId, GETUTCDATE(), GETUTCDATE())
					SET @RoomId = SCOPE_IDENTITY()
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Room_Read]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Room_Read]
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Room
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Room_ReadById]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Room_ReadById](
@RoomId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Room
					WHERE RoomId = @RoomId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Room_Update]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Room_Update](
@RoomId int,
@HotelId int,
@IsAvailable bit,
@Booked bit
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Room
					SET HotelId = @HotelId, IsAvailable = @IsAvailable, Booked = @Booked, ModifiedOn = GETUTCDATE()
					WHERE RoomId = @RoomId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Type_Delete]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Type_Delete](
@HotelTypeId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Type
					SET IsDeleted = 1
					WHERE HotelTypeId = @HotelTypeId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Type_Insert]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Type_Insert](
@HotelTypeId int out,
@HotelType varchar(80)
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					INSERT INTO Type
									(HotelType, CreatedOn, ModifiedOn)
					VALUES
									(@HotelType, GETUTCDATE(), GETUTCDATE())
					SET @HotelTypeId = SCOPE_IDENTITY()
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Type_Read]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Type_Read]
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Type
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Type_ReadById]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Type_ReadById](
@HotelTypeId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Type
					WHERE HotelTypeId = @HotelTypeId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Type_Update]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Type_Update](
@HotelTypeId int,
@HotelType varchar(80)
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Type
					SET HotelType = @HotelType, ModifiedOn = GETUTCDATE()
					WHERE HotelTypeId = @HotelTypeId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[User_ActivateAccount]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[User_ActivateAccount](
@UserGuid uniqueidentifier
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Users
					SET IsActive = 1
					WHERE UserGuid = @UserGuid
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[User_Delete]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[User_Delete](
@UserId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Users
					SET IsDeleted = 1, DeletedOn = GETUTCDATE()
					WHERE UserId = @UserId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[User_Insert]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[User_Insert](
@UserId int out,
@UserName varchar(80),
@Email varchar(50),
@Password varchar(20),
@RoleId int,
@Phone varchar(15),
@UserGuid uniqueIdentifier
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					INSERT INTO Users
									(UserName, Email, Password, RoleId, Phone, UserGuid, CreatedOn, ModifiedOn)
					VALUES
									(@UserName, @Email, @Password, @RoleId, @Phone, @UserGuid, GETUTCDATE(), GETUTCDATE())
					SET @UserId = SCOPE_IDENTITY()
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[User_Login]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[User_Login](
@Password varchar(20),
@Email varchar(50)
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Users
					WHERE Password = @Password
					AND Email = @Email
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[User_Read]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[User_Read]
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Users
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[User_ReadById]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[User_ReadById](
@UserId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Users
					WHERE UserId = @UserId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[User_Update]    Script Date: 21-08-2019 11:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[User_Update](
@UserId int,
@UserName varchar(80),
@Email varchar(50),
@Password varchar(20),
@RoleId int,
@Phone varchar(15)
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Users
					SET UserName = @UserName, Email = @Email, Password = @Password, RoleId = @RoleId, Phone = @Phone, ModifiedOn = GETUTCDATE()
					WHERE UserId = @UserId
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
USE [master]
GO
ALTER DATABASE [HRS_DB] SET  READ_WRITE 
GO
