using DenemeTakipAPI.Application.DTOs.Tyt;
using DenemeTakipAPI.Application.Features.Commands.Tyt.UpdateTyt;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Validators.TytValidators
{
    public class UpdateTytValidators:AbstractValidator<UpdateTytCommandRequest>
    {
        public UpdateTytValidators()
        {
            RuleFor(c => c.TurkceDogru)
                .Must(c => c >= 0)
                .Must(c => c <= 40)
                .WithMessage("Türkçe doğru 0 ile 40 arasında olmalıdır.");
            RuleFor(c => c.TurkceYanlis)
                .Must(c => c >= 0)
                .Must(c => c <= 40)
                .WithMessage("Türkçe yanlış 0 ile 40 arasında olmalıdır.");
            RuleFor(c => new { c.TurkceDogru, c.TurkceYanlis })
                .Must(x => x.TurkceDogru + x.TurkceYanlis <= 40)
                .WithMessage("Türkce alanı için toplam 40 soru girilmelidir");

            RuleFor(c => c.MatematikDogru)
                .Must(c => c >= 0)
                .Must(c => c <= 40)
                .WithMessage("Matematik doğru 0 ile 40 arasında olmalıdır");
            RuleFor(c => c.MatematikYanlis)
                .Must(c => c >= 0)
                .Must(c => c <= 40)
                .WithMessage("Matematik yanlış 0 ile 40 arasında olmalıdır.");
            RuleFor(c => new { c.MatematikDogru, c.MatematikYanlis })
                .Must(x => x.MatematikDogru + x.MatematikYanlis <= 40)
                .WithMessage("Matematik alanı için toplam 40 soru girilmelidir");

            RuleFor(c => c.FenDogru)
                .Must(c => c >= 0)
                .Must(c => c <= 20)
                .WithMessage("Fen doğru 0 ile 20 arasında olmalıdır");
            RuleFor(c => c.FenYanlis)
                .Must(c => c >= 0)
                .Must(c => c <= 20)
                .WithMessage("Fen yanlış 0 ile 20 arasında olmalıdır.");
            RuleFor(c => new { c.FenDogru, c.FenYanlis })
                .Must(x => x.FenDogru + x.FenYanlis <= 20)
                .WithMessage("Fen alanı için toplam 20 soru girilmelidir");

            RuleFor(c => c.SosyalDogru)
                .Must(c => c >= 0)
                .Must(c => c <= 20)
                .WithMessage("Sosyal doğru 0 ile 20 arasında olmalıdır");
            RuleFor(c => c.SosyalYanlis)
                .Must(c => c >= 0)
                .Must(c => c <= 20)
                .WithMessage("Sosyal yanlış 0 ile 20 arasında olmalıdır.");

            RuleFor(c => new { c.SosyalDogru, c.SosyalYanlis })
                .Must(x => x.SosyalDogru + x.SosyalYanlis <= 20)
                .WithMessage("Sosyal alanı için toplam 20 soru girilmelidir");

        }
    }
}
