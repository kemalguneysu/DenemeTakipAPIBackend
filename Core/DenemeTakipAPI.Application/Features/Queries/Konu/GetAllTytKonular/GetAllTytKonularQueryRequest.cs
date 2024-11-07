using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DenemeTakipAPI.Application.Features.Queries.Konu.GetAllTytKonular
{
    public class GetAllTytKonularQueryRequest:IRequest<GetAllTytKonularQueryResponse>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        [FromQuery(Name = "dersId")]
        public List<string> DersIds { get; set; }= new List<string>();
        public string? KonuAdi { get; set; }

    }
}
