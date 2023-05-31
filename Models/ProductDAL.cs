using AuthenticationDemo.Data;

namespace Ecomm.Models
{
    public class ProductDAL
    {
        private readonly ApplicationDbContext db;
        public ProductDAL(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Product> GetAllProducts()
        {
            //var result = (from p in db.Products select p).ToList();
            //return result;
            //or

            return db.products.ToList();
        }
        public Product GetProductById(int id)
        {
            //var result = (from p in db.Products
            //              where p.Id == id
            //              select p).FirstOrDefault();

            //OR
            //var result = db.Products.Where(x => x.Id == id).FirstOrDefault();
            //return result;

            var result = db.products.FirstOrDefault(x => x.Id == id);
            return result;
        }
        public int AddProduct(Product prod)
        {
            int result = 0;
            db.products.Add(prod); // add first in the DBSet
            result = db.SaveChanges(); // reflect the changes in Database
            return result;
        }
        public int UpdateProduct(Product prod) //new record
        {
            int res = 0;
            var result = db.products.Where(x => x.Id == prod.Id).FirstOrDefault();
            if (result != null) // result hold old prod data
            {
                result.Name = prod.Name;
                result.Catid = prod.Catid;
                result.Company = prod.Company;
                result.Description = prod.Description;
                result.Price = prod.Price;
                result.Img = prod.Img;
                res = db.SaveChanges();
            }
            return res;
        }
        public int DeleteProduct(int id)
        {
            int res = 0;
            var result = db.products.Where(x => x.Id == id).FirstOrDefault();
            if (result != null) // result hold old prod data
            {
                db.products.Remove(result); // remove the object from DbSet
                res = db.SaveChanges();// update the changes in main DB
            }
            return res;
        }

        public List<Product> SearchProducts(string searchString)
        {
            var query = from p in db.products
                        where p.Name.Contains(searchString) || p.Description.Contains(searchString)
                        select p;

            return query.ToList();
        }
    }
}
