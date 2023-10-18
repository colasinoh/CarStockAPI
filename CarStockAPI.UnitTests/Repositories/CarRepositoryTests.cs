using Car_Stock_API.Data;
using Car_Stock_API.Repositories;
using Car_Stock_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarStockAPI.UnitTests.Repositories
{
    [TestFixture]
    public class CarRepositoryTests
    {
        private ICarRepository _repository;

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public async Task CarRepository_IfExists_ThenReturnRecord()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "MockCarStock")
                .Options;

            using (var context = new DataContext(options))
            {
                context.Cars.Add(new Car("0", "Ford", "Everest", 2016));
                context.SaveChanges();

                _repository = new CarRepository(context);

                var result = await _repository.GetAsync("ford");

                Assert.That(result.Count(), Is.EqualTo(1));
            }
        }

        [Test]
        public async Task CarRepository_IfGet_ThenReturnAll()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "MockCarStock")
                .Options;

            using (var context = new DataContext(options))
            {
                context.Cars.Add(new Car("1", "Toyota", "Hi Ace", 2019));
                context.Cars.Add(new Car("2", "Toyota", "Vios", 2016));
                context.Cars.Add(new Car("3", "Isuzu", "Crosswind", 2013));
                context.Cars.Add(new Car("4", "Toyota", "Wigo", 2015));
                context.SaveChanges();

                _repository = new CarRepository(context);

                var result = await _repository.Get();

                Assert.That(result.Count(), Is.EqualTo(5));
            }
        }
    }
}
