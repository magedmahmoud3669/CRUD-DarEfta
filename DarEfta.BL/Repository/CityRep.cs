using DarEfta.BL.Interface;
using DarEfta.DAL.Database;
using DarEfta.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DarEfta.BL.Repository
{
    public class CityRep : ICityRep
    {


        ApplicationContext db;
        public CityRep(ApplicationContext _db)
        {
            db = _db;
        }

        public async Task<IEnumerable<City>> GetAsync(Expression<Func<City, bool>> filter = null)
        {
            if (filter == null)
            {
                // Eager Load
                var data = await db.City.Include("Country").Select(x => x).ToListAsync();
                return data;
            }
            else
            {
                var data = await db.City.Where(filter).Include("Country").ToListAsync();
                return data;
            }
        }

        public async Task<City> GetByIdAsync(Expression<Func<City, bool>> filter)
        {
            var data = await db.City.Where(filter).Include("Country").FirstOrDefaultAsync();
            return data;
        }
    }
}
