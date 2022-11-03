using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos
{
    public class CreateTasinmazDto
    {
        public int IlId { get; set; }
        public int IlceId { get; set; }
        public int MahalleId { get; set; }
        public int Parsel { get; set; }
        public int Ada { get; set; }
        public string Nitelik { get; set; }
        public string XCoordinate { get; set; }
        public string YCoordinate { get; set; }
        public string ParselCoordinate { get; set; }
    }
}
