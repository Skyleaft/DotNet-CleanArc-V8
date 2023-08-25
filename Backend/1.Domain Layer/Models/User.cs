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
    [Index(nameof(Username),IsUnique = true)]
    public class User : Modifier
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The value cannot exceed 30 characters. And Less than 3"), MinLength(3)]
        public string Username { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Password must be 8 characters or more"), MinLength(8)]
        public string Password { get; set; }
        public UserDetail? UserDetail { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }

    }
}
