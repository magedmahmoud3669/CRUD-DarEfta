using AutoMapper;
using DarEfta.BL.Models;
using DarEfta.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarEfta.BL.Mapper
{
    public class DomainProfile : Profile
    {

        public DomainProfile()
        {
            CreateMap<Department,DepartmentVM>(); // Retrive
            CreateMap<DepartmentVM, Department>();// Create - Update - Delete

            CreateMap<Employee, EmployeeVM>(); // Retrive
            CreateMap<EmployeeVM, Employee>();// Create - Update - Delete

            CreateMap<Country, CountryVM>(); // Retrive
            CreateMap<CountryVM, Country>();// Create - Update - Delete

            CreateMap<City, CityVM>(); // Retrive
            CreateMap<CityVM, City>();// Create - Update - Delete

            CreateMap<District, DistrictVM>(); // Retrive
            CreateMap<DistrictVM, District>();// Create - Update - Delete

        }

    }
}
