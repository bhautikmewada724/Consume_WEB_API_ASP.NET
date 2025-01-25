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
    public class BillController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public BillController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Bill");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var bills = JsonConvert.DeserializeObject<List<BillModel>>(data);
                return View(bills);
            }
            return View(new List<BillModel>());
        }

        public async Task<IActionResult> Add(int? BillID)
        {
            await LoadUserList();
            await LoadOrderList();

            if (BillID.HasValue)
            {
                var response = await _httpClient.GetAsync($"api/Bill/{BillID}");
                ViewBag.BillID = BillID.Value;
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var bill = JsonConvert.DeserializeObject<BillModel>(data);

                    return View(bill);
                }
            }
            return View(new BillModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(BillModel bill)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(bill);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;

                if (bill.BillID == null)
                    response = await _httpClient.PostAsync("api/Bill", content);
                else
                    response = await _httpClient.PutAsync($"api/Bill", content);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            await LoadOrderList();
            await LoadUserList();
            return View("Add", bill);
        }

        public async Task<IActionResult> Delete(int BillID)
        {
            var response = await _httpClient.DeleteAsync($"api/Bill/{BillID}");
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

    }
}