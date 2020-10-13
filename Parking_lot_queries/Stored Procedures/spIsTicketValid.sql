USE [ParkingLotDB]
GO
/****** Object:  StoredProcedure [dbo].[spIsTicketValid]    Script Date: 12-10-2020 16:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- -----------------------------------------------------------------------
-- Author : Saksham Singh
-- description : Stored procedure for getting all users details
-- ----------------------------------------------------------------------
ALTER PROCEDURE [dbo].[spIsTicketValid]
@ticketId int
AS
Declare @status int = 0;
BEGIN
	If EXISTS(Select TOP 1 * From ParkingLot Where ParkingID = @ticketId)
	Set @status = 1
	Else
	Set @status = 0
	Select @status
END