using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Durum { get; set; }
        public string IslemTipi { get; set; }
        public string Aciklama { get; set; }
        public string DateTime { get; set; }
        public string UserIp { get; set; }
    }
}
