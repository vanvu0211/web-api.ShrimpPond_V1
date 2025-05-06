using FluentValidation;
using ShrimpPond.Application.Feature.Food.Commands.CreateNewFood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Medicine.Commands.CreateMedicine
{
    public class CreateMedicineValidation: AbstractValidator<CreateMedicine>
    {
        public CreateMedicineValidation() 
        {
            RuleFor(p => p.name)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);

        }
    }
}
