using Microsoft.AspNetCore.Mvc;
using WebAPI_Practice.Data;
using WebAPI_Practice.Model;

namespace WebAPI_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        #region Congiguration
        private readonly CountryRepository _countryRepository;
        public CountryController(CountryRepository countryRepository) => _countryRepository = countryRepository;

        #endregion

        #region GetAllCountries
        [HttpGet]
        public IActionResult GetAllCountries()
        {
            var countries = _countryRepository.SelectAllCountries();
            return Ok(countries);
        }
        #endregion

        #region InsertCountry
        [HttpPost]
        public IActionResult InsertCountry([FromBody] CountryModel country)
        {
            if (country == null)
            {
                return BadRequest();
            }
            bool isInserted = _countryRepository.InsertCountry(country);

            if (isInserted)
            {
                return Ok(new { Message = "Country Inserted Successfully" });
            }
            return StatusCode(500, "An Error Occurred while Inserting Country");
        }
        #endregion

        #region UpdateCountry
        [HttpPut("{countryId}")]
        public IActionResult UpdateCountry(int countryId, [FromBody] CountryModel country)
        {
            if (country == null || countryId != country.CountryID)
            {
                return BadRequest();
            }
            var isUpdated = _countryRepository.UpdateCountry(country);

            if (isUpdated)
            {
                return Ok(new { Message = "Country Updated Successfully" });
            }
            return NoContent();
        }
        #endregion

        #region DeleteCountry
        [HttpDelete]
        public IActionResult DeleteCountry(int countryId, [FromBody] CountryModel country)
        {
            if (country == null)
            {
                return BadRequest();
            }
            bool isDeleted = _countryRepository.DeleteCountry(countryId);

            if (isDeleted)
            {
                return Ok(new { Message = "Country Deleted Successfully" });
            }
            return StatusCode(500, "An Error Occurred while Deleting Country");
        }
        #endregion

        #region GetCountryById
        [HttpGet("{id}")]
        public IActionResult GetCountryById(int id)
        {
            var country = _countryRepository.GetByCountryID(id);
            if (country == null || !country.Any()) // Check if no data is returned
            {
                return NotFound(new { Message = "Country not found." });
            }
            return Ok(country);
        }
        #endregion
    }
}
