USE [ParkingLotDB]
GO
/****** Object:  StoredProcedure [dbo].[spCheckIfVehicleParkedOrNot]    Script Date: 12-10-2020 16:25:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- -----------------------------------------------------------------------
-- Author : Saksham Singh
-- description : Stored procedure for getting all users details
-- ----------------------------------------------------------------------
ALTER PROCEDURE [dbo].[spCheckIfVehicleParkedOrNot]
@VehicleNumber varchar(50)
AS
Declare @status int = 0;
BEGIN
	If EXISTS(Select TOP 1 VehicleNumber From ParkingLot Where VehicleNumber = @VehicleNumber)
	Set @status = 1
	Else
	Set @status = 0
	Select @status
END