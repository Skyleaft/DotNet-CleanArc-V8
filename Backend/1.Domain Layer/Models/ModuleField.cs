using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class ModuleField
    {
        [Key]
        public Int64 Id { get; set; }
        public Int64 ModuleId { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public int Sequence { get; set; }
        public int DataTypeId { get; set; }
        public Common.DataType? DataType { get; set; }
        public int ControlTypeId { get; set; }
        public ControlType? ControlType { get; set; }
        public int? FieldSize { get; set; }
        public string? DefaultValue { get; set; }
        public bool isPrimaryKey { get; set; }
        public bool isUnique { get; set; }
        public bool isRequired { get; set; }
    }
}
