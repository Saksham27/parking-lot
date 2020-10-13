USE [ParkingLotDB]
GO
/****** Object:  StoredProcedure [dbo].[spCheckIfDriverHandicapped]    Script Date: 12-10-2020 16:24:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- -----------------------------------------------------------------------
-- Author : Saksham Singh
-- description : Stored procedure for getting all users details
-- ----------------------------------------------------------------------
ALTER PROCEDURE [dbo].[spCheckIfDriverHandicapped]
@userName varchar(50)
AS
Declare @result int
BEGIN
	IF (Select Handicapped from RegisteredUsers where UserName = @userName) = 1
	Set @result = 1
	Else
	Set @result = 0
	Select @result
END