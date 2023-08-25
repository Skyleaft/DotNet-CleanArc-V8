using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "The value cannot exceed 50 characters. ")]
        public string Name { get; set; }
        public int Level { get; set; }
    }
}
