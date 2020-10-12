using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParkingLot.CL.Model
{
    public class ShowUserInformation
    {
        [Required(ErrorMessage = "Id Is Required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee Name Is Required")]
        [System.ComponentModel.DataAnnotations.RegularExpression("^[A-Z][a-zA-Z]{3,15}$", ErrorMessage = "First Name is not valid")]
        public string FirstName { get; set; }

        [RegularExpression("^[A-Z][a-zA-Z]{3,15}$", ErrorMessage = "Last Name is not valid")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "EmailID Is Required")]
        [RegularExpression("^[a-zA-Z0-9]{1,}([.]?[-]?[+]?[a-zA-Z0-9]{1,})?[@]{1}[a-zA-Z0-9]{1,}[.]{1}[a-z]{2,3}([.]?[a-z]{2})?$", ErrorMessage = "E-mail is not valid")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Address Is Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "UserRole Is Required")]
        [RegularExpression("^[A-Z][a-zA-Z]{3,15}$", ErrorMessage = "Driver Category is not valid")]
        public string UserRole { get; set; }

        [Required(ErrorMessage = "Username Is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Disability status Is Required")]
        public bool Handicapped { get; set; }

        public string RegistationDate { get; set; }

        public string LastUpdated { get; set; }


    }
}
