using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserEvent
    {
        public string Id { get; set; } 

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Action { get; set; }
    }
}
