CREATE TABLE SlotManagement
(
  SlotTransactionId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  ParkingId int FOREIGN KEY REFERENCES ParkingLot(ParkingId),
  LotNumber int,
  SlotsLeftinLot int
);
Insert into SlotManagement (LotNumber, SlotsLeftinLot)
Values(1,100)
Insert into SlotManagement (LotNumber, SlotsLeftinLot)
Values(2,100)
select * from SlotManagement