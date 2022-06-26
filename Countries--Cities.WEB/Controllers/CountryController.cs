using AutoMapper;
using Countries__Cities.Core.Concrete;
using Countries__Cities.Core.DTOs;
using Countries__Cities.WEB.Filters;
using Countries__Cities.WEB.Models;
using Countries__Cities.WEB.Services;
using Countries__Cities.WEB.Validations;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Countries__Cities.WEB.Controllers
{
    public class CountryController : Controller
    {

        private readonly APICountryService _countryService;
        private readonly IMapper _mapper;

        public CountryController(APICountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _countryService.GetAllAsync());
        }


        [HttpGet]
        public IActionResult SaveCountry()
        {

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> SaveCountry(CountryViewModel countryViewModel)
        {

            string[] validFileTypes = { "gif", "jpg", "png" };
            bool isValidType = false;

            CountryViewModelValidation countryViewModelValidator = new CountryViewModelValidation();
            ValidationResult validationResult = countryViewModelValidator.Validate(countryViewModel);

            if (validationResult.IsValid)
            {

                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(countryViewModel.image.FileName);

                for (int i = 0; i < validFileTypes.Length; i++)
                {
                    if (extension == "." + validFileTypes[i])
                    {
                        isValidType = true;
                        break;
                    }
                }

                if (!isValidType)
                {
                    ViewBag.Message = "Lutfen png,jpg ve gif dosyasi yukleyin!";

                    return View();
                }

                var imagename = Guid.NewGuid() + extension;
                var saveLocation = resource + "/wwwroot/countryimage/" + imagename;
                var stream = new FileStream(saveLocation, FileMode.Create);
                await countryViewModel.image.CopyToAsync(stream);

                countryViewModel.imageUrl = imagename;

                await _countryService.SaveAsync(_mapper.Map<CountryDTO>(countryViewModel));


                return RedirectToAction(nameof(Index));

            }
            else
            {

                foreach (var item in validationResult.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
   
                }

            }

            return View();

        }

        [ServiceFilter(typeof(NotFoundFilter<Country>))]
        [HttpGet]
        public async Task<IActionResult> UpdateCountry(int id)
        {
            var country = await _countryService.GetByIdAsync(id);

            ViewBag.imageUrl = country.imageUrl;

            return View(country);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCountry(CountryViewModel countryViewModel)
        {

            string[] validFileTypes = { "gif", "jpg", "png" };
            bool isValidType = false;

            CountryViewModelValidation countryViewModelValidator = new CountryViewModelValidation();
            ValidationResult validationResult = countryViewModelValidator.Validate(countryViewModel);

            if (validationResult.IsValid)
            {

                if (countryViewModel.image != null)
                {

                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(countryViewModel.image.FileName);

                    for (int i = 0; i < validFileTypes.Length; i++)
                    {
                        if (extension == "." + validFileTypes[i])
                        {
                            isValidType = true;
                            break;
                        }
                    }

                    if (!isValidType)
                    {
                        ViewBag.Message = "Lutfen png,jpg ve gif dosyasi yukleyin!";

                        return View();
                    }

                    var imagename = Guid.NewGuid() + extension;
                    var saveLocation = resource + "/wwwroot/countryimage/" + imagename;
                    var stream = new FileStream(saveLocation, FileMode.Create);
                    await countryViewModel.image.CopyToAsync(stream);

                    countryViewModel.imageUrl = imagename;

                }

                await _countryService.UpdateAsync(_mapper.Map<CountryDTO>(countryViewModel));

                return RedirectToAction(nameof(Index));

            }
            else
            {

                foreach (var item in validationResult.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    ViewBag.Message = item.ErrorMessage;

                }

            }


            return View(countryViewModel);

        }

        [ServiceFilter(typeof(NotFoundFilter<Country>))]
        public async Task<IActionResult> Remove(int id)
        {
            await _countryService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }


 
    }
}
