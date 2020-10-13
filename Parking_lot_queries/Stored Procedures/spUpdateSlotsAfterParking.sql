USE [ParkingLotDB]
GO
/****** Object:  StoredProcedure [dbo].[spUpdateSlotsAfterParking]    Script Date: 12-10-2020 16:29:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- -----------------------------------------------------------------------
-- Author : Saksham Singh
-- description : Stored procedure for getting all users details
-- ----------------------------------------------------------------------
ALTER PROCEDURE [dbo].[spUpdateSlotsAfterParking]
@parkingId int,
@parkingLot int
AS
BEGIN
	Insert into SlotManagement(ParkingId,LotNumber,SlotsLeftinLot)
		Values(@parkingId,@parkingLot, (Select Top 1 SlotsLeftinLot from SlotManagement where (LotNumber=@parkingLot) ORDER by SlotTransactionId desc)-1)
END