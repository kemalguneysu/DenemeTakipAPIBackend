using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.ToDoElement.UpdateToDoElement
{
    public class UpdateToDoElementCommandHandler : IRequestHandler<UpdateToDoElementCommandRequest, UpdateToDoElementCommandResponse>
    {
        readonly IToDoElementService _toDoElementService;

        public UpdateToDoElementCommandHandler(IToDoElementService toDoElementService)
        {
            _toDoElementService = toDoElementService;
        }

        public async Task<UpdateToDoElementCommandResponse> Handle(UpdateToDoElementCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _toDoElementService.UpdateToDoElementAsync(request.ToDoId,request.IsCompleted,request.ToDoTitle);
            return new()
            {
                Message = response.Message,
                Succeeded = response.Succeeded,
            };
        }
    }
}
