using DenemeTakipAPI.Application.Features.Commands.Konu.CreateKonu;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Validators.KonuValidators
{
    public class CreateKonuValidators:AbstractValidator<CreateKonuCommandRequest>
    {
        public CreateKonuValidators()
        {
            RuleFor(u => u.KonuAdi)
                .NotEmpty()
                .WithMessage("Konu adı alanı boş geçilmemelidir.");

            RuleFor(u=>u.DersId)
                .NotEmpty()
                .WithMessage("Her konu bir adet derse ait olmalıdır.");
        }
    }
}
