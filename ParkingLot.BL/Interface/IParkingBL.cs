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
    }
}
