using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ToDoApp.Models.Users
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Username { get; set; }

        [Required]
        [MaxLength(150)]
        public string Password { get; set; }

        [MaxLength(150)]
        public string FirstName { get; set; }

        [MaxLength(150)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Role { get; set; }

        [Required]
        public DateTime AddedOn { get; set; }

        public DateTime? EditedOn { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Id: {Id}");
            sb.AppendLine($"Username: {Username}");
            sb.AppendLine($"Password: {Password}");
            sb.AppendLine($"First Name: {FirstName}");
            sb.AppendLine($"Last Name: {LastName}");
            sb.AppendLine($"Role: {Role}");
            sb.AppendLine($"Date Of Creation: {AddedOn}");
            sb.AppendLine($"Date Of Last Edit: {EditedOn}");

            return sb.ToString();
        }

    }
}
