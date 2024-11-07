using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DenemeTakipAPI.Application.Features.Queries.Ayt.GetAllAyt
{
    public class GetAllAytQueryRequest:IRequest<GetAllAytQueryResponse>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        [FromQuery(Name = "orderByAndDirection")]
        public List<string>? OrderByAndDirections { get; set; } = new List<string>();
    }
}
