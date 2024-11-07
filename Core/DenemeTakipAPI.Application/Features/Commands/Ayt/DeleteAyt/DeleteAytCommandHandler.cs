using DenemeTakipAPI.Application.Abstraction.Hubs;
using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.Ayt.DeleteAyt
{
    public class DeleteAytCommandHandler : IRequestHandler<DeleteAytCommandRequest, DeleteAytCommandResponse>
    {
        readonly IAytService _aytService;
        readonly IAytHubService _aytHubService;
        public DeleteAytCommandHandler(IAytService aytService, IAytHubService aytHubService)
        {
            _aytService = aytService;
            _aytHubService = aytHubService;
        }

        public async Task<DeleteAytCommandResponse> Handle(DeleteAytCommandRequest request, CancellationToken cancellationToken)
        {
            var result=await _aytService.DeleteAyt(request.Ids);
            
            return new()
            {
                Succeeded = result.Succeeded,
                Message = result.Message,
            };
        }
    }
}
