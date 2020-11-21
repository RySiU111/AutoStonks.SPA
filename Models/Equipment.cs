using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStonks.SPA.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AdvertEquipment> AdvertEquipments { get; set; }
    }
}
