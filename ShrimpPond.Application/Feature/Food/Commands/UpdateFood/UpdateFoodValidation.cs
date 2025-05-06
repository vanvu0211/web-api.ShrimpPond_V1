using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Food.Commands.UpdateFood
{
    public class UpdateFoodValidation : AbstractValidator<UpdateFood>
    {
        public UpdateFoodValidation()
        {
            
             
            RuleFor(p => p.newName)
               .NotEmpty().WithMessage("{property} is required")
               .NotNull()
               .MaximumLength(100);
        }
    }
}
