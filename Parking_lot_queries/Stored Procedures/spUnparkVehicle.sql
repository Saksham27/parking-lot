USE [ParkingLotDB]
GO
/****** Object:  StoredProcedure [dbo].[spUnparkVehicle]    Script Date: 12-10-2020 16:29:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- -----------------------------------------------------------------------
-- Author : Saksham Singh
-- description : Stored procedure for login
-- ----------------------------------------------------------------------
ALTER PROCEDURE [dbo].[spUnparkVehicle]
@ticketId int,
@exitTime datetime
AS
BEGIN
	Update ParkingLot
	Set ParkingStatus = 'unparked', ExitTime = @exitTime Where (ParkingID = @ticketId)
	Insert Into SlotManagement(ParkingId, LotNumber, SlotsLeftinLot)
			Values((Select ParkingID from ParkingLot where (ParkingID = @ticketId)),
			(Select ParkedInLot from ParkingLot where (ParkingID = @ticketId)), 
			(Select top 1 SlotsLeftinLot from SlotManagement 
				Where LotNumber = (Select ParkedInLot from ParkingLot where (ParkingID = @ticketId)) 
				order by SlotTransactionId desc)+1)
	Select * from ParkingLot where (ParkingID = @ticketId)
END