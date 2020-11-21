using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStonks.SPA.Models
{
    public class UserAdvert
    {
        public int AdvertId { get; set; }
        public Advert Advert { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
