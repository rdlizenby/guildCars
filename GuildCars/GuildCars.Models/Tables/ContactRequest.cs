using GuildCars.Models.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GuildCars.Models.Tables
{
    [PhoneOrEmailAttribute(ErrorMessage =("Please provide either email or phone number"))]
    public class ContactRequest
    {
        public int ContactRequestId { get; set; }

        [Required(ErrorMessage = ("Please enter your name"))]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage ="Not a valid email format")]
        public string Email { get; set; }

        [Phone(ErrorMessage = ("Phone number format not valid"))]
        public string Phone { get; set; }

        [Required(ErrorMessage = ("Please enter a message"))]
        public string Message { get; set; }
        public string SalesPersonId { get; set; }         //UserId
        public DateTime RespondedOn { get; set; }
        public string Response { get; set; }
    }
}
