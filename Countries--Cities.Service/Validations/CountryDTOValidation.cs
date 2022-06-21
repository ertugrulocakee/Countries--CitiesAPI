using Countries__Cities.Core.Concrete;
using Countries__Cities.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries__Cities.Service.Validations
{
    public class CountryDTOValidation : AbstractValidator<CountryDTO>
    {

        public CountryDTOValidation() {

            RuleFor(x => x.name).NotNull().WithErrorCode("{PropertyName} is required!");
            RuleFor(x => x.name).NotEmpty().WithErrorCode("{PropertyName} is required!");

            RuleFor(x => x.imageUrl).NotNull().WithErrorCode("{PropertyName} is required!");
            RuleFor(x => x.imageUrl).NotEmpty().WithErrorCode("{PropertyName} is required!");

            RuleFor(x => x.capital).NotNull().WithErrorCode("{PropertyName} is required!");
            RuleFor(x => x.capital).NotEmpty().WithErrorCode("{PropertyName} is required!");

            RuleFor(x => x.population).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0!");
          

        }
    }
}
