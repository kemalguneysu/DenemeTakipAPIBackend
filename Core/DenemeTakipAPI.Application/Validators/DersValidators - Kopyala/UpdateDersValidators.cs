using DenemeTakipAPI.Application.Features.Commands.Ders.CreateKonu;
using DenemeTakipAPI.Application.Features.Commands.Ders.UpdateDers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Validators.DersValidators___Kopyala
{
    public class UpdateDersValidators : AbstractValidator<UpdateDersCommandRequest>
    {
        public UpdateDersValidators()
        {
            RuleFor(u => u.DersAdi)
                .NotEmpty()
                .WithMessage("Ders adı alanı boş geçilmemelidir.");
        }
    }
}
