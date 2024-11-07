using DenemeTakipAPI.Application.Abstraction.Hubs;
using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DenemeTakipAPI.Application.Features.Commands.Tyt.CreateTyt
{
    public class CreateTytCommandHandler : IRequestHandler<CreateTytCommandRequest, CreateTytCommandResponse>
    {
        readonly ITytService _tytService;
        readonly ITytHubService _tytHubService;

        public CreateTytCommandHandler(ITytService tytService, ITytHubService tytHubService)
        {
            _tytService = tytService;
            _tytHubService = tytHubService;
        }
        public async Task<CreateTytCommandResponse> Handle(CreateTytCommandRequest request, CancellationToken cancellationToken)
        {
            await _tytService.CreateTytAsync(new()
            {
                TurkceDogru = request.TurkceDogru,
                TurkceYanlis = request.TurkceYanlis,
                MatematikDogru=request.MatematikDogru,
                MatematikYanlis=request.MatematikYanlis,
                SosyalDogru= request.SosyalDogru,
                SosyalYanlis= request.SosyalYanlis,
                FenDogru=request.FenDogru,
                FenYanlis=request.FenYanlis,
                YanlisKonularId=request.YanlisKonularId,
                BosKonularId=request.BosKonularId,
            });

            //await _tytHubService.TytAddedMessage("Tyt denemesi eklendi.");
            return new();
        }
    }
}
