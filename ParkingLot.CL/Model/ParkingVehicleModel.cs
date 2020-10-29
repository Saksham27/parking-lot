using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ParkingLot.CL.Model
{
    public class ParkingVehicleModel
    {
        [DataMember(Name = "VehicleOwnerId")]
        [Required(ErrorMessage = "Vehicle Owner Id Is Required")]
        [RegularExpression(" ^[1-9][0-9]*$", ErrorMessage = "Vehicle owner id is not valid")]
        public int VehicleOwnerId { get; set; }

        [DataMember(Name = "VehicleNumber")]
        [Required(ErrorMessage = "Vehicle Number Is Required")]
        [RegularExpression("^[A-Z][a-zA-Z]{3,15}$", ErrorMessage = "Vehicle Number is not valid")]
        public string VehicleNumber { get; set; }

        [DataMember(Name = "VehicleBrand")]
        [Required(ErrorMessage = "Vehicle brand Is Required")]
        [RegularExpression("^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$", ErrorMessage = "Vehicle brand is not valid")]
        public string VehicleBrand { get; set; }

        [DataMember(Name = "VehicleColor")]
        [Required(ErrorMessage = "Vehicle Color Is Required")]
        [RegularExpression("[a-zA-Z]+$", ErrorMessage = "Vehicle color is not valid")]
        public string VehicleColor { get; set; }

        [DataMember(Name = "ParkedInLot")]
        [RegularExpression("^([12])$", ErrorMessage = "Lot number is not valid")]
        public int ParkedInLot { get; set; }

        [DataMember(Name = "ParkingSlot")]
        [RegularExpression("^([1-9]|[1-8][0-9]|9[0-9]|100)$", ErrorMessage = "Parking slot is not valid")]
        public int ParkingSlot { get; set; }

        [DataMember(Name = "ParkingStatus")]
        [Required(ErrorMessage = "Parking status Is Required")]
        public bool ParkingStatus { get; set; }

        public string EntryTime { get; set; }

        public string ExitTime { get; set; }
    }
}
