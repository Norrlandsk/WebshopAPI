using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;

namespace WebshopAPI.Models
{
    public class SoldBook
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        
        public int CategoryId { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
