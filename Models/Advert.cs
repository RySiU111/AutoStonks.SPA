using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStonks.SPA.Models
{
    public enum State
    {
        New,
        Active,
        Expired,
        Terminated
    }
    public class Advert
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string Description { get; set; }
        public string VIN { get; set; }
        public DateTime FirstRegistrationDate { get; set; }
        public string PlateNumber { get; set; }
        public State State { get; set; }
        public  AdvertDetails AdvertDetails { get; set; }
        public List<Photo> Photos { get; set; }
        public List<AdvertEquipment> AdvertEquipments { get; set; }
        public List<UserAdvert> UserAdverts { get; set; }
    }
}
