using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models
{
    public class Ilce
    {
        public int Id { get; set; }
        public string IlceName { get; set; }
        [ForeignKey("IlId")]
        public int IlId { get; set; }
        public virtual Il Iller { get; set; }
    }
}
