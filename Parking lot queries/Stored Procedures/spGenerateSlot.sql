USE [ParkingLotDB]
GO
/****** Object:  StoredProcedure [dbo].[spGenerateSlot]    Script Date: 12-10-2020 16:26:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- -----------------------------------------------------------------------
-- Author : Saksham Singh
-- description : Stored procedure for getting all users details
-- ----------------------------------------------------------------------
ALTER PROCEDURE [dbo].[spGenerateSlot]
@Lot int,
@isDriverHandicapped int
AS
BEGIN
	Declare @LotSign varchar(10)
	IF @lot = 1
	Set @LotSign = 'A'
	Else
	Set @LotSign = 'B'
	IF @isDriverHandicapped = 1
	BEGIN
		DECLARE @counter int = 1;
		DECLARE @selectedSlotId int
		WHILE @counter <= 100
		BEGIN
			set @selectedSlotId = (Select Top 1 ParkingId FROM ParkingLot Where ( ParkedInLot = @Lot AND ParkingSlot = @LotSign+'-'+CAST(@counter as varchar)) Order By ParkingID desc)
			IF @selectedSlotId = NULL
			BEGIN
				Select @counter
				BREAK;
			END
			ELSE
			BEGIN
				IF (Select ParkingStatus from ParkingLot where ParkingID = @selectedSlotId) = 'parked'
				BEGIN
					Set @counter = @counter + 1;
					Continue;
				END
				ELSE
				Select @counter
				BREAK;
			END
			
		END
	END
	ELSE
	BEGIN
		DECLARE @counter1 int = 100;
		DECLARE @selectedSlotId1 int
		WHILE @counter1 >= 1
		BEGIN
			set @selectedSlotId1 = (Select Top 1 ParkingId FROM ParkingLot Where ( ParkedInLot = @Lot AND ParkingSlot = @LotSign+'-'+CAST(@counter1 as varchar)) Order By ParkingID desc)
			IF @selectedSlotId1 = NULL
			BEGIN
				Select @counter1
				BREAK;
			END
			ELSE
			BEGIN
				IF (Select ParkingStatus from ParkingLot where ParkingID = @selectedSlotId1) = 'parked'
				BEGIN
					Set @counter1 = @counter1 - 1;
					Continue;
				END
				ELSE 
				Select @counter1
				BREAK;
			END	
			
		END
	END
END