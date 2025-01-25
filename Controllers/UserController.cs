using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using ApiConsume.Models;

namespace ApiConsume.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/User");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var useres = JsonConvert.DeserializeObject<List<UserModel>>(data);
                return View(useres);
            }
            return View(new List<UserModel>());
        }

        public async Task<IActionResult> Add(int? UserID)
        {
            

            if (UserID.HasValue)
            {
                var response = await _httpClient.GetAsync($"api/User/{UserID}");
                ViewBag.UserID = UserID.Value;
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<UserModel>(data);
                    
                    return View(user);
                }
            }
            return View(new UserModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;

                if (user.UserID == null)
                    response = await _httpClient.PostAsync("api/User", content);
                else
                    response = await _httpClient.PutAsync($"api/User", content);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            
            return View("Add", user);
        }

        public async Task<IActionResult> Delete(int UserID)
        {
            var response = await _httpClient.DeleteAsync($"api/User/{UserID}");
            return RedirectToAction("Index");
        }

       
    }
}