using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class ModuleDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? ModuleDetailFieldId { get; set; }
        public ICollection<ModuleDetailField>? DetailField { get; set; }
    }
}
