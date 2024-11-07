using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.Ders.UpdateDers
{
    public class UpdateDersCommandHandler:IRequestHandler<UpdateDersCommandRequest, UpdateDersCommandResponse>
    {
        readonly IDersService _service;

        public UpdateDersCommandHandler(IDersService service)
        {
            _service = service;
        }

        public async Task<UpdateDersCommandResponse> Handle(UpdateDersCommandRequest request, CancellationToken cancellationToken)
        {
            await _service.UpdateDers(new()
            {
                DersId = request.DersId,
                DersAdi=request.DersAdi,
                IsTyt=request.IsTyt,
            });
            return new();
        }
    }
}
