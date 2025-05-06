using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Feeding.Commands.Feeding
{
    public class FoodFeedingValidation : AbstractValidator<FoodFeeding>
    {
        public FoodFeedingValidation()
        {
            RuleFor(p => p.pondId)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);
        }
    }
}
