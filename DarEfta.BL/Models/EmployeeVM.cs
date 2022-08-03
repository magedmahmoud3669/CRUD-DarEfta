using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DarEfta.DAL.Entity;
using Microsoft.AspNetCore.Http;

namespace DarEfta.BL.Models
{
    public class EmployeeVM
    {

        public EmployeeVM()
        {
            IsDeleted = false;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Name Required")]
        [MaxLength(50 , ErrorMessage = "Max Len 50")]
        [MinLength(3 , ErrorMessage = "Min Len 3")]
        public string Name { get; set; }

        [Range(1000 , 100000 , ErrorMessage = "Range Btw 1K : 100K")] 
        public double Salary { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        // 12-Street-City-Country
        [RegularExpression("[0-9]{1,5}-[a-zA-Z]{1,10}-[a-zA-Z]{1,10}-[a-zA-Z]{1,10}", ErrorMessage = "12-Street-City-Country")]
        public string Address { get; set; }

        public string Notes { get; set; }

        //[DataType(DataType.Date)]
        //[Display(Name = "x")]
        public DateTime HireDate { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DeleteDate { get; set; }

        public bool IsUpdate { get; set; }

        public DateTime UpdateDate { get; set; }

        public int DepartmentId { get; set; }
        public int? DistrictId { get; set; }

        public Department? Department { get; set; }
        public District? District { get; set; }
        public string? ImageName { get; set; }
        public string? CvName { get; set; }

        public IFormFile? Image { get; set; }
        public IFormFile? Cv { get; set; }

        // File Name
        // File Type
        // File Length


    }
}
