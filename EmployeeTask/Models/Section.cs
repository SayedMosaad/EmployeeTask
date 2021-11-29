using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Models
{
    public class Section:BaseEntity
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<SubSection> SubSections { get; set; }
    }
}
