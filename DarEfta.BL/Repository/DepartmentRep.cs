using DarEfta.BL.Interface;
using DarEfta.BL.Models;
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
    public class DepartmentRep : IDepartmentRep
    {

        ApplicationContext db;
        public DepartmentRep(ApplicationContext _db)
        {
            db = _db;
        }

        public async Task<IEnumerable<Department>> GetAsync(Expression<Func<Department, bool>> filter = null)
        {
            if(filter == null)
            {
                var data = await db.Department.Select(x => x).ToListAsync();
                return data;
            }
            else
            {
                var data = await db.Department.Where(filter).ToListAsync();
                return data;
            }
        }

        public async Task<Department> GetByIdAsync(Expression<Func<Department, bool>> filter)
        {

            var data = await db.Department.Where(filter).FirstOrDefaultAsync();
            return data;

        }

        public async Task<Department> CreateAsync(Department obj)
        {
            await db.Department.AddAsync(obj);
            await db.SaveChangesAsync();

            var data = await db.Department.OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            return data;
        }

        public async Task<Department> UpdateAsync(Department obj)
        {

            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();

            var NewData = await db.Department.Where(x => x.Id == obj.Id).FirstOrDefaultAsync();

            return NewData;

        }

        public async Task DeleteAsync(int id)
        {
            var oldData = db.Department.Find(id);

            db.Department.Remove(oldData);
            await db.SaveChangesAsync();
        }
    }
}
