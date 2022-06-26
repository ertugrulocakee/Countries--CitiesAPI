using Countries__Cities.WEB.Models;
using FluentValidation;

namespace Countries__Cities.WEB.Validations
{
    public class CountryViewModelValidation : AbstractValidator<CountryViewModel>
    {
        public CountryViewModelValidation()
        {

            RuleFor(x => x.name).NotNull().WithErrorCode("{PropertyName} is required!");
            RuleFor(x => x.name).NotEmpty().WithErrorCode("{PropertyName} is required!");
           
            RuleFor(x => x.capital).NotNull().WithErrorCode("{PropertyName} is required!");
            RuleFor(x => x.capital).NotEmpty().WithErrorCode("{PropertyName} is required!");

            RuleFor(x => x.population).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0!");


        }

    }
}
