using DenemeTakipAPI.Application.Abstraction.Hubs;
using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.Tyt.DeleteTyt
{
    public class DeleteTytCommandHandler:IRequestHandler<DeleteTytCommandRequest,DeleteTytCommandResponse>
    {
        readonly ITytService _tytService;
        readonly ITytHubService _tytHubService;

        public DeleteTytCommandHandler(ITytService tytService, ITytHubService tytHubService)
        {
            _tytService = tytService;
            _tytHubService = tytHubService;
        }

        public async Task<DeleteTytCommandResponse> Handle(DeleteTytCommandRequest request, CancellationToken cancellationToken)
        {
            var result=await _tytService.DeleteTyt(request.Ids);
          
            return new()
            {
                Succeeded = result.Succeeded,
                Message=result.Message
            };
        }
    }
}
