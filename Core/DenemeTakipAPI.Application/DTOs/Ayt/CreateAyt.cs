using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.DTOs.Ayt
{
    public class CreateAyt
    {
        public int MatematikDogru { get; set; } = 0;
        public int MatematikYanlis { get; set; } = 0;

        public int FizikDogru { get; set; } = 0;
        public int FizikYanlis { get; set; } = 0;

        public int KimyaDogru { get; set; } = 0;
        public int KimyaYanlis { get; set; } = 0;

        public int BiyolojiDogru { get; set; } = 0;
        public int BiyolojiYanlis { get; set; } = 0;



        public int EdebiyatDogru { get; set; } = 0;
        public int EdebiyatYanlis { get; set; } = 0;

        public int Cografya1Dogru { get; set; } = 0;
        public int Cografya1Yanlis { get; set; } = 0;

        public int Tarih1Dogru { get; set; } = 0;
        public int Tarih1Yanlis { get; set; } = 0;



        public int Cografya2Dogru { get; set; } = 0;
        public int Cografya2Yanlis { get; set; } = 0;

        public int Tarih2Dogru { get; set; } = 0;
        public int Tarih2Yanlis { get; set; } = 0;

        public int DinDogru { get; set; } = 0;
        public int DinYanlis { get; set; } = 0;

        public int FelsefeDogru { get; set; } = 0;
        public int FelsefeYanlis { get; set; } = 0;

        public int DilDogru { get; set; } = 0;
        public int DilYanlis { get; set; } = 0;

        public List<string>? YanlisKonularId { get; set; } = new List<string>();
        public List<string>? BosKonularId { get; set; } = new List<string>();

    }
}
