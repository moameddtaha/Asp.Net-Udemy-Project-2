using System.ComponentModel.DataAnnotations;

namespace CitiesManager.WebAPI.Modles
{
    public class City
    {
        [Key]
        public Guid CityId { get; set; }

        [Required(ErrorMessage = "City Name can't be blank")]
        public string? CityName { get; set; }
    }
}
