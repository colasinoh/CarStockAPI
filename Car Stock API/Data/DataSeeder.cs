using Car_Stock_API.Models;

namespace Car_Stock_API.Data
{
    public class DataSeeder
    {
        private readonly DataContext _context;

        public DataSeeder(DataContext context)
        {
            this._context = context;
        }

        public void Seed()
        {
            var car = new Car("0", "Ford", "Everest", 2016);

            _context.Add(car);
            _context.SaveChanges();
        }
    }
}
