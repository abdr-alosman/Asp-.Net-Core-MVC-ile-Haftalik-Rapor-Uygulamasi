using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HaftalıkRaporu.Models
{
    public class Rapor
    {
        [Key]
        public int Id { get; set; }
        public string RaporTanimi { get; set; }
        public bool Durumu { get; set; }
        public string RaporlayanKisi { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DuzenlenmeTarihi { get; set; }
        public string ApplicationUserId { get; set; }
     
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public List<Satir> SatirList { get; set; }


    }
}
