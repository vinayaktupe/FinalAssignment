using FinalAssignment.DAL.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalAssignment.Services.InputModel
{
    public class EmployeeInputModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please Provide Name")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Please Select Type of Employee")]
        public EmployeeType EmployeeType { set; get; }
    }
}
