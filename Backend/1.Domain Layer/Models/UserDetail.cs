using DomainLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class UserDetail
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [StringLength(40, ErrorMessage = "The value cannot exceed 40 characters. ")]
        public string? Name { get; set; }
        [StringLength(40, ErrorMessage = "The value cannot exceed 40 characters. ")]
        public string? Email { get; set; }
        [StringLength(20, ErrorMessage = "The value cannot exceed 20 characters. ")]
        public string? Phone { get; set; }
        public string? PhotoURL { get; set; }

    }
}
