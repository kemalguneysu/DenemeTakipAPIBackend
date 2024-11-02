using DenemeTakipAPI.Domain.Entities.Common;
using DenemeTakipAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Domain.Entities.DenemeFolder.TytFolder
{
    public class TytDeneme:BaseEntity
    {
        public TytDeneme()
        {
            YanlisKonular = new List<Konu>();
            BosKonular = new List<Konu>();
        }
        public int TurkceDogru { get; set; }
        public int TurkceYanlis { get; set; }

        public int MatematikDogru { get; set; }
        public int MatematikYanlis { get; set; }

        public int FenDogru { get; set; }
        public int FenYanlis { get; set; }

        public int SosyalDogru { get; set; }
        public int SosyalYanlis { get; set; }

        public List<Konu>? YanlisKonular { get; set; }
        public List<Konu>? BosKonular { get; set; }
        public AppUser User { get; set; }


    }
}
