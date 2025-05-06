using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Commands.LossShrimpUpdate
{
    public class LossShrimpUpdateValidation: AbstractValidator<LossShrimpUpdate>
    {
        public LossShrimpUpdateValidation() 
        {
            RuleFor(p => p.pondId)
               .NotEmpty().WithMessage("{property} is required")
               .NotNull()
               .MaximumLength(100);
            RuleFor(p => p.lossValue)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull();
            RuleFor(p => p.updateDate)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull();
        }
    }
}
