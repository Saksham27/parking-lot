USE [ParkingLotDB]
GO
/****** Object:  StoredProcedure [dbo].[spDeleteUserDetail]    Script Date: 12-10-2020 16:26:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- -----------------------------------------------------------------------
-- Author : Saksham Singh
-- description : Stored procedure for deleting employee
-- ----------------------------------------------------------------------
ALTER PROCEDURE [dbo].[spDeleteUserDetail]
@Id int
AS
declare @deletionStatus int=0
BEGIN
	if exists(select Id from RegisteredUsers where Id=@Id)
	BEGIN
		Delete From RegisteredUsers where Id=@Id
		Set @deletionStatus=1
	END
	Else
		Set @deletionStatus=0
	select @deletionStatus
END