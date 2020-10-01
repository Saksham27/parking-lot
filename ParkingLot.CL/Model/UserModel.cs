using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ParkingLot.CL.Model
{
    [DataContract]
    public class UserModel
    {
        [DataMember(Name = "FirstName")]
        [Required(ErrorMessage = "Employee Name Is Required")]
        [RegularExpression("^[A-Z][a-zA-Z]{3,15}$", ErrorMessage = "First Name is not valid")]
        public string FirstName { get; set; }

        [DataMember(Name = "LastName")]
        [RegularExpression("^[A-Z][a-zA-Z]{3,15}$", ErrorMessage = "Last Name is not valid")]
        public string LastName { get; set; }

        [DataMember(Name = "EmailID")]
        [Required(ErrorMessage = "EmailID Is Required")]
        [RegularExpression("^[a-zA-Z0-9]{1,}([.]?[-]?[+]?[a-zA-Z0-9]{1,})?[@]{1}[a-zA-Z0-9]{1,}[.]{1}[a-z]{2,3}([.]?[a-z]{2})?$", ErrorMessage = "E-mail is not valid")]
        public string EmailID { get; set; }

        [DataMember(Name = "UserRole")]
        [Required(ErrorMessage = "UserRole Is Required")]
        [RegularExpression("^[A-Z][a-zA-Z]{3,15}$", ErrorMessage = "Driver Category is not valid")]
        public string UserRole { get; set; }

        [DataMember(Name = "Handicapped")]
        [Required(ErrorMessage = "Disability status Is Required")]
        public bool Handicapped { get; set; }

        [DataMember(Name = "UserName")]
        [Required(ErrorMessage = "Username Is Required")]
        public string UserName { get; set; }

        [DataMember(Name = "Password")]
        [Required(ErrorMessage = "Password Is Required")]
        [RegularExpression("^.{8,15}$", ErrorMessage = "Password Length should be between 8 to 15")]
        public string Password { get; set; }

    }
}
