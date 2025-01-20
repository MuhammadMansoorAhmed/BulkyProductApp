using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repositories.IRepository;
using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category{  get; private set; }
        public IProductsRepository Products { get; private set; }

        

        private ApplicationDbContext _DbContext;
        public UnitOfWork(ApplicationDbContext db) 
        {
            _DbContext = db;
            Category = new CategoryRepository(_DbContext);
            Products = new ProductRepository(_DbContext);
        }

        public void Save()
        {
            _DbContext.SaveChanges();
        }
    }
}
