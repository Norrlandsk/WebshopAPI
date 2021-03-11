using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebshopAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; } = "Codic2021";
        [Required]
        public DateTime LastLogin { get; set; }
        [Required]
        public DateTime SessionTimer { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public bool IsAdmin { get; set; } = false;
        [Required]
        public List<SoldBook> UserId { get; set; }
    }
}