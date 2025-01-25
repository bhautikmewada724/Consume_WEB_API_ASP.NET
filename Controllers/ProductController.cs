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
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Product");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ProductModel>>(data);
                return View(products);
            }
            return View(new List<ProductModel>());
        }

        public async Task<IActionResult> Add(int? ProductID)
        {
            await LoadUserList();

            if (ProductID.HasValue)
            {
                var response = await _httpClient.GetAsync($"api/Product/{ProductID}");
                ViewBag.ProductID = ProductID.Value;
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var product = JsonConvert.DeserializeObject<ProductModel>(data);

                    return View(product);
                }
            }
            return View(new ProductModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;

                if (product.ProductID== null)
                    response = await _httpClient.PostAsync("api/Product", content);
                else
                    response = await _httpClient.PutAsync($"api/Product", content);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            await LoadUserList();

            return View("Add", product);
        }

        public async Task<IActionResult> Delete(int ProductID)
        {
            var response = await _httpClient.DeleteAsync($"api/Product/{ProductID}");
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