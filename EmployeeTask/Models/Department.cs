using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Models
{
    public class Department:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Section> Sections { get; set; }
    }
}
