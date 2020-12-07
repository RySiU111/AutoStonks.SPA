using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStonks.SPA.Models
{
    public class Payment
    {
        //wzór na rabat: (1 - [ilość ogłoszeń użytkownika / 50]) * 10
        //zabezpieczyć przed zerem
        //maksymalnie 50% rabatu

        public int Id { get; set; }
        public Advert Advert { get; set; }
        public int AdvertId { get; set; }
        public float Price { get; set; } // 10-> 1zł/d, 20-> 0.9zł/d, 30-> 0.8zł/d; wyróżnienie 5zł/10dni
        public DateTime PaymentInitiation { get; set; } //data rozpoczęcia płatności
        public DateTime PaymentTermination { get; set; } //data otrzymania płatności
        public DateTime StartDate { get; set; } //kiedy zaczyna ogłoszenie/wyróżnienie
        public int DurationInDays { get; set; } //na ile dni wykupiono ogłoszenie; 10, 20 lub 30 dni
    }
}
