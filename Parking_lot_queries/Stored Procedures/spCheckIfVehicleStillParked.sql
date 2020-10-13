USE [ParkingLotDB]
GO
/****** Object:  StoredProcedure [dbo].[spCheckIfVehicleStillParked]    Script Date: 12-10-2020 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- -----------------------------------------------------------------------
-- Author : Saksham Singh
-- description : Stored procedure for getting all users details
-- ----------------------------------------------------------------------
ALTER PROCEDURE [dbo].[spCheckIfVehicleStillParked]
@ticketId int
AS
Declare @status int = 0;
BEGIN
	If (Select TOP 1 ParkingStatus From ParkingLot Where ParkingID = @ticketId) = 'parked'
	Set @status = 1
	Else
	Set @status = 0
	Select @status
END