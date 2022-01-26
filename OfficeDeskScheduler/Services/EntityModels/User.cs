using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.EntityModels
{
    public class User
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? EditedDate { get; set; }
        public long? EditedBy { get; set; }
        public string? ContactNumber { get; set; }
        public string? EmailAddress { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? AddressOne { get; set; }
        public string? AddressTwo { get; set; }
        public string? ZipCode { get; set; }
        public string? Gender { get; set; }
        public string? ImagePath { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public bool IsUserLoggedIn { get; set; }
        public string? Wallpaper { get; set; }
    }
}
