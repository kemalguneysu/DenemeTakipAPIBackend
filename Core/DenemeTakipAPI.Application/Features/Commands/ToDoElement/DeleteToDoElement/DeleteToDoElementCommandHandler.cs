using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.ToDoElement.DeleteToDoElement
{
    public class DeleteToDoElementCommandHandler : IRequestHandler<DeleteToDoElementCommandRequest, DeleteToDoElementCommandResponse>
    {
        readonly IToDoElementService _toDoElementService;

        public DeleteToDoElementCommandHandler(IToDoElementService toDoElementService)
        {
            _toDoElementService = toDoElementService;
        }

        public async Task<DeleteToDoElementCommandResponse> Handle(DeleteToDoElementCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _toDoElementService.DeleteToDoElementAsync(request.ToDoId);
            return new()
            {
                Message = response.Message,
                Succeeded = response.Succeeded
            };
        }
    }
}
