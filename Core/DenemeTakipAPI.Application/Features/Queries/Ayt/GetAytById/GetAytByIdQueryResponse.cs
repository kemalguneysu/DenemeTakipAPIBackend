using DenemeTakipAPI.Application.DTOs.Tyt;
using DenemeTakipAPI.Domain.Entities.DenemeFolder.AytFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Ayt.GetAytById
{
    public class GetAytByIdQueryResponse
    {
        public string Id { get; set; }
        public int MatematikDogru { get; set; }
        public int MatematikYanlis { get; set; }
        public int FizikDogru { get; set; }
        public int FizikYanlis { get; set; }

        public int KimyaDogru { get; set; }
        public int KimyaYanlis { get; set; }

        public int BiyolojiDogru { get; set; }
        public int BiyolojiYanlis { get; set; }



        public int EdebiyatDogru { get; set; }
        public int EdebiyatYanlis { get; set; }

        public int Tarih1Dogru { get; set; }
        public int Tarih1Yanlis { get; set; }

        public int Cografya1Dogru { get; set; }
        public int Cografya1Yanlis { get; set; }
        public int Tarih2Dogru { get; set; }
        public int Tarih2Yanlis { get; set; }

        public int Cografya2Dogru { get; set; }
        public int Cografya2Yanlis { get; set; }
        public int FelsefeDogru { get; set; }
        public int FelsefeYanlis { get; set; }
        public int DinDogru { get; set; }
        public int DinYanlis { get; set; }
        public int DilDogru { get; set; }
        public int DilYanlis { get; set; }

        public List<KonularAdDers> YanlisKonularAdDers { get; set; }
        public List<KonularAdDers> BosKonularAdDers { get; set; }
    }
}
