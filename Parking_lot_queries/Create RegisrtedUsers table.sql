CREATE TABLE RegisteredUsers
(
  ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  FirstName varchar(50),
  LastName varchar(50),
  EmailID varchar(50),
  Address varchar(max),
  UserName varchar(50),
  Password varchar(50),
  UserRole varchar(50),
  Handicapped bit,
  RegistrationDate datetime,
  LastUpdated datetime
);
select * from RegisteredUsers