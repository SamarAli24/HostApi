using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public int UserType { get; set; }
        public string Name { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string Mobile { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string confirmPassword { get; set; } = null!;
        public DateTime JoinDate { get; set; }
        public DateTime Timestamp { get; set; }
        public int Sex { get; set; }
    }
}
