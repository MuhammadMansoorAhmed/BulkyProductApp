using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repositories.IRepository;
using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repositories
{
    public class ProductRepository : Repository<Products>, IProductsRepository
    {
        private ApplicationDbContext _DbContext;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _DbContext = db;
        }

        public void Save()
        {
            _DbContext.SaveChanges();
        }

        public void Update(Products obj)
        {
            var objFormDb = _DbContext.Product.FirstOrDefault(u => u.ProductId == obj.ProductId);
            if (objFormDb != null)
            {
                objFormDb.Title = obj.Title;
                objFormDb.Description = obj.Description;
                objFormDb.ISBN = obj.ISBN;
                objFormDb.CategoryId = obj.CategoryId;
                objFormDb.Category = obj.Category;
                objFormDb.Price = obj.Price;
                objFormDb.ListPrice = obj.ListPrice;
                objFormDb.Price50 = obj.Price50;
                objFormDb.Price100 = obj.Price100;
                objFormDb.Author = obj.Author;
                if (obj.ImageUrl != null)
                {
                    objFormDb.ImageUrl = obj.ImageUrl;
                }

            }

        }
    }
}
