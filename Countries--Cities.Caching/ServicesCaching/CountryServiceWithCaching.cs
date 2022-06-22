using AutoMapper;
using Countries__Cities.Core.Concrete;
using Countries__Cities.Core.DTOs;
using Countries__Cities.Core.Repository;
using Countries__Cities.Core.Service;
using Countries__Cities.Core.UnitOfWorks;
using Countries__Cities.Service.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Countries__Cities.Caching.ServicesCaching
{
    public class CountryServiceWithCaching : ICountryService
    {
        private readonly IMemoryCache _cache;
        private readonly ICountryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const string CacheCountryKey = "countriesCache";

        public CountryServiceWithCaching(IMemoryCache cache, ICountryRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _cache = cache;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;


            if (!_cache.TryGetValue(CacheCountryKey, out _))
            {

                _cache.Set(CacheCountryKey, _repository.GetCountriesWithCitiesAsync().Result);

            }


        }

        public async Task<Country> AddAsync(Country entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllCountriesAsync();
            return entity;
        }

        public async Task<IEnumerable<Country>> AddRangeAsync(IEnumerable<Country> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllCountriesAsync();

            return entities;
        }

        public Task<bool> AnyAsync(Expression<Func<Country, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Country>> GetAllAsync()
        {
            return Task.FromResult(_cache.Get<IEnumerable<Country>>(CacheCountryKey));

        }

        public Task<Country> GetByIdAsync(int id)
        {
            var country = _cache.Get<List<Country>>(CacheCountryKey).FirstOrDefault(x => x.Id == id);

            if (country == null)
            {

                throw new NotFoundException($"{typeof(Country).Name}({id}) not found!");
            }


            return Task.FromResult(country);
        }

        public async Task RemoveAsync(Country entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllCountriesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Country> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllCountriesAsync();
        }

        public Task<CustomResponseDTO<List<CountryWithCityDTO>>> TGetCountriesWithCityAsync()
        {
            var countries = _cache.Get<IEnumerable<Country>>(CacheCountryKey);

            var countriesWithCityDTOs = _mapper.Map<List<CountryWithCityDTO>>(countries);

            return Task.FromResult(CustomResponseDTO<List<CountryWithCityDTO>>.Success(200, countriesWithCityDTOs));

        }

        public Task<CustomResponseDTO<CountryWithCityDTO>> TGetCountryWithCityAsync(int CountryID)
        {
            var country = _cache.Get<IEnumerable<Country>>(CacheCountryKey).FirstOrDefault(x => x.Id == CountryID);

            var countryWithCityDTO = _mapper.Map<CountryWithCityDTO>(country);

            return Task.FromResult(CustomResponseDTO<CountryWithCityDTO>.Success(200, countryWithCityDTO));
        }

        public async Task UpdateAsync(Country entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllCountriesAsync();
        }

        public IQueryable<Country> Where(Expression<Func<Country, bool>> expression)
        {
            return _cache.Get<List<Country>>(CacheCountryKey).Where(expression.Compile()).AsQueryable();
        }

        public async Task CacheAllCountriesAsync()
        {

            _cache.Set(CacheCountryKey, await _repository.GetCountriesWithCitiesAsync());

        }

    }
}
