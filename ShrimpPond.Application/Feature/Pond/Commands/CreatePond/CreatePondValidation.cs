using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond
{
    public class CreatePondValidation :AbstractValidator<CreatePond>
    {
        public CreatePondValidation() 
        {
            RuleFor(p => p.pondId)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);
            RuleFor(p => p.pondTypeId)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);
            RuleFor(p => p.deep)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull();
            RuleFor(p => p.diameter)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull();

        }
    }
}
