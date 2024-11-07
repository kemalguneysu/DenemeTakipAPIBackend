using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.DTOs.Tyt
{
    public class ListSingleTyt
    {
        public string Id { get; set; }
        public int TurkceDogru { get; set; } 
        public int TurkceYanlis { get; set; } 

        public int MatematikDogru { get; set; } 
        public int MatematikYanlis { get; set; } 

        public int FenDogru { get; set; } 
        public int FenYanlis { get; set; } 

        public int SosyalDogru { get; set; } 
        public int SosyalYanlis { get; set; }

        public List<KonularAdDers> YanlisKonularAd { get; set; }
        public List<KonularAdDers> BosKonularAd { get; set; }

    }
    public class KonularAdDers
    {
        public string KonuAdi { get; set; }
        public string DersAdi { get; set; }
        public string DersId { get; set; }
        public string KonuId { get; set; }


    }
}
