using DenemeTakipAPI.Domain.Entities.Common;
using DenemeTakipAPI.Domain.Entities.DenemeFolder.AytFolder;
using DenemeTakipAPI.Domain.Entities.DenemeFolder.TytFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Domain.Entities.DenemeFolder
{
    public class Konu:BaseEntity
    {
        //public Konu()
        //{
        //    AytDenemes = new List<AytDeneme>();
        //    TytDenemes= new List<TytDeneme>();
        //}
        public string KonuAdi { get; set; }
        public Ders Ders { get; set; }
        public bool IsTyt { get; set; }
        public List<AytDeneme> AytDenemesBos { get; set; } = new();
        public List<AytDeneme> AytDenemesYanlis { get; set; } = new();

        public List<TytDeneme> TytDenemesBos { get; set; } = new();
        public List<TytDeneme> TytDenemesYanlis { get; set; } = new();
        public List<UserKonu> UserKonular{ get; set; }=new();

    }
}
