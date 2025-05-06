using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;


namespace ShrimpPond.Application.Exceptions
{
    public class BadRequestException : Exception
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public BadRequestException(string message) : base(message)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
        public BadRequestException(string message, ValidationResult validationResult) : base(message)
        {
            ValidationErrors = validationResult.ToDictionary();
        }

        public IDictionary<string, string[]> ValidationErrors { get; set; }
    }
}
