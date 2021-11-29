using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Models
{
    public class SubSection:BaseEntity
    {
        public string Name { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
