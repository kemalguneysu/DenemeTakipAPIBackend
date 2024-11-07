using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Users.GetUserById
{
    public class GetUserByIdQueryRequest:IRequest<GetUserByIdQueryResponse>
    {
        public string UserId { get; set; }
    }
}
