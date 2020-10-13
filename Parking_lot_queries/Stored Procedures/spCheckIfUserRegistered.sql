USE [ParkingLotDB]
GO
/****** Object:  StoredProcedure [dbo].[spCheckIfUserRegistered]    Script Date: 12-10-2020 16:25:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- -----------------------------------------------------------------------
-- Author : Saksham Singh
-- description : Stored procedure for login
-- ----------------------------------------------------------------------
ALTER PROCEDURE [dbo].[spCheckIfUserRegistered]
@userName nvarchar(50)
AS
declare @userRigistered int
BEGIN
	If Exists(Select * From RegisteredUsers Where (UserName=@userName)) 
		Set @userRigistered=1
	Else
		Set @userRigistered=0
	Select @userRigistered
END