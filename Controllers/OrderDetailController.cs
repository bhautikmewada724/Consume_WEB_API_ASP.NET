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
    public class OrderDetailController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public OrderDetailController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/OrderDetail");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var orderDetails = JsonConvert.DeserializeObject<List<OrderDetailModel>>(data);
                return View(orderDetails);
            }
            return View(new List<OrderDetailModel>());
        }

        public async Task<IActionResult> Add(int? OrderDetailID)
        {
            await LoadUserList();
            await LoadOrderList();
            await LoadProductList();
            if (OrderDetailID.HasValue)
            {
                var response = await _httpClient.GetAsync($"api/OrderDetail/{OrderDetailID}");
                ViewBag.OrderDetailID = OrderDetailID.Value;
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var orderDetail = JsonConvert.DeserializeObject<OrderDetailModel>(data);

                    return View(orderDetail);
                }
            }
            return View(new OrderDetailModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(OrderDetailModel orderDetail)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(orderDetail);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;

                if (orderDetail.OrderDetailID== null)
                    response = await _httpClient.PostAsync("api/OrderDetail", content);
                else
                    response = await _httpClient.PutAsync($"api/OrderDetail", content);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }

            await LoadUserList();
            await LoadOrderList();
            await LoadProductList();

            return View("Add", orderDetail);
        }

        public async Task<IActionResult> Delete(int OrderDetailID)
        {
            var response = await _httpClient.DeleteAsync($"api/OrderDetail/{OrderDetailID}");
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

        private async Task LoadOrderList()
        {
            var response = await _httpClient.GetAsync("api/Order/DropDown");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var OrderList = JsonConvert.DeserializeObject<List<OrderDropDownModel>>(data);
                ViewBag.OrderList = OrderList;
            }
        }

        private async Task LoadProductList()
        {
            var response = await _httpClient.GetAsync("api/Product/DropDown");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var ProductList = JsonConvert.DeserializeObject<List<ProductDropDownModel>>(data);
                ViewBag.ProductList = ProductList;
            }
        }
    }
}