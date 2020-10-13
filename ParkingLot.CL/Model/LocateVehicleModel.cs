using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot.CL.Model
{
    public class LocateVehicleModel
    {
        public string VehicleNumber { get; set; }

        public string VehicleBrand { get; set; }

        public string VehicleColor { get; set; }

        public string ParkingSlot { get; set; }

        public string ParkingStatus { get; set; }

        public string EntryTime { get; set; }

        public string ExitTime { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; } 

        public string UserName { get; set; }

        public string EmailID { get; set; }

        public string Address { get; set; }

        public string UserRole { get; set; }

        public bool Handicapped { get; set; }

        public string RegistationDate { get; set; }

    }
}
