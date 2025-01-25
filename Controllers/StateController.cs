<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Practice.Data;
using WebAPI_Practice.Model;

namespace WebAPI_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        #region Configuration
        private readonly StateRepository _stateRepository;

        public StateController(StateRepository stateRepository) => _stateRepository = stateRepository;

        #endregion

        #region GetAllCountries
        [HttpGet]
        public IActionResult GetAllCountries()
        {
            var states = _stateRepository.SelectAllStates();
            return Ok(states);
        }
        #endregion

        #region GetStateById
        [HttpGet("{id}")]
        public IActionResult GetCountryById(int id)
        {
            var state = _stateRepository.GetStateByID(id);
            if (state == null || !state.Any()) // Check if no data is returned
            {
                return NotFound(new { Message = "State not found." });
            }
            return Ok(state);
        }
        #endregion

        #region InsertState
        [HttpPost]
        public IActionResult InsertCountry([FromBody] StateModel state)
        {
            if (state == null)
            {
                return BadRequest();
            }
            bool isInserted = _stateRepository.InsertState(state);

            if (isInserted)
            {
                return Ok(new { Message = "State Inserted Successfully" });
            }
            return StatusCode(500, "An Error Occurred while Inserting State");
        }
        #endregion

        #region UpdateState
        [HttpPut("{stateID}")]
        public IActionResult UpdateCountry(int stateID, [FromBody] StateModel state)
        {
            if (state == null || stateID != state.StateID)
            {
                return BadRequest();
            }
            var isUpdated = _stateRepository.UpdateState(state);

            if (isUpdated)
            {
                return Ok(new { Message = "State Updated Successfully" });
            }
            return NoContent();
        }
        #endregion

        #region DeleteState
        [HttpDelete]
        public IActionResult DeleteCountry(int stateID,StateModel state)
        {
            if (state == null)
            {
                return BadRequest();
            }
            bool isDeleted = _stateRepository.DeleteState(stateID);

            if (isDeleted)
            {
                return Ok(new { Message = "State Deleted Successfully" });
            }
            return StatusCode(500, "An Error Occurred while Deleting Country");
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
    public class StateController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public StateController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/State");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var states = JsonConvert.DeserializeObject<List<StateModel>>(data);
                return View(states);
            }
            return View(new List<StateModel>());
        }

        public async Task<IActionResult> Add(string? EnStateID)
        {
            await LoadCountryList();

            if (!string.IsNullOrEmpty(EnStateID))
            {
                int? StateID = Convert.ToInt32(UrlEncryptor.Decrypt(EnStateID.ToString()));
                var response = await _httpClient.GetAsync($"api/State/{StateID}");
                ViewBag.StateID = StateID.Value;
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var state = JsonConvert.DeserializeObject<StateModel>(data);
                    
                    return View(state);
                }
            }
            return View(new StateModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(StateModel state)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(state);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;

                if (state.StateID == null)
                    response = await _httpClient.PostAsync("api/State", content);
                else
                    response = await _httpClient.PutAsync($"api/State", content);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            await LoadCountryList();
            return View("Add", state);
        }

        public async Task<IActionResult> Delete(string EnStateID)
        {
            if (!string.IsNullOrEmpty(EnStateID)) {
                int StateID = Convert.ToInt32(UrlEncryptor.Decrypt(EnStateID.ToString()));
                var response = await _httpClient.DeleteAsync($"api/State/{StateID}");
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

        

    }
}
>>>>>>> cc8f604 (Created API's for more tables and tested it sucessfully.)
