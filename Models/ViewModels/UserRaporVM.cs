using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaftalıkRaporu.Models.ViewModels
{
    public class UserRaporVM
    {
        public ApplicationUser ApplicationUser  { get; set; }
        public Rapor Rapor  { get; set; }
        public Satir Satir  { get; set; }
        public IEnumerable<ApplicationUser> UserList { get; set; }
        public IEnumerable<Rapor> RaportList { get; set; }
        public IEnumerable<Satir> SatirList { get; set; }
    }
}
