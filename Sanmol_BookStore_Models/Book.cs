using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmol_BookStore_Models
{
    public class Book
    {
        public int Id { get; set; }  

        [Required(ErrorMessage = "Please enter book title")]
        [StringLength(100)]
        public string Title { get; set; }  

        [Required(ErrorMessage = "Please enter book Author")]
        [StringLength(100)]
        public string Author { get; set; }  

        [Required(ErrorMessage = "Please enter book ISBN")]
        [StringLength(13)]
        public string ISBN { get; set; } 

        [Required(ErrorMessage = "Please enter Price")]
        [Range(0, 10000)]
        public decimal Price { get; set; }  

        [Required(ErrorMessage = "Please enter book Category")]
        public int CategoryId { get; set; }  
        public Category? Category { get; set; }  

        [StringLength(255)]
        public string? CoverImagePath { get; set; }  

        public DateTime CreatedAt { get; set; }  

        public DateTime UpdatedAt { get; set; }  


    }
}
