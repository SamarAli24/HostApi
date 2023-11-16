using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class UserSetModel
    {
        public int Id { get; set; }
        public int UserType { get; set; }
        public string Name { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string Mobile { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Instagram { get; set; } = null!;
        public string Twitter { get; set; } = null!;
        public string PassHash { get; set; } = null!;
        public string PassSalt { get; set; } = null!;
        public DateTime JoinDate { get; set; }
        public DateTime Timestamp { get; set; }
        public int Sex { get; set; }

        public int? CityId { get; set; }
    }
}
