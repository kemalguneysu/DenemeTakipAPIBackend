using DenemeTakipAPI.Application.Features.Commands.Ayt.CreateAyt;
using DenemeTakipAPI.Application.Features.Commands.Ders.CreateKonu;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Validators.DersValidators
{
    public class CreateDersValidators : AbstractValidator<CreateDersCommandRequest>
    {
        public CreateDersValidators()
        {
            RuleFor(u => u.DersAdi)
                .NotEmpty()
                .WithMessage("Ders adı alanı boş geçilmemelidir.");
        }
    }
}
