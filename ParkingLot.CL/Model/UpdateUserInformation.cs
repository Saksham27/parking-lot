using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParkingLot.CL.Model
{
    public class UpdateUserInformation
    {
       
        [RegularExpression("^[A-Z][a-zA-Z]{3,15}$", ErrorMessage = "First Name is not valid")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

       
        [RegularExpression("^[a-zA-Z0-9]{1,}([.]?[-]?[+]?[a-zA-Z0-9]{1,})?[@]{1}[a-zA-Z0-9]{1,}[.]{1}[a-z]{2,3}([.]?[a-z]{2})?$", ErrorMessage = "E-mail is not valid")]
        public string EmailID { get; set; }

        public string Address { get; set; }
        
        [RegularExpression("^[A-Z][a-zA-Z]{3,15}$", ErrorMessage = "Driver Category is not valid")]
        public string UserRole { get; set; }

        
        public bool Handicapped { get; set; }

       
        public string UserName { get; set; }

        
        [RegularExpression("^.{8,15}$", ErrorMessage = "Password Length should be between 8 to 15")]
        public string Password { get; set; }
    }
}
