using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repositories.IRepository
{
    public interface IProductsRepository : IRepository<Products>
    {
        void Update(Products obj);
        void Save();
    }
}
