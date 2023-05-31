using AuthenticationDemo.Data;
using Ecomm.Models;
using Microsoft.AspNetCore.Mvc;
using static Ecomm.Controllers.CategoryController;

namespace Ecomm.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController

        private readonly ApplicationDbContext configuration;
        CategoryDAL catdl;
        ProductDAL db;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        public ProductController(ApplicationDbContext configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.configuration = configuration;
            db = new ProductDAL(this.configuration);
            catdl = new CategoryDAL(this.configuration);
            this.env = env;
        }
        public ActionResult Index()
        {
            List<Product> model = db.GetAllProducts();
            List<Category> model1 = catdl.GetAllCategories();
            foreach (Product p in model)
            {
                foreach (Category c in model1)
                {
                    if (c.Catid == p.Id)
                    {
                        p.Catname = c.Catname;
                    }
                }
            }
            return View(model);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var model = db.GetProductById(id);
            return View();
        }

        //// GET: ProductController/Create
        //public ActionResult Create()
        //{

        //    List<Category> model = catdl.GetAllCategories();
        //    ViewBag.categories = model;
        //    return View();
        //}

        //// POST: ProductController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Product prod)
        //{
        //    try
        //    {
        //        int res = db.AddProduct(prod);
        //        if (res > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            ViewBag.Error("Something went wrong");
        //            return View();
        //        }

        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


        // GET: ProductController/Create
        //add new product

        public ActionResult Create()
        {
            List<Category> model = catdl.GetAllCategories();
            ViewBag.categories = model;
            return View();
        }

        
        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product prod, IFormFile file)
        {
            try
            {
                using (var fs = new FileStream(env.WebRootPath + "\\images\\" + file.FileName, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fs);
                }
                prod.Img = "~/images/" + file.FileName;
                int res = db.AddProduct(prod);

                if (res > 0)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {

                    ViewBag.Error = "Something Went Wrong";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }



        //// GET: ProductController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    var model = db.GetProductById(id);
        //    List<Category> model2 = catdl.GetAllCategories();


        //    ViewBag.categories = model2;
        //    return View(model);
        //}

        //// POST: ProductController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public ActionResult Edit(Product prod)
        //{
        //    try
        //    {
        //        int res = db.UpdateProduct(prod);
        //        if (res > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            ViewBag.Error("Something went wrong");
        //            return View();
        //        }
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: ProductController/Edit/5
        public ActionResult Edit(int Id)
        {
            var model = db.GetProductById(Id);
            List<Category> model2 = catdl.GetAllCategories();
            HttpContext.Session.SetString("oldImageUrl", model.Img);

            ViewBag.categories = model2;
            return View(model);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product prod, IFormFile file)
        {
            try
            {
                string oldimageurl = HttpContext.Session.GetString("oldImageUrl");
                if (file != null)
                {
                    using (var fs = new FileStream(env.WebRootPath + "\\images\\" + file.FileName, FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(fs);
                    }
                    prod.Img = "~/images/" + file.FileName;

                    string[] str = oldimageurl.Split("/");
                    string str1 = (str[str.Length - 1]);
                    string path = env.WebRootPath + "\\images\\" + str1;
                    System.IO.File.Delete(path);
                }
                else
                {
                    prod.Img = oldimageurl;
                }

                int res = db.UpdateProduct(prod);
                if (res > 0)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {

                    ViewBag.Error = "Something Went Wrong";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }



        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = db.GetProductById(id);
            return View(model);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int res = db.DeleteProduct(id);
                if (res > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error("Something went wrong");
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
