using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ParkingLot.CL.Model
{
    public class ParkingBill
    {

        [DataMember(Name = "VehicleNumber")]
        public string VehicleNumber { get; set; }

        public string EntryTime { get; set; }

        public string ExitTime { get; set; }

        public string ParkingCost { get; set; }

    }
}
