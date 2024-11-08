using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.ToDoElement.CreateToDoElement
{
    public class CreateToDoElementCommandHandler : IRequestHandler<CreateToDoElementCommandRequest, CreateToDoElementCommandResponse>
    {
        readonly IToDoElementService _toDoElementService;

        public CreateToDoElementCommandHandler(IToDoElementService toDoElementService)
        {
            _toDoElementService = toDoElementService;
        }

        public async Task<CreateToDoElementCommandResponse> Handle(CreateToDoElementCommandRequest request, CancellationToken cancellationToken)
        {
            await _toDoElementService.CreateToDoElementAsync(request.ToDoElementTitle,request.ToDoDate,request.IsCompleted);
            return new();
        }
    }
}
