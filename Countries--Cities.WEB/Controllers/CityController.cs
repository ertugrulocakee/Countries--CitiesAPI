using AutoMapper;
using Countries__Cities.Core.DTOs;
using Countries__Cities.WEB.Models;
using Countries__Cities.WEB.Services;
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


            if (ModelState.IsValid)
            {


                await _cityService.SaveAsync(_mapper.Map<CityDTO>(cityViewModel));


                return RedirectToAction(nameof(Index));

            }


            var countries = await _countryService.GetAllAsync();

            ViewBag.countries = new SelectList(countries, "Id", "name");

            return View();

        }

        [HttpGet]
        public async Task<IActionResult> UpdateCity(int id)
        {
            var city = await _cityService.GetByIdAsync(id);

            var countries = await _countryService.GetAllAsync();

            ViewBag.countries = new SelectList(countries, "Id", "name");

            return View(city);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCity(CityViewModel cityViewModel)
        {
            if (ModelState.IsValid)
            {

                await _cityService.UpdateAsync(_mapper.Map<CityDTO>(cityViewModel));

                return RedirectToAction(nameof(Index));

            }

            var countries = await _countryService.GetAllAsync();

            ViewBag.countries = new SelectList(countries, "Id", "name");

            return View(cityViewModel);

        }


        public async Task<IActionResult> Remove(int id)
        {
            await _cityService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
