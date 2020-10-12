USE [ParkingLotDB]
GO
/****** Object:  StoredProcedure [dbo].[spLoginUser]    Script Date: 12-10-2020 16:27:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- -----------------------------------------------------------------------
-- Author : Saksham Singh
-- description : Stored procedure for login
-- ----------------------------------------------------------------------
ALTER PROCEDURE [dbo].[spLoginUser]
@UserName nvarchar(50),
@Password nvarchar(50)
AS
declare @loginStatus int
BEGIN
	If Exists(Select * From RegisteredUsers Where (UserName=@UserName AND Password=@Password) OR (EmailID=@UserName AND Password=@Password))
		Set @loginStatus=1
	Else
		Set @loginStatus=0
	Select @loginStatus
END