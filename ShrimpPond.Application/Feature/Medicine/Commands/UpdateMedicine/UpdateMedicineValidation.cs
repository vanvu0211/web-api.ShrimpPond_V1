using FluentValidation;
using ShrimpPond.Application.Feature.Food.Commands.UpdateFood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Medicine.Commands.UpdateMedicine
{
    public class UpdateMedicineValidation : AbstractValidator<UpdateMedicine>
    {
        public UpdateMedicineValidation()
        {
            

           
            RuleFor(p => p.newName)
               .NotEmpty().WithMessage("{property} is required")
               .NotNull()
               .MaximumLength(100);
        }
    }
}
