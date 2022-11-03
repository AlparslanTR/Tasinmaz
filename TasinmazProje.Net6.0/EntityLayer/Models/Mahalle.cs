using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models
{
    public class Mahalle
    {
        public int Id { get; set; }
        public string MahalleAdi { get; set; }
        [ForeignKey("Ilce")]
        public int IlceId { get; set; }
        public virtual Ilce Ilceler { get; set; }
    }
}
