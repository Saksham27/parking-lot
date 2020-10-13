USE [ParkingLotDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- -----------------------------------------------------------------------
-- Author : Saksham Singh
-- description : Stored procedure for getting all users details
-- ----------------------------------------------------------------------
create PROCEDURE [dbo].[spFindVehiclesByNumberPlate]
@vehicleNumber varchar(50)
AS
BEGIN
	Select VehicleNumber, VehicalBrand,
	VehicalColor, ParkingSlot, ParkingStatus, EntryTime, ExitTime,
	FirstName, LastName, UserName, EmailID, 
	Address, UserRole, Handicapped, RegistrationDate 
	from (ParkingLot JOIN RegisteredUsers
	ON ParkingLot.VehicleOwner = RegisteredUsers.ID) where VehicleNumber =@vehicleNumber
	
END