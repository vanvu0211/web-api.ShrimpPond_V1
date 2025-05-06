using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Commands.HarvestPond
{
    public class HarvestPondValidation : AbstractValidator<HarvestPond>
    {
        public HarvestPondValidation()
        {
            RuleFor(p => p.pondId)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);
            RuleFor(p => p.size)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull();
            RuleFor(p => p.amount)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull();
            RuleFor(p => p.harvestDate)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull();
            //RuleFor(p => p.Unit)
            //   .NotEmpty().WithMessage("{property} is required")
            //   .NotNull();

        }
    }
}
