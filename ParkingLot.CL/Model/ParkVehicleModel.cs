using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ParkingLot.CL.Model
{
    public class ParkVehicleModel
    {
        [DataMember(Name = "VehicleOwnerUserName")]
        [Required(ErrorMessage = "Vehicle Owner User name Is Required")]
        public string VehicleOwnerUserName { get; set; }

        [DataMember(Name = "VehicleNumber")]
        [Required(ErrorMessage = "Vehicle Number Is Required")]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "Vehicle Number is not valid")]
        public string VehicleNumber { get; set; }

        [DataMember(Name = "VehicleBrand")]
        [Required(ErrorMessage = "Vehicle brand Is Required")]
        [RegularExpression("^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$", ErrorMessage = "Vehicle brand is not valid")]
        public string VehicleBrand { get; set; }

        [DataMember(Name = "VehicleColor")]
        [Required(ErrorMessage = "Vehicle Color Is Required")]
        [RegularExpression("[a-zA-Z]+$", ErrorMessage = "Vehicle color is not valid")]
        public string VehicleColor { get; set; }
    }
}
