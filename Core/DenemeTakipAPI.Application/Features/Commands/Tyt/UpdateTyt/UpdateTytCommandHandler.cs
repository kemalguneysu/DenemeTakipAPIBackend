using DenemeTakipAPI.Application.Abstraction.Hubs;
using DenemeTakipAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Features.Commands.Tyt.UpdateTyt
{
    public class UpdateTytCommandHandler : IRequestHandler<UpdateTytCommandRequest, UpdateTytCommandResponse>
    {
        readonly ITytService _tytService;
        readonly ITytHubService _tytHubService;

        public UpdateTytCommandHandler(ITytService tytService, ITytHubService tytHubService)
        {
            _tytService = tytService;
            _tytHubService = tytHubService;
        }

        public async Task<UpdateTytCommandResponse> Handle(UpdateTytCommandRequest request, CancellationToken cancellationToken)
        {
            await _tytService.UpdateTytAsync(new()
            {
                TytId=request.TytId,
                TurkceDogru = request.TurkceDogru,
                TurkceYanlis = request.TurkceYanlis,
                MatematikDogru = request.MatematikDogru,
                MatematikYanlis = request.MatematikYanlis,
                SosyalDogru = request.SosyalDogru,
                SosyalYanlis = request.SosyalYanlis,
                FenDogru = request.FenDogru,
                FenYanlis = request.FenYanlis,
                YanlisKonular = request.YanlisKonular,
                BosKonular = request.BosKonular,
            });
            return new();
        }
    }
}
