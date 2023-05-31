using AuthenticationDemo.Data;
using AuthenticationDemo.Models;
using Ecomm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static Ecomm.Controllers.CategoryController;

namespace Ecomm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext configuration;
        CategoryDAL catdl;
        ProductDAL db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext configuration)
        {
            _logger = logger;
            this.configuration = configuration;
            db = new ProductDAL(this.configuration);
            catdl = new CategoryDAL(this.configuration);
        }

        //[Authorize]
        public ActionResult Index(string search)
        {
            List<Product> model;
            if (!string.IsNullOrEmpty(search))
            {
                // Search for products containing the search string in their name or description
                model = db.SearchProducts(search);
            }
            else
            {
                // Get all products
                model = db.GetAllProducts();
            }

            // Get all categories
            List<Category> categories = catdl.GetAllCategories();

            // Add category names to products
            foreach (Product p in model)
            {
                foreach (Category c in categories)
                {
                    if (c.Catid == p.Catid)
                    {
                        p.Catname = c.Catname;
                    }
                }
            }

            ViewBag.Categories = categories;
            ViewBag.SearchTerm = search; // Add the search term to the ViewBag
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}