<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Http;
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
=======
﻿using Microsoft.AspNetCore.Mvc;
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
    public class CityController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public CityController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/City");
            
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var cities = JsonConvert.DeserializeObject<List<CityModel>>(data);
                return View(cities);
            }
            return View(new List<CityModel>());
        }

        public async Task<IActionResult> Add(string? EnCityID)
        {
            await LoadCountryList();
            
            if (!string.IsNullOrEmpty(EnCityID))
            {
                int? CityID = Convert.ToInt32(UrlEncryptor.Decrypt(EnCityID.ToString()));
                var response = await _httpClient.GetAsync($"api/City/{CityID}");
                ViewBag.CityID = CityID.Value;
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var city = JsonConvert.DeserializeObject<CityModel>(data);
                    ViewBag.StateList = await GetStatesByCountryID(city.CountryID);
                    return View(city);
                }
            }
            return View(new CityModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(CityModel city)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(city);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;

                if (city.CityID == null)
                    response = await _httpClient.PostAsync("api/City", content);
                else
                    response = await _httpClient.PutAsync($"api/City", content);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            await LoadCountryList();
            return View("Add", city);
        }

        public async Task<IActionResult> Delete(string? EnCityID)
        {
            if (!string.IsNullOrEmpty(EnCityID)) { 
            
            int? CityID = Convert.ToInt32(UrlEncryptor.Decrypt(EnCityID.ToString()));
            var response = await _httpClient.DeleteAsync($"api/City/DeleteCity/{CityID}");
            }
            return RedirectToAction("Index");
        }

        private async Task LoadCountryList()
        {
            var response = await _httpClient.GetAsync("api/City/Countries");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var countries = JsonConvert.DeserializeObject<List<CountryDropDownModel>>(data);
                ViewBag.CountryList = countries;
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetStatesByCountry(int CountryID)
        {
            var states = await GetStatesByCountryID(CountryID);
            return Json(states);
        }

        private async Task<List<StateDropDownModel>> GetStatesByCountryID(int CountryID)
        {
            var response = await _httpClient.GetAsync($"api/City/States/{CountryID}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<StateDropDownModel>>(data);
            }
            return new List<StateDropDownModel>();
        }
    }
}
>>>>>>> cc8f604 (Created API's for more tables and tested it sucessfully.)
