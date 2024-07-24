using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS_Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
       
        [ForeignKey("userRole")]
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }= true;
        public bool IsDelted { get; set; }= false;
        public UserRole userRole { get; set; }
    }
}
