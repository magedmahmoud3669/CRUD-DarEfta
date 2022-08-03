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
    public class EmployeeRep : IEmployeeRep
    {

        ApplicationContext db;
        public EmployeeRep(ApplicationContext _db)
        {
            db = _db;
        }


        // When You Try To Load Data

        // Lazy Load
        // Eager Load
        // Explicit Load



        public async Task<IEnumerable<Employee>> GetAsync(Expression<Func<Employee, bool>> filter = null)
        {
            if (filter == null)
            {
                // Eager Load
                var data = await db.Employee
                                    .Include("Department")
                                    .Include("District")
                                    .Select(x => x).ToListAsync();
                return data;
            }
            else
            {
                var data = await db.Employee
                                    .Where(filter)
                                    .Include("Department")
                                    .Include("District")
                                    .ToListAsync();
                return data;
            }
        }


        public async Task<IEnumerable<Employee>> SearchAsync(Expression<Func<Employee, bool>> filter = null)
        {
            if (filter == null)
            {
                // Eager Load
                var data = await db.Employee.Include("Department").Include("District").Select(x => x).ToListAsync();
                return data;
            }
            else
            {
                var data = await db.Employee.Where(filter).Include("Department").Include("District").ToListAsync();
                return data;
            }
        }

        public async Task<Employee> GetByIdAsync(Expression<Func<Employee, bool>> filter)
        {
            var data = await db.Employee.Where(filter).Include("Department").Include("District").FirstOrDefaultAsync();
            return data;
        }

        public async Task<Employee> CreateAsync(Employee obj)
        {
            obj.CreationDate = DateTime.Now;

            await db.Employee.AddAsync(obj);
            await db.SaveChangesAsync();

            var data = await db.Employee.OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            return data;
        }

        public async Task<Employee> UpdateAsync(Employee obj)
        {

            obj.IsUpdate = true;
            obj.UpdateDate = DateTime.Now;

            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();

            var NewData = await db.Employee.Where(x => x.Id == obj.Id).FirstOrDefaultAsync();

            return NewData;

        }

        public async Task DeleteAsync(Employee obj)
        {
            var oldData = db.Employee.Find(obj.Id);
            //oldData.IsDeleted = true;
            //oldData.DeleteDate = DateTime.Now;
            
            db.Employee.Remove(oldData);
            await db.SaveChangesAsync();
        }


    }
}
