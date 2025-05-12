using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Sanmol_BookStore_Models
{
    public class Category
    {
        public int Id { get; set; }  

        [Required]
        [StringLength(100)]
        public string Name { get; set; }  
        public bool IsDeleted { get; set; } = false;


        public virtual ICollection<Book>? Books { get; set; } 
    }
}
