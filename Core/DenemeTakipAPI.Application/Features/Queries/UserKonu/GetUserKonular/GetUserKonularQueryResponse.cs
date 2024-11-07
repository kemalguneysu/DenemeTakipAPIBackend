using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.UserKonu.GetUserKonular
{
    public class GetUserKonularQueryResponse
    {
        public int TotalCount { get; set; }
        public object UserKonular { get; set; }
    }
}
