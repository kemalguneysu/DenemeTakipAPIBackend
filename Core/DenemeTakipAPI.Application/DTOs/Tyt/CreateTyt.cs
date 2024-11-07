using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.DTOs.Tyt
{
    public class CreateTyt
    {
        public int TurkceDogru { get; set; } = 0;
        public int TurkceYanlis { get; set; } = 0;

        public int MatematikDogru { get; set; } = 0;
        public int MatematikYanlis { get; set; } = 0;

        public int FenDogru { get; set; } = 0;
        public int FenYanlis { get; set; } = 0;

        public int SosyalDogru { get; set; } = 0;
        public int SosyalYanlis { get; set; } = 0;

        public List<string>? YanlisKonularId { get; set; } = new List<string>();
        public List<string>? BosKonularId { get; set; } = new List<string>();
    }
}
