using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace DenemeTakipAPI.Application.Features.Queries.Konu.GetAllKonularPaginated
{
    public class GetAllKonularPaginatedQueryRequest:IRequest<GetAllKonularPaginatedQueryResponse>
    {
        public int? Size { get; set; }
        public int? Page { get; set; }
        [FromQuery(Name = "dersId")]
        public List<string> DersIds { get; set; } = new List<string>();
        public string? KonuOrDersAdi { get; set; }
        public bool? IsTyt { get; set; }
    }
}