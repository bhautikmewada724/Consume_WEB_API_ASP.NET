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
    public class OrderController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Order");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<List<OrderModel>>(data);
                return View(orders);
            }
            return View(new List<ProductModel>());
        }

        public async Task<IActionResult> Add(int? OrderID)
        {
            await LoadUserList();
            await LoadCustomerList();

            if (OrderID.HasValue)
            {
                var response = await _httpClient.GetAsync($"api/Order/{OrderID}");
                ViewBag.OrderID = OrderID.Value;
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var order = JsonConvert.DeserializeObject<OrderModel>(data);

                    return View(order);
                }
            }
            return View(new OrderModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(OrderModel order)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(order);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;

                if (order.OrderID == null)
                    response = await _httpClient.PostAsync("api/Order", content);
                else
                    response = await _httpClient.PutAsync($"api/Order", content);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            await LoadCustomerList();
            await LoadUserList();
            return View("Add", order);
        }

        public async Task<IActionResult> Delete(int OrderID)
        {
            var response = await _httpClient.DeleteAsync($"api/Order/{OrderID}");
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


        private async Task LoadCustomerList()
        {
            var response = await _httpClient.GetAsync("api/Customer/DropDown");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var CustomerList = JsonConvert.DeserializeObject<List<CustomerDropDownModel>>(data);
                ViewBag.CustomerList = CustomerList;
            }
        }

    }
}