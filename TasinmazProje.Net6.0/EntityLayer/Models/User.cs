using CoreLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Models
{
    public class User
    {
        public int Id { get; set; }

        [ForeignKey("Rols")]
        public int RolId { get; set; }
        public Rol Rol { get; set; }


        public string Name { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
