using AuthenticationDemo.Data;
using Ecomm.Models;
using Microsoft.AspNetCore.Components.Routing;

namespace Ecomm.Models
{
    public class CategoryDAL
    {
        private readonly ApplicationDbContext db;

        public CategoryDAL(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Category> GetAllCategories()
        {

            return db.categories.ToList();
        }

        public Category GetCategoryById(int Id)
        {
            var result = db.categories.Where(x => x.Catid == Id).FirstOrDefault();
            return result;
        }

        public int AddCategory(Category category)
        {
            var result = 0;
            db.categories.Add(category);
            result = db.SaveChanges();
            return result;
        }

        public int UpdateCategory(Category category)
        {
            int result = 0;
            var res = db.categories.Where(x => x.Catid == category.Catid).FirstOrDefault();
            if (res != null)
            {
                res.Catname = category.Catname;

                result = db.SaveChanges();
            }
            return result;
        }


        public int DeleteCategory(int Id)
        {
            int res = 0;
            var result = db.categories.Where(x => x.Catid == Id).FirstOrDefault();
            if (result != null)
            {
                db.categories.Remove(result);
                res = db.SaveChanges();
            }
            return res;
        }
    }
}
