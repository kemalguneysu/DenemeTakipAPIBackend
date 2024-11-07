using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Queries.Users.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQueryRequest, GetUserByIdQueryResponse>
    {
        readonly IUserService _userService;

        public GetUserByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetUserByIdQueryResponse> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var data=await _userService.GetUserById(request.UserId);
            if (data == null)
                throw new Exception("Kullanıcı bulunamadı.");
            return new()
            {
                UserId=data.UserId,
                UserName=data.UserName,
                Email=data.Email,
                EmailConfirmed=data.EmailConfirmed,
            };
        }
    }
}
