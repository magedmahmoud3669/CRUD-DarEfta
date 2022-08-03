using DarEfta.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DarEfta.BL.Interface
{
    public interface IDistrictRep
    {
        Task<IEnumerable<District>> GetAsync(Expression<Func<District, bool>> filter = null);
        Task<District> GetByIdAsync(Expression<Func<District, bool>> filter);

    }
}
