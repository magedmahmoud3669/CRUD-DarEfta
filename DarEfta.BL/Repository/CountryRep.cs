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
    public class CountryRep : ICountryRep
    {


        ApplicationContext db;
        public CountryRep(ApplicationContext _db)
        {
            db = _db;
        }

        public async Task<IEnumerable<Country>> GetAsync(Expression<Func<Country, bool>> filter = null)
        {
            if (filter == null)
            {
                // Eager Load
                var data = await db.Country.Select(x => x).ToListAsync();
                return data;
            }
            else
            {
                var data = await db.Country.Where(filter).ToListAsync();
                return data;
            }
        }

        public async Task<Country> GetByIdAsync(Expression<Func<Country, bool>> filter)
        {
            var data = await db.Country.Where(filter).FirstOrDefaultAsync();
            return data;
        }
    }
}
