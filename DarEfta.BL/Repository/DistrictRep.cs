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
    public class DistrictRep : IDistrictRep
    {

        ApplicationContext db;
        public DistrictRep(ApplicationContext _db)
        {
            db = _db;
        }

        public async Task<IEnumerable<District>> GetAsync(Expression<Func<District, bool>> filter = null)
        {
            if (filter == null)
            {
                // Eager Load
                var data = await db.District.Include("City").Select(x => x).ToListAsync();
                return data;
            }
            else
            {
                var data = await db.District.Where(filter).Include("City").ToListAsync();
                return data;
            }
        }

        public async Task<District> GetByIdAsync(Expression<Func<District, bool>> filter)
        {
            var data = await db.District.Where(filter).Include("City").FirstOrDefaultAsync();
            return data;
        }
    }
}
