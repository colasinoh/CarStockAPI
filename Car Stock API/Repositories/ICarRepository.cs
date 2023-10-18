using Car_Stock_API.Data;
using Car_Stock_API.Models;

namespace Car_Stock_API.Repositories
{
    public interface ICarRepository
    {
        Task<Car[]> Get();
        Task Create(Car car);
        Task Update(Car car);
        Task<Car[]> GetAsync(string searchTerms);
        Task Delete(string carId);
    }
}
