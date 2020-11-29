using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStonks.SPA.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public char Role { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime LastPasswordChange { get; set; }
        public bool EnforcePasswordChange { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public List<Advert> Adverts { get; set; }
    }
}
