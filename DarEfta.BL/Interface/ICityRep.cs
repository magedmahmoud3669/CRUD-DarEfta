﻿using DarEfta.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DarEfta.BL.Interface
{
    public interface ICityRep
    {
        Task<IEnumerable<City>> GetAsync(Expression<Func<City, bool>> filter = null);
        Task<City> GetByIdAsync(Expression<Func<City, bool>> filter); 
    }
}
