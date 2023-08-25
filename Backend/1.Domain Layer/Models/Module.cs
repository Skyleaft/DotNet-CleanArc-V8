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
    public class Module : Modifier
    {
        [Key]
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string? Description { get; set; }
        public ICollection<ModuleField>? ModuleField { get; set; }
        public ICollection<ModuleDetail>? ModuleDetail { get; set; }
        public bool? isSupportView { get; set; }
        public bool? isSupportAdd { get; set; }
        public bool? isSupportEdit { get; set; }
        public bool? isSupportDelete { get; set; }
        public bool? isSupportDetail { get; set; }

    }
}
