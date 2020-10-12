CREATE TABLE ParkingLot
(
  ParkingID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  VehicleOwner int FOREIGN KEY REFERENCES RegisteredUsers(Id),
  VehicleNumber varchar(50),
  VehicalBrand varchar(50),
  VehicalColor varchar(50),
  ParkedInLot int,
  ParkingSlot  varchar(50),
  ParkingStatus varchar(50),
  EntryTime  datetime,
  ExitTime  datetime,
);
select * from ParkingLot