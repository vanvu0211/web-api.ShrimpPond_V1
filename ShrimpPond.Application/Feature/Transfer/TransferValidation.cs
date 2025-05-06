using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Transfer
{
    public class TransferValidation : AbstractValidator<Transfer>
    {
        public TransferValidation()
        {
            RuleFor(p => p.transferPondId)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);
            RuleFor(p => p.originPondId)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull()
                .MaximumLength(100);
            RuleFor(p => p.amount)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull();
            RuleFor(p => p.size)
                .NotEmpty().WithMessage("{property} is required")
                .NotNull();
        }
    }
}
