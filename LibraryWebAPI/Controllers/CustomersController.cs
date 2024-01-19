using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryModel.Data;
using LibraryModel.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly LibraryContext _context;
        private string _baseUrl = "http://localhost:7222/api/Customers";

        public CustomersController(LibraryContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl);

            if (response.IsSuccessStatusCode)
            {
                var customers = JsonConvert.DeserializeObject<List<Customer>>(await response.Content.ReadAsStringAsync());
                return new JsonResult(customers);
            }
            return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
        }

        // GET: api/customers/details/5
        [HttpGet("details/{id}")]
        public async Task<JsonResult> Details(int? id)
        {
            if (id == null)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status400BadRequest };
            }

            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/details/{id.Value}");

            if (response.IsSuccessStatusCode)
            {
                var customer = JsonConvert.DeserializeObject<Customer>(
                    await response.Content.ReadAsStringAsync());
                return new JsonResult(customer);
            }

            return new JsonResult(null) { StatusCode = StatusCodes.Status404NotFound };
        }


        // POST: api/customers/create
        [HttpPost("create")]
        public async Task<JsonResult> Create([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status400BadRequest };
            }

            try
            {
                var client = new HttpClient();
                string json = JsonConvert.SerializeObject(customer);
                var response = await client.PostAsync($"{_baseUrl}/create",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return new JsonResult(new { success = true }) { StatusCode = StatusCodes.Status200OK };
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return new JsonResult(new { success = false, message = $"Unable to create record: {ex.Message}" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            return new JsonResult(new { success = false }) { StatusCode = StatusCodes.Status500InternalServerError };
        }

        // GET: api/customers/edit/5
        [HttpGet("edit/{id}")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/edit/{id.Value}");

            if (response.IsSuccessStatusCode)
            {
                var customer = JsonConvert.DeserializeObject<Customer>(
                    await response.Content.ReadAsStringAsync());
                return new JsonResult(customer) { StatusCode = StatusCodes.Status200OK };
            }

            return new NotFoundResult();
        }

        // POST: api/customers/edit
        [HttpPost("edit")]
        public async Task<JsonResult> Edit([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(null) { StatusCode = StatusCodes.Status400BadRequest };
            }

            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(customer);
            var response = await client.PutAsync($"{_baseUrl}/edit/{customer.Id}",
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return new JsonResult(new { success = true }) { StatusCode = StatusCodes.Status200OK };
            }

            return new JsonResult(new { success = false }) { StatusCode = StatusCodes.Status500InternalServerError };
        }

        // GET: api/customers/delete/5
        [HttpGet("delete/{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/delete/{id.Value}");

            if (response.IsSuccessStatusCode)
            {
                var customer = JsonConvert.DeserializeObject<Customer>(
                    await response.Content.ReadAsStringAsync());
                return new JsonResult(customer) { StatusCode = StatusCodes.Status200OK };
            }

            return new NotFoundResult();
        }

        // POST: api/customers/delete
        [HttpPost("delete")]
        public async Task<JsonResult> Delete([FromBody] Customer customer)
        {
            try
            {
                var client = new HttpClient();
                HttpRequestMessage request =
                    new HttpRequestMessage(HttpMethod.Delete,
                        $"{_baseUrl}/delete/{customer.Id}")
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(customer),
                            Encoding.UTF8, "application/json")
                    };
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return new JsonResult(new { success = true }) { StatusCode = StatusCodes.Status200OK };
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
            }

            return new JsonResult(new { success = false }) { StatusCode = StatusCodes.Status500InternalServerError };
        }

    }
}