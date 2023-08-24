﻿using DomainLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Module : Modifier
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? ModuleFieldId { get; set; }
        public ICollection<ModuleField>? Fields { get; set; }
        public bool? isSupportDetail { get; set; }
        public int? ModuleDetailId { get; set; }
        public ICollection<ModuleDetail>? ModuleDetail { get; set; }

    }
}