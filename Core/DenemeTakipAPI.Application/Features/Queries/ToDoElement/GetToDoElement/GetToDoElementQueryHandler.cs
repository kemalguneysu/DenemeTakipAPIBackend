using DenemeTakipAPI.Application.Abstraction.Services;
using DenemeTakipAPI.Application.Features.Queries.ToDoElement.GetToDoElements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.ToDoElement.GetToDoElement
{
    public class GetToDoElementQueryHandler : IRequestHandler<GetToDoElementQueryRequest, GetToDoElementQueryResponse>
    {
        readonly IToDoElementService _toDoElementService;

        public GetToDoElementQueryHandler(IToDoElementService toDoElementService)
        {
            _toDoElementService = toDoElementService;
        }

        public async Task<GetToDoElementQueryResponse> Handle(GetToDoElementQueryRequest request, CancellationToken cancellationToken)
        {
            if (request.ToDoDateStart > request.ToDoDateEnd)
                throw new Exception("Başlangıç tarihi bitiş tarihinden büyük olamaz.");
            
            var response=await _toDoElementService.GetToDoElements(request.ToDoDateStart,request.ToDoDateEnd,request.IsCompleted);
            return new()
            {
                ToDoElements = response.ToDoElements,
                TotalCount = response.TotalCount,
            };
        }
    }
}
