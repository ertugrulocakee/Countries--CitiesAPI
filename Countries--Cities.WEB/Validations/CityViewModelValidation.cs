using Countries__Cities.WEB.Models;
using FluentValidation;

namespace Countries__Cities.WEB.Validations
{
    public class CityViewModelValidation : AbstractValidator<CityViewModel>
    {

        public CityViewModelValidation()
        {

            RuleFor(x => x.name).NotNull().WithErrorCode("{PropertyName} is required!");
            RuleFor(x => x.name).NotEmpty().WithErrorCode("{PropertyName} is required!");

          
            RuleFor(x => x.population).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0!");
            RuleFor(x => x.CountryId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0!");

        }


    }
}
