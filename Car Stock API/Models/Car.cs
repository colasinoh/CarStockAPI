using System.ComponentModel.DataAnnotations;

namespace Car_Stock_API.Models
{
    public class Car
    {
        public Car() { }

        public Car(string id, string make, string model, int year)
        {
            Id = id;
            Make = make;
            Model = model;
            Year = year;
        }

        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Car Make is required")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Car Model is required")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Car Year is required")]
        public int Year { get; set; }
    }
}
