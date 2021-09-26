using FinalAssignment.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FinalAssignment.Services.InputModel
{
    public class MinutesInputModel
    {
        public MinutesInputModel()
        {
            SupervisorID = new List<Supervisor>();
        }

        public int ID { set; get; }

        [Required(ErrorMessage = "Please Select Minute Type")]
        public MinuteType MinuteType { set; get; }

        public List<Supervisor> SupervisorID { set; get; }

        [Required(ErrorMessage = "Please provide Topic")]
        public string Topic { set; get; }

        [Required(ErrorMessage = "Please Provide Date")]
        public DateTime Date { set; get; }

        [Required]
        public int CrewID { set; get; }
    }
}
