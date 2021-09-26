using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FinalAssignment.DAL.Data.Models
{
    public class Employee
    {

        [Key]
        public int ID { set; get; }

        [Required(ErrorMessage ="Please Provide Name")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Please Select Type of Employee")]
        public EmployeeType EmployeeType { set; get; }

        public DateTime? CreatedAt { set; get; }

        public DateTime? UpdatedAt { set; get; }

        public bool? IsActive { set; get; }

        public virtual Minute Minutes { get; set; }
    }

    public enum EmployeeType
    {
        Normal,
        Supervisor
    }
}
