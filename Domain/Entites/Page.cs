using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Page
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(2048)]
        public string Url { get; set; }

        [Required]
        public DateTime FoundDateTime { get; set; }

        public DateTime? LastScanDate { get; set; }

        [Required]
        public int SiteId { get; set; }
        public Site Site { get; set; }
    }
}
