﻿
using BPJSApotekOnlineDeveloper.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPJSApotekOnlineDeveloper.Areas.MasterData.Models
{
    [Table("MasterUserActive", Schema = "dbo")]
    public class UserActive : UserActivity
    {
        [Key]
        public Guid UserActiveId { get; set; }
        public string UserActiveCode { get; set; }
        public string FullName { get; set; }
        public string IdentityNumber { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Handphone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

    }
}
