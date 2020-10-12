USE [ParkingLotDB]
GO
/****** Object:  StoredProcedure [dbo].[spSelectLotToPark]    Script Date: 12-10-2020 16:28:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- -----------------------------------------------------------------------
-- Author : Saksham Singh
-- description : Stored procedure for getting all users details
-- ----------------------------------------------------------------------
ALTER PROCEDURE [dbo].[spSelectLotToPark]
AS
BEGIN
	Declare @SpaceInLot1 int = (Select Top 1 SlotsLeftinLot from SlotManagement where 
									LotNumber = 1 Order by SlotTransactionId desc)
	Declare @SpaceInLot2 int = (Select Top 1 SlotsLeftinLot from SlotManagement where 
									LotNumber = 2 Order by SlotTransactionId desc)
	IF @SpaceInLot1 > @SpaceInLot2
	BEGIN
		Select 1
	END
	IF @SpaceInLot1 < @SpaceInLot2
	BEGIN
		Select 2
	END
	IF @SpaceInLot1 = @SpaceInLot2
	BEGIN
		SELECT CAST(FLOOR(RAND()*(2-1+1)+1) AS int);
	END
END