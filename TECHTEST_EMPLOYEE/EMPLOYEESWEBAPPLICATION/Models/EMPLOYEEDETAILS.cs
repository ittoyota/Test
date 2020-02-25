using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EMPLOYEESWEBAPPLICATION.Models
{
    public class EMPLOYEEDETAILS
    {
        public int EMPLOYEEID { get; set; }
        public string NAME { get; set; }
        public string DESIGNATION { get; set; }
        public Nullable<int> AGE { get; set; }
        public string SALARY { get; set; }
        [Display(Name = "User Pwd")]
        [Required(ErrorMessage = "Pwd is required")]
        public string USERNAME { get; set; }
        [Display(Name = "User Pwd")]
        [Required(ErrorMessage = "Pwd is required")]
        public string USERPASSWORD { get; set; }
        public string EMPROLE { get; set; }
    }
}