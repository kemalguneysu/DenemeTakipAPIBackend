using DenemeTakipAPI.Application.DTOs.Ayt;
using DenemeTakipAPI.Application.DTOs.Tyt;
using DenemeTakipAPI.Application.Features.Commands.Ayt.CreateAyt;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Application.Validators.AytValidators
{
    public class CreateAytValidators : AbstractValidator<CreateAytCommandRequest>
    {
        public CreateAytValidators()
        {
            RuleFor(c => c.MatematikDogru)
                .Must(c => c >= 0)
                .Must(c => c <= 40)
                .WithMessage("Matematik doğru 0 ile 40 arasında olmalıdır");
            RuleFor(c => c.MatematikYanlis)
                .Must(c => c >= 0)
                .Must(c => c <= 40)
                .WithMessage("Matematik yanlış 0 ile 40 arasında olmalıdır");
            RuleFor(c => new { c.MatematikDogru, c.MatematikYanlis })
                .Must(x => x.MatematikDogru + x.MatematikYanlis <= 40)
                .WithMessage("Matematik alanı için toplam 40 soru girilmelidir");

            RuleFor(c => c.FizikDogru)
                .Must(c => c >= 0)
                .Must(c => c <= 14)
                .WithMessage("Fizik doğru 0 ile 14 arasında olmalıdır");
            RuleFor(c => c.FizikYanlis)
                .Must(c => c >= 0)
                .Must(c => c <= 14)
                .WithMessage("Fizik yanlış 0 ile 14 arasında olmalıdır");
            RuleFor(c => new { c.FizikDogru, c.FizikYanlis })
                .Must(x => x.FizikDogru + x.FizikYanlis <= 14)
                .WithMessage("Fizik alanı için toplam 14 soru girilmelidir");

            RuleFor(c => c.KimyaDogru)
                .Must(c => c >= 0)
                .Must(c => c <= 13)
                .WithMessage("Kimya doğru 0 ile 13 arasında olmalıdır");
            RuleFor(c => c.KimyaYanlis)
                .Must(c => c >= 0)
                .Must(c => c <= 13)
                .WithMessage("Kimya yanlış 0 ile 13 arasında olmalıdır");
            RuleFor(c => new { c.KimyaDogru, c.KimyaYanlis })
                .Must(x => x.KimyaDogru + x.KimyaYanlis <= 13)
                .WithMessage("Kimya alanı için toplam 13 soru girilmelidir");

            RuleFor(c => c.BiyolojiDogru)
               .Must(c => c >= 0)
               .Must(c => c <= 13)
               .WithMessage("Biyoloji doğru 0 ile 13 arasında olmalıdır");
            RuleFor(c => c.BiyolojiYanlis)
                .Must(c => c >= 0)
                .Must(c => c <= 13)
                .WithMessage("Biyoloji yanlış 0 ile 13 arasında olmalıdır");
            RuleFor(c => new { c.BiyolojiDogru, c.BiyolojiYanlis })
                .Must(x => x.BiyolojiDogru + x.BiyolojiYanlis <= 13)
                .WithMessage("Biyoloji alanı için toplam 13 soru girilmelidir");

            RuleFor(c => c.EdebiyatDogru)
               .Must(c => c >= 0)
               .Must(c => c <= 24)
               .WithMessage("Edebiyat doğru 0 ile 24 arasında olmalıdır");
            RuleFor(c => c.EdebiyatYanlis)
                .Must(c => c >= 0)
                .Must(c => c <= 24)
                .WithMessage("Edebiyat yanlış 0 ile 24 arasında olmalıdır");
            RuleFor(c => new { c.EdebiyatDogru, c.EdebiyatYanlis })
                .Must(x => x.EdebiyatDogru + x.EdebiyatYanlis <= 24)
                .WithMessage("Edebiyat alanı için toplam 24 soru girilmelidir");

            RuleFor(c => c.Tarih1Dogru)
               .Must(c => c >= 0)
               .Must(c => c <= 10)
               .WithMessage("Tarih1 doğru 0 ile 10 arasında olmalıdır");
            RuleFor(c => c.Tarih1Yanlis)
                .Must(c => c >= 0)
                .Must(c => c <= 10)
                .WithMessage("Tarih1 yanlış 0 ile 10 arasında olmalıdır");
            RuleFor(c => new { c.Tarih1Dogru, c.Tarih1Yanlis })
                .Must(x => x.Tarih1Dogru + x.Tarih1Yanlis <= 10)
                .WithMessage("Tarih1 alanı için toplam 10 soru girilmelidir");

            RuleFor(c => c.Cografya1Dogru)
               .Must(c => c >= 0)
               .Must(c => c <= 6)
               .WithMessage("Coğrafya1 doğru 0 ile 6 arasında olmalıdır");
            RuleFor(c => c.Cografya1Yanlis)
                .Must(c => c >= 0)
                .Must(c => c <= 6)
                .WithMessage("Coğrafya1 yanlış 0 ile 6 arasında olmalıdır");
            RuleFor(c => new { c.Cografya1Dogru, c.Cografya1Yanlis })
                .Must(x => x.Cografya1Dogru + x.Cografya1Yanlis <= 6)
                .WithMessage("Coğrafya1 alanı için toplam 6 soru girilmelidir");

            RuleFor(c => c.Tarih2Dogru)
               .Must(c => c >= 0)
               .Must(c => c <= 11)
               .WithMessage("Tarih2 doğru 0 ile 11 arasında olmalıdır");
            RuleFor(c => c.Tarih2Yanlis)
                .Must(c => c >= 0)
                .Must(c => c <= 11)
                .WithMessage("Tarih2 yanlış 0 ile 11 arasında olmalıdır");
            RuleFor(c => new { c.Tarih2Dogru, c.Tarih2Yanlis })
                .Must(x => x.Tarih2Dogru + x.Tarih2Yanlis <= 11)
                .WithMessage("Tarih2 alanı için toplam 11 soru girilmelidir");

            RuleFor(c => c.Cografya2Dogru)
               .Must(c => c >= 0)
               .Must(c => c <= 11)
               .WithMessage("Coğrafya2 doğru 0 ile 11 arasında olmalıdır");
            RuleFor(c => c.Cografya2Yanlis)
                .Must(c => c >= 0)
                .Must(c => c <= 11)
                .WithMessage("Coğrafya2 yanlış 0 ile 11 arasında olmalıdır");
            RuleFor(c => new { c.Cografya2Dogru, c.Cografya2Yanlis })
                .Must(x => x.Cografya2Dogru + x.Cografya2Yanlis <= 11)
                .WithMessage("Coğrafya2 alanı için toplam 11 soru girilmelidir");

            RuleFor(c => c.FelsefeDogru)
               .Must(c => c >= 0)
               .Must(c => c <= 12)
               .WithMessage("Felsefe doğru 0 ile 12 arasında olmalıdır");
            RuleFor(c => c.FelsefeYanlis)
                .Must(c => c >= 0)
                .Must(c => c <= 12)
                .WithMessage("Felsefe yanlış 0 ile 12 arasında olmalıdır");
            RuleFor(c => new { c.FelsefeDogru, c.FelsefeYanlis })
                .Must(x => x.FelsefeDogru + x.FelsefeYanlis <= 12)
                .WithMessage("Felsefe alanı için toplam 12 soru girilmelidir");

            RuleFor(c => c.DinDogru)
               .Must(c => c >= 0)
               .Must(c => c <= 6)
               .WithMessage("Din doğru 0 ile 6 arasında olmalıdır");
            RuleFor(c => c.DinYanlis)
                .Must(c => c >= 0)
                .Must(c => c <= 6)
                .WithMessage("Din yanlış 0 ile 6 arasında olmalıdır");
            RuleFor(c => new { c.DinDogru, c.DinYanlis })
                .Must(x => x.DinDogru + x.DinYanlis <= 6)
                .WithMessage("Din alanı için toplam 6 soru girilmelidir");

            RuleFor(c => c.DilDogru)
               .Must(c => c >= 0)
               .Must(c => c <= 80)
               .WithMessage("Dil doğru 0 ile 80 arasında olmalıdır");
            RuleFor(c => c.DilYanlis)
                .Must(c => c >= 0)
                .Must(c => c <= 80)
                .WithMessage("Dil yanlış 0 ile 80 arasında olmalıdır");
            RuleFor(c => new { c.DilDogru, c.DilYanlis })
                .Must(x => x.DilDogru + x.DilYanlis <= 80)
                .WithMessage("Dil alanı için toplam 80 soru girilmelidir");


        }
        
    }
}
