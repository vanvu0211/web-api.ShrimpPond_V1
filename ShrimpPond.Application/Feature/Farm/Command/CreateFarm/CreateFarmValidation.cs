using FluentValidation;
using ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Command.CreateFarm
{
    public class CreateFarmValidation : AbstractValidator<CreateFarm>
    {
        public CreateFarmValidation() 
        { 
            RuleFor(f=>f.FarmName)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);
            RuleFor(f => f.Address)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);

        }
    }
}
