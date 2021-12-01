using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTask.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedOnUTC { get; set; } = DateTime.UtcNow;
        public int RoleId { get; set; }
        public Role Roles { get; set; }
    }

    public class LoginVm
    {
        public string Name { get; set; }
        //[Required, EmailAddress]
        //public string Email { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
    public class RegisterVm:LoginVm
    {
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public int RoleId { get; set; }
    }

    

    public class UserItem
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public String Role { get; set; }
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    }
}
