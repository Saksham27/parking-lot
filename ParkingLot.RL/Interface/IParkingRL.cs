
using ParkingLot.CL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot.RL.Interface
{
    public interface IParkingRL
    {
        Tuple<string,ParkingTicket> ParkVehicle(ParkVehicleModel vehcileDetails);
        Tuple<string,ParkingBill> UnparkVehicle(int ticketId);
    }
}
