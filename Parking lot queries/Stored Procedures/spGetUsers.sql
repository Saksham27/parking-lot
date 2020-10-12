USE [ParkingLotDB]
GO
/****** Object:  StoredProcedure [dbo].[spGetUsers]    Script Date: 12-10-2020 16:27:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- -----------------------------------------------------------------------
-- Author : Saksham Singh
-- description : Stored procedure for getting all users details
-- ----------------------------------------------------------------------
ALTER PROCEDURE [dbo].[spGetUsers]
AS
BEGIN
	Select * into #TempTable from RegisteredUsers
	Alter table #TempTable drop column Password
	Select * from #TempTable
	Drop table #TempTable
END