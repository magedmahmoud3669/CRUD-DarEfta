using DarEfta.BL.Models;
using DarEfta.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DarEfta.BL.Interface
{
    public interface IDepartmentRep
    {
        Task<IEnumerable<Department>> GetAsync(Expression<Func<Department, bool>> filter = null);
        Task<Department> GetByIdAsync(Expression<Func<Department, bool>> filter);
        Task<Department> CreateAsync(Department obj);
        Task<Department> UpdateAsync(Department obj);
        Task DeleteAsync(int id);
    }
}
