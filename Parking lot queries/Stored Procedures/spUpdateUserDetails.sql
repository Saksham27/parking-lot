USE [ParkingLotDB]
GO
/****** Object:  StoredProcedure [dbo].[spUpdateUserDetails]    Script Date: 12-10-2020 16:30:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- -----------------------------------------------------------------------
-- Author : Saksham Singh
-- description : Stored procedure for updating user details
-- ----------------------------------------------------------------------
ALTER PROCEDURE [dbo].[spUpdateUserDetails] (
-- result=1 for successful execution
@Id int,
@FirstName varchar(50) = null,
@LastName varchar(50) = null,
@EmailID varchar(max) = null,
@Address varchar(max) = null,
@UserName varchar(50) = null,
@Handicapped bit = null,
@LastUpdated datetime
)
AS
declare @result int=0
BEGIN
	if exists (select * from RegisteredUsers where Id=@Id)
	BEGIN
		Update RegisteredUsers
		Set FirstName=ISNULL(@FirstName,FirstName), LastName=ISNULL(@LastName,LastName),
			EmailID=ISNULL(@EmailID,EmailID),  Address=ISNULL(@Address,Address),
			UserName=ISNULL(@UserName,UserName),
			Handicapped=ISNULL(@Handicapped,Handicapped), LastUpdated = @LastUpdated
		Where Id=@Id AND ( @FirstName IS NOT NULL OR 
						@LastName IS NOT NULL OR @EmailID IS NOT NULL OR 
						@UserName IS NOT NULL OR @Handicapped IS NOT NULL);
		Set @result=1;
	END
	select @result
END