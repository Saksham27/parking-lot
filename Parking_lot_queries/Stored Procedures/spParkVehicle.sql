USE [ParkingLotDB]
GO
/****** Object:  StoredProcedure [dbo].[spParkVehicle]    Script Date: 12-10-2020 16:28:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- -----------------------------------------------------------------------
-- Author : Saksham Singh
-- description : Stored procedure for registering Users
-- ----------------------------------------------------------------------
ALTER PROCEDURE [dbo].[spParkVehicle] (
@VehicleOwnerUserName varchar(50),
@VehicleNumber varchar(50),
@VehicleBrand varchar(50),
@VehicleColor varchar(50),
@ParkedInLot int,
@ParkingSlot varchar(50),
@ParkingStatus varchar(50),
@EntryTime datetime
)
AS
BEGIN
	Insert into ParkingLot(VehicleOwner,VehicleNumber,VehicalBrand,VehicalColor,ParkedInLot,ParkingSlot,ParkingStatus,EntryTime)
			Values((SELECT ID from RegisteredUsers where UserName = @VehicleOwnerUserName),@VehicleNumber,@VehicleBrand,@VehicleColor,@ParkedInLot,@ParkingSlot,@ParkingStatus,@EntryTime)
	Select * from ParkingLot where VehicleNumber=@VehicleNumber
END