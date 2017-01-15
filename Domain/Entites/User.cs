using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 3)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
