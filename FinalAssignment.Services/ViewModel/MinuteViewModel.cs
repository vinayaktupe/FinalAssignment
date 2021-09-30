using FinalAssignment.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FinalAssignment.Services.ViewModel
{
    public class MinuteViewModel
    {
        [Display(Name ="Minute ID")]
        public int ID { get; set; }

        [Display(Name = "Supervisors")]
        public List<Employee> Employee { set; get; }

        [Display(Name = "Supervisors")]
        public ICollection<Supervisor> Supervisor { set; get; }

        [Display(Name = "Minute Type")]
        public MinuteType MinuteType { set; get; }

        [Display(Name = "Minute Type")]
        public string MinType { set; get; }

        [Display(Name = "Minute Topic")]
        public string Topic { set; get; }

        [Display(Name = "Minute Date")]
        public string Date { set; get; }

        [Display(Name = "Minute Crew")]
        public Crew Crews { set; get; }

        [Display(Name = "Approval Status")]
        public bool ApprovalStatus { set; get; }

        [Display(Name = "Approval History")]
        public bool ApprovalHistory { set; get; }

    }
}
