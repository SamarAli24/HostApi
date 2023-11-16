using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class UserCUModel
    {
       // public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string FamilyName { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
        public int Sex { get; set; }
        public string Password { get; set; } = null!;
        public string confirmPassword { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = null!;
     
        
      
        public DateTime JoinDate { get; set; }
        
    }
}
