﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Users.GetAllUsers
{
    public class GetAllUsersQueryRequest:IRequest<GetAllUsersQueryResponse>
    {
        public int Size { get; set; }
        public int Page { get; set; }
        public string? NameOrEmail { get; set; }
        public string? IsAdmin { get; set; }
    }
}
