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
    public class CategoryRepository : Repository<Category>,  ICategoryRepository
    {
        private ApplicationDbContext _DbContext;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _DbContext = db;
        }

        public void Save()
        {
            _DbContext.SaveChanges();
        }

        public void Update(Category obj)
        {
            _DbContext.Category.Update(obj);
        }
    }
}
