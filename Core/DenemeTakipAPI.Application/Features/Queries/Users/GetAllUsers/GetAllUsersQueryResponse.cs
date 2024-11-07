using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Users.GetAllUsers
{
    public class GetAllUsersQueryResponse
    {
        public int TotalCount { get; set; }
        public object Users { get; set; }
    }
}
