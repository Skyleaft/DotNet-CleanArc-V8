using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ModuleField
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public ModuleType Type { get; set; }
        public int FieldSize { get; set; }
        public bool isPrimaryKey { get; set; }
        public bool isUnique { get; set; }
        public bool isRequired { get; set; }
    }
}
