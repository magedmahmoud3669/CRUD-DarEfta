using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DarEfta.DAL.Entity
{

    [Table("Department")]
    public class Department
    {

        [Key]
        public int Id { get; set; }

        [StringLength(50) , Required]
        public string Name { get; set; }

        [StringLength(50), Required]
        public string Code { get; set; }

    }
}
