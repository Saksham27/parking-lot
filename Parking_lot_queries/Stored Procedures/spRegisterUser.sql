USE [ParkingLotDB]
GO
/****** Object:  StoredProcedure [dbo].[spRegisterUser]    Script Date: 12-10-2020 16:28:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- -----------------------------------------------------------------------
-- Author : Saksham Singh
-- description : Stored procedure for registering Users
-- ----------------------------------------------------------------------
ALTER PROCEDURE [dbo].[spRegisterUser] (
@FirstName varchar(50),
@LastName varchar(50),
@Email varchar(50),
@Address varchar(50),
@UserName varchar(50),
@Password varchar(50),
@UserRole varchar(50),
@Handicapped bit,
@RegistrationDate datetime
)
AS
Declare @registrationStatus int
BEGIN
	If Not Exists(Select * From RegisteredUsers Where UserName=@UserName OR EmailID=@Email)
	BEGIN
		Insert into RegisteredUsers(FirstName,LastName,EmailID,Address,UserName,Password,UserRole,Handicapped,RegistrationDate)
		Values(@FirstName,@LastName,@Email,@Address,@UserName,@Password,@UserRole,@Handicapped,@RegistrationDate);
		Set @registrationStatus=1
	END
	Else
		Set @registrationStatus=0	
	Select @registrationStatus
END