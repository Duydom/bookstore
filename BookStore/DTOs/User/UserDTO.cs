using BookStore.DTOs.Role;
using BookStore.Model;
using System.ComponentModel.DataAnnotations;

namespace BookStore.DTOs.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public string Phone { get; set; }
        public DateTime Dob { get; set; }
        public string Avatar { get; set; }
        public string Username { get; set; }
        public bool IsDeleted { get; set; }
        public virtual RoleDTO Role { get; set; }
    }
}
