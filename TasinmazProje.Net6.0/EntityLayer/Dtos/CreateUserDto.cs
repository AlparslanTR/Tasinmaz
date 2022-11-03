using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos
{
    public class CreateUserDto
    {
        public int RolId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
    }
}
