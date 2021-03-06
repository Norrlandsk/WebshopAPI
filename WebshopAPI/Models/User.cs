using System;
using System.ComponentModel.DataAnnotations;

namespace WebshopAPI.Models
{
    /// <summary>
    /// Model class for User table
    /// </summary>
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime LastLogin { get; set; }

        public DateTime SessionTimer { get; set; }

        public bool IsActive { get; set; }

        public bool IsAdmin { get; set; }

        
    }
}