using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.Konu.DeleteKonu
{
    public class DeleteKonuCommandHandler : IRequestHandler<DeleteKonuCommandRequest, DeleteKonuCommandResponse>
    {
        readonly IKonuService _service;

        public DeleteKonuCommandHandler(IKonuService service)
        {
            _service = service;
        }

        public async Task<DeleteKonuCommandResponse> Handle(DeleteKonuCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.DeleteKonu(request.Ids);
            return new()
            {
                Succeeded = result.Succeeded,
                Message = result.Message,
            };
        }
    }
}
