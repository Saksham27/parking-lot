using ParkingLot.CL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot.BL.Interface
{
    public interface IParkingBL
    {
        ResponseMessage<ParkingTicket> ParkVehicle(ParkVehicleModel vehicleDetails);
        ResponseMessage<ParkingBill> UnparkVehicle(int ticketId);
        ResponseMessage<List<LocateVehicleModel>> FindVehiclesByColor(string color);
        ResponseMessage<List<LocateVehicleModel>> FindVehiclesByBrand(string brand);
        ResponseMessage<List<LocateVehicleModel>> FindVehiclesByNumberPlate(string vehicleNumber);

    }
}
