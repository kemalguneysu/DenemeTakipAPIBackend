using DenemeTakipAPI.Application.DTOs.Tyt;
using DenemeTakipAPI.Domain.Entities.DenemeFolder.TytFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Tyt.GetTytById
{
    public class GetTytByIdQueryResponse
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

        public List<KonularAdDers> YanlisKonularAdDers { get; set; }
        public List<KonularAdDers> BosKonularAdDers { get; set; }
    }
}
