using FinalAssignment.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FinalAssignment.Services.ViewModel
{
    public class MinuteViewModel
    {
        public int ID { get; set; }
        public List<Employee> Employee { set; get; }
        public List<Supervisor> Supervisor { set; get; }

        public MinuteType MinuteType { set; get; }
        public string MinType { set; get; }

        public string Topic { set; get; }

        public string Date { set; get; }

        public string Crew { set; get; }

        public bool ApprovalStatus { set; get; }

        public bool ApprovalHistory { set; get; }

    }
}
