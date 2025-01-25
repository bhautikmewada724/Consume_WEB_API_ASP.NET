using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
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
=======
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using ApiConsume.Models;
using ApiConsume.Helpers;

namespace ApiConsume.Controllers
{
    public class CountryController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public CountryController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Country");
            
            /*if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var countries = JsonConvert.DeserializeObject<List<CountryModel>>(data);
                return View(countries);
            }*/
            if (response.IsSuccessStatusCode)
            {
                JsonOperation<List<CountryModel>> jsonOperation = new JsonOperation<List<CountryModel>>();

                ApiResultData<List<CountryModel>> apiResultData = await jsonOperation.jsonDeserialization(response);
                return View(apiResultData.Data);
            }
            return View();
        }


       



        public async Task<IActionResult> Add(string? EnCountryID)
        {
            
            
            if (!string.IsNullOrEmpty(EnCountryID))
            {
                int? CountryID = Convert.ToInt32(UrlEncryptor.Decrypt(EnCountryID.ToString()));
                ViewBag.CountryID = CountryID.Value;
                var response = await _httpClient.GetAsync($"api/Country/{CountryID}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var country = JsonConvert.DeserializeObject<CountryModel>(data);
                    
                    return View(country);
                }
            }
            return View(new CountryModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(CountryModel country)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(country);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;

                if (country.CountryID == null)
                    response = await _httpClient.PostAsync("api/Country", content);
                else
                    response = await _httpClient.PutAsync($"api/Country", content);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            
            return View("Add", country);
        }

        public async Task<IActionResult> Delete(string EnCountryID)
        {
            if (!string.IsNullOrEmpty(EnCountryID)) {
                int? CountryID = Convert.ToInt32(UrlEncryptor.Decrypt(EnCountryID.ToString()));
                var response = await _httpClient.DeleteAsync($"api/Country/{CountryID}");
            }
            return RedirectToAction("Index");
        }

       

       

       
    }
}
>>>>>>> cc8f604 (Created API's for more tables and tested it sucessfully.)
