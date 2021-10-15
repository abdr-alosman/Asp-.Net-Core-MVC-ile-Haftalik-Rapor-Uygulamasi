using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HaftalıkRaporu.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Unvan { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public List<Rapor> RaporList { get; set; }

    }
}
