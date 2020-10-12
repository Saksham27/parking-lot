using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ParkingLot.CL.Model
{
    public class ParkingTicket
    {
        [DataMember(Name = "TicketId")]
        [Required(ErrorMessage = "Ticket Id Is Required")]
        [RegularExpression(" ^[1-9][0-9]*$", ErrorMessage = "Ticket id is not valid")]
        public int TicketId { get; set; }

        [DataMember(Name = "VehicleNumber")]
        [Required(ErrorMessage = "Vehicle Number Is Required")]
        [RegularExpression("^[A-Z][a-zA-Z]{3,15}$", ErrorMessage = "Vehicle Number is not valid")]
        public string VehicleNumber { get; set; }

        [DataMember(Name = "ParkingSlot")]
        public string ParkingSlot { get; set; }

        public string EntryTime { get; set; }

    }
}
