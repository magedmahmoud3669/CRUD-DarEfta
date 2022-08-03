using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarEfta.BL.Models
{
    public class DepartmentVM
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Name Required")]
        [MinLength(3 , ErrorMessage = "Min Len 3")]
        [MaxLength(50 , ErrorMessage = "Max Len 50")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code Required")]
        [Range(100 , 2000 , ErrorMessage = "Code Range Btw 100 : 2000")]
        public string Code { get; set; }

    }
}
