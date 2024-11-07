using DenemeTakipAPI.Application.DTOs.UserKonu;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.UserKonu.CreateOrUpdate
{
    public class CreateUserKonuCommandRequest:IRequest<CreateUserKonuCommandResponse>
    {
        public List<string> KonuIds { get; set; } = new();
    }
}
