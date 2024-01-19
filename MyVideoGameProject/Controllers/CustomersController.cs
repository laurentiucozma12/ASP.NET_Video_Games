using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoGameModel.Data;
using VideoGameModel.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Cozma_Laurentiu_Lab2.Controllers
{
    [Authorize(Policy = "SalesManager")]
    public class CustomersController : Controller
    {
        private readonly VideoGameContext _context;

        public CustomersController(VideoGameContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            return NotFound();

        }

        // GET: Inventory/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            var client = new HttpClient();

            return NotFound();
        }
        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,Name,Address,BirthDate")] Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            try
            {
                var client = new HttpClient();
                string json = JsonConvert.SerializeObject(customer);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record:{ex.Message}");
            }

            return View(customer);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            return new NotFoundResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("Id,Name,Address,BirthDate")] Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(customer);

            return View(customer);
        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            var client = new HttpClient();

            return new NotFoundResult();
        }
        // POST: Customers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind("Id")] Customer customer)
        {
            try
            {
                var client = new HttpClient();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to delete record: {ex.Message}");
            }

            return View(customer);
        }
    }
}