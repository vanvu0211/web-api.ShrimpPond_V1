using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Food.Commands.CreateNewFood
{
    public class CreateNewFoodValidation: AbstractValidator<CreateNewFood>
    {
        public CreateNewFoodValidation() 
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);
            
        }
    }
}
