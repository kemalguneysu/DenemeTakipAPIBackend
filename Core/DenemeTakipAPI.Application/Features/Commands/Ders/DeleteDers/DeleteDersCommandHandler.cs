using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace DenemeTakipAPI.Application.Features.Commands.Ders.DeleteDers
{
    public class DeleteDersCommandHandler : IRequestHandler<DeleteDersCommandRequest, DeleteDersCommandResponse>
    {
        readonly IDersService _service;

        public DeleteDersCommandHandler(IDersService service)
        {
            _service = service;
        }

        public async Task<DeleteDersCommandResponse> Handle(DeleteDersCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _service.DeleteDers(request.Ids);
            return new()
            {
                Succeeded = response.Succeeded,
                Message = response.Message
            };
        }
    }
}
