using FluentValidation;
using ShrimpPond.Application.Feature.Farm.Command.CreateFarm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Machine.Command.CreateMachine
{
    class CreateMachineValidation : AbstractValidator<CreateMachine>
    {
        public CreateMachineValidation()
        {
            RuleFor(f => f.machineName)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);
          
        }
    }
}
