using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Practice.Data;
using WebAPI_Practice.Model;

namespace WebAPI_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {

        private readonly CityRepository _cityRepository;

        public CityController(CityRepository cityRepository) => _cityRepository = cityRepository;

        #region GetAllCities
        [HttpGet]
        public IActionResult GetAllCities() {
            var cities = _cityRepository.SelectAllCities();
            return Ok(cities);
        }
        #endregion

        #region GetCityById

        [HttpGet("{id}")]
        public IActionResult GetCityById(int id)
        {
            var city = _cityRepository.GetCityByID(id);
            if (city == null || !city.Any()) // Check if no data is returned
            {
                return NotFound(new { Message = "City not found." });
            }
            return Ok(city);
        }
        #endregion

        #region InsertCity
        [HttpPost]
        public IActionResult InsertCity([FromBody] CityModel city)
        {
            if (city == null)
            {
                return BadRequest();
            }
            bool isInserted = _cityRepository.Insertcity(city);

            if (isInserted)
            {
                return Ok(new { Message = "City Inserted Successfully" });
            }
            return StatusCode(500, "An Error Occurred while Inserting City");
        }
        #endregion

        #region UpdateCity
        [HttpPut("{cityID}")]
        public IActionResult UpdateCity(int cityID, [FromBody] CityModel city)
        {
            if (city == null || cityID != city.CityID)
            {
                return BadRequest();
            }
            var isUpdated = _cityRepository.UpdateCity(city);

            if (isUpdated)
            {
                return Ok(new { Message = "City Updated Successfully" });
            }
            return NoContent();
        }
        #endregion

        #region DeleteCity
        [HttpDelete]
        public IActionResult DeleteCity(int cityID, CityModel city)
        {
            if (city == null)
            {
                return BadRequest();
            }
            bool isDeleted = _cityRepository.DeleteCity(cityID);

            if (isDeleted)
            {
                return Ok(new { Message = "City Deleted Successfully" });
            }
            return StatusCode(500, "An Error Occurred while Deleting City");
        }
        #endregion

        #region CountryDropDownModel
        public class CountryDropDownModel
        {
            public int CountryID { get; set; }
            public string CountryName { get; set; }
        }
        #endregion

    }
}
