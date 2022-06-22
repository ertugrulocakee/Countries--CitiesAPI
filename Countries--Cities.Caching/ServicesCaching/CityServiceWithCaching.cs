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
    public class CityServiceWithCaching : ICityService
    {

        private const string CacheCityKey = "citiesCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ICityRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CityServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, ICityRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _repository = repository;
            _unitOfWork = unitOfWork;


            if (!_memoryCache.TryGetValue(CacheCityKey, out _))
            {

                _memoryCache.Set(CacheCityKey, _repository.GetCitiesWithCountryAsync().Result);

            }
        }

        public async Task<City> AddAsync(City entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllCitiesAsync();
            return entity;
        }

        public async Task<IEnumerable<City>> AddRangeAsync(IEnumerable<City> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllCitiesAsync();

            return entities;
        }

        public Task<bool> AnyAsync(Expression<Func<City, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<City>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<City>>(CacheCityKey));
        }

        public Task<City> GetByIdAsync(int id)
        {
            var city = _memoryCache.Get<List<City>>(CacheCityKey).FirstOrDefault(x => x.Id == id);

            if (city == null)
            {

                throw new NotFoundException($"{typeof(City).Name}({id}) not found!");
            }


            return Task.FromResult(city);
        }

        public async Task RemoveAsync(City entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllCitiesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<City> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllCitiesAsync();
        }

        public Task<CustomResponseDTO<List<CityWithCountryDTO>>> TGetCitiesWithCountryAsync()
        {
            var cities = _memoryCache.Get<IEnumerable<City>>(CacheCityKey);

            var cityDTOs = _mapper.Map<List<CityWithCountryDTO>>(cities);

            return Task.FromResult(CustomResponseDTO<List<CityWithCountryDTO>>.Success(200, cityDTOs));

        }

        public Task<CustomResponseDTO<CityWithCountryDTO>> TGetCityWithCountryAsync(int CityID)
        {
            var city = _memoryCache.Get<IEnumerable<City>>(CacheCityKey).FirstOrDefault(x => x.Id == CityID);

            var cityDTO = _mapper.Map<CityWithCountryDTO>(city);

            return Task.FromResult(CustomResponseDTO<CityWithCountryDTO>.Success(200, cityDTO));

        }

        public async Task UpdateAsync(City entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllCitiesAsync();
        }

        public IQueryable<City> Where(Expression<Func<City, bool>> expression)
        {
            return _memoryCache.Get<List<City>>(CacheCityKey).Where(expression.Compile()).AsQueryable();
        }

        public async Task CacheAllCitiesAsync()
        {
            _memoryCache.Set(CacheCityKey, await _repository.GetCitiesWithCountryAsync());
        }
    }
}
