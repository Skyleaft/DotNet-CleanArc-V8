using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class MValueString : Modifier
    {
        public int ModuleFieldId { get; set; }
        public string? Value { get; set; }
    }
}
