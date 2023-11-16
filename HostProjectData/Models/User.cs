using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    [Table("user")]
    public partial class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("userType")]
        public int UserType { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [Column("firstName")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [Column("lastName")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Column("dob", TypeName = "datetime")]
        public DateTime Dob { get; set; }
        [Required]
        [Column("mobile")]
        [StringLength(50)]
        public string Mobile { get; set; }
        [Required]
        [Column("email")]
        [StringLength(250)]
        public string Email { get; set; }
        [Required]
        [Column("passHash")]
        [StringLength(50)]
        public string PassHash { get; set; }
        [Required]
        [Column("passSalt")]
        [StringLength(50)]
        public string PassSalt { get; set; }
        [Column("joinDate", TypeName = "datetime")]
        public DateTime JoinDate { get; set; }
        [Column("timestamp", TypeName = "datetime")]
        public DateTime Timestamp { get; set; }
        [Column("sex")]
        public int Sex { get; set; }
    }
}
