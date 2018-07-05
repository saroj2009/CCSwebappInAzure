using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CCSmvc.Models
{
    
    [Table("PaymentDetails")]
    public class Payments
    {
        [Key]
        public int? Id{ get; set; }

        [Required(ErrorMessage = "Required Name")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Required ContactName")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Required DOB")]
        public string DOB { get; set; }
       

        [Required(ErrorMessage = "Required Amount")]
        public int Amount { get; set; }
       

       // [Required(ErrorMessage = "Required PostalCode")]
        public string Date { get; set; }

        //[Required(ErrorMessage = "Required Country")]
        public string Month { get; set; }

        //[Required(ErrorMessage = "Required Phone")]
        public string Status { get; set; }
        //public string Fax { get; set; }

        public string EmpID { get; set; }
        public string Email { get; set; }
    }
    
}