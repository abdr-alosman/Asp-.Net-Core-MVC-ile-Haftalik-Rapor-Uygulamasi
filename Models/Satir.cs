using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HaftalıkRaporu.Models
{
    public class Satir
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 5, ErrorMessage = "Önem Derecesi 1 ile 5 Arasında olması lazım")]
        public int OnemDerecesi { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime BaslangisTar { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime BitisTar { get; set; }

        public string YapilanIsler { get; set; }
        public string TalepSahibi { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime BlanlanmisTar { get; set; }

        public bool Timeout { get; set; }

        public string HarcananSure { get; set; }
        public bool Durumu { get; set; }
        public string Yorumlar { get; set; }

        [Display(Name = "Rapor")]
        public int RaporId { get; set; }
        [ForeignKey("RaporId")]
        public virtual Rapor Rapor { get; set; }
    }
}
