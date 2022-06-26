using AutoMapper;
using Countries__Cities.Core.Concrete;
using Countries__Cities.Core.DTOs;
using Countries__Cities.WEB.Filters;
using Countries__Cities.WEB.Models;
using Countries__Cities.WEB.Services;
using Countries__Cities.WEB.Validations;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Countries__Cities.WEB.Controllers
{
    public class CityController : Controller
    {

        private readonly APICityService _cityService;
        private readonly APICountryService _countryService;
        private readonly IMapper _mapper;

        public CityController(APICityService cityService, APICountryService countryService, IMapper mapper)
        {
            _cityService = cityService;
            _countryService = countryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _cityService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> SaveCity()
        {

            var countries = await _countryService.GetAllAsync();

            ViewBag.countries = new SelectList(countries, "Id", "name");

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> SaveCity(CityViewModel cityViewModel)
        {

            string[] validFileTypes = { "gif", "jpg", "png" };
            bool isValidType = false;

            CityViewModelValidation cityViewModelValidator = new CityViewModelValidation();
            ValidationResult validationResult = cityViewModelValidator.Validate(cityViewModel);

            if (validationResult.IsValid)
            {

                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(cityViewModel.image.FileName);

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
                var saveLocation = resource + "/wwwroot/cityimage/" + imagename;
                var stream = new FileStream(saveLocation, FileMode.Create);
                await cityViewModel.image.CopyToAsync(stream);

                cityViewModel.imageUrl = imagename;


                await _cityService.SaveAsync(_mapper.Map<CityDTO>(cityViewModel));


                return RedirectToAction(nameof(Index));

            }
            else
            {

                foreach (var item in validationResult.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }

            }


            var countries = await _countryService.GetAllAsync();

            ViewBag.countries = new SelectList(countries, "Id", "name");

            return View();

        }

        [ServiceFilter(typeof(NotFoundFilter<City>))]
        [HttpGet]
        public async Task<IActionResult> UpdateCity(int id)
        {
            var city = await _cityService.GetByIdAsync(id);

            var countries = await _countryService.GetAllAsync();

            ViewBag.imageUrl = city.imageUrl;

            ViewBag.countries = new SelectList(countries, "Id", "name");

            return View(city);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCity(CityViewModel cityViewModel)
        {

            string[] validFileTypes = { "gif", "jpg", "png" };
            bool isValidType = false;

            CityViewModelValidation cityViewModelValidator = new CityViewModelValidation();
            ValidationResult validationResult = cityViewModelValidator.Validate(cityViewModel);

            if (validationResult.IsValid)
            {
                if (cityViewModel.image != null)
                {

                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(cityViewModel.image.FileName);

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
                    var saveLocation = resource + "/wwwroot/cityimage/" + imagename;
                    var stream = new FileStream(saveLocation, FileMode.Create);
                    await cityViewModel.image.CopyToAsync(stream);

                    cityViewModel.imageUrl = imagename;

                }


                await _cityService.UpdateAsync(_mapper.Map<CityDTO>(cityViewModel));

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


            var countries = await _countryService.GetAllAsync();

            ViewBag.countries = new SelectList(countries, "Id", "name");

            return View(cityViewModel);

        }

        [ServiceFilter(typeof(NotFoundFilter<City>))]
        public async Task<IActionResult> Remove(int id)
        {
            await _cityService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
