using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class PersonPageRank
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Rank { get; set; }

        [Required]
        public int PersonId { get; set; }
        public Person Person { get; set; }

        [Required]
        public int PageId { get; set; }
        public Page Page { get; set; }
    }
}
