using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Models
{
    public class Employee:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime HireDate { get; set; }
        [Required]
        public double Salary { get; set; }
        [Required]
        [Display(Name ="Sub Section")]
        public int SubSectionId { get; set; }
        public SubSection SubSection { get; set; }
    }
}
