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
    public class CustomerController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Customer");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var customers = JsonConvert.DeserializeObject<List<CustomerModel>>(data);
                return View(customers);
            }
            return View(new List<CustomerModel>());
        }

        public async Task<IActionResult> Add(int? CustomerID)
        {
            await LoadUserList();

            if (CustomerID.HasValue)
            {
                var response = await _httpClient.GetAsync($"api/Customer/{CustomerID}");
                ViewBag.CustomerID = CustomerID.Value;
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var customer = JsonConvert.DeserializeObject<CustomerModel>(data);

                    return View(customer);
                }
            }
            return View(new CustomerModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(CustomerModel customer)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(customer);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;

                if (customer.CustomerID == null)
                    response = await _httpClient.PostAsync("api/Customer", content);
                else
                    response = await _httpClient.PutAsync($"api/Customer", content);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }

            return View("Add", customer);
        }

        public async Task<IActionResult> Delete(int CustomerID)
        {
            var response = await _httpClient.DeleteAsync($"api/Customer/{CustomerID}");
            return RedirectToAction("Index");
        }

        private async Task LoadUserList()
        {
            var response = await _httpClient.GetAsync("api/User/DropDown");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var UserList = JsonConvert.DeserializeObject<List<UserDropDownModel>>(data);
                ViewBag.UserList = UserList;
            }
        }


    }
}