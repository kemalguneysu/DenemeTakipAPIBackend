using MediatR;

namespace DenemeTakipAPI.Application.Features.Commands.User.PasswordReset
{
    public class PasswordResetCommandRequest:IRequest<PasswordResetCommandResponse>
    {
        public string emailOrUserName { get; set; }

    }
}