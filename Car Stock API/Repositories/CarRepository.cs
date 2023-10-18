using Car_Stock_API.Data;
using Car_Stock_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Car_Stock_API.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly DataContext _dataContext;

        public CarRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public Task<Car[]> Get()
        {
            return _dataContext.Cars.ToArrayAsync();
        }

        public async Task Create(Car car)
        {
            if (_dataContext.Cars.FirstOrDefault().Id == car.Id)
            {
                return;
            }

            await _dataContext.AddAsync(car);
            await _dataContext.SaveChangesAsync();
        }

        public async Task Update(Car car)
        {
            var existingrecord = await _dataContext.Cars.FirstOrDefaultAsync(x => x.Id == car.Id);

            if (existingrecord == null)
                return;

            if (existingrecord.Id == car.Id)
            {
                existingrecord.Make = car.Make;
                existingrecord.Model = car.Model;
                existingrecord.Year = car.Year;

                await _dataContext.SaveChangesAsync();
            }
        }

        public Task<Car[]> GetAsync(string searchTerms)
        {
            var result = _dataContext.Cars.Where(c => c.Model.ToLower().Equals(searchTerms.ToLower()) || c.Make.ToLower().Equals(searchTerms.ToLower())).ToArrayAsync();

            return result;
        }

        public async Task Delete(string carId)
        {
            var record = await _dataContext.Cars.FirstOrDefaultAsync(x => x.Id == carId);

            if (record == null)
                return;

            if (record.Id == carId)
            {
                _dataContext.Cars.Remove(record);
                _dataContext.SaveChanges();
            }
        }
    }
}
