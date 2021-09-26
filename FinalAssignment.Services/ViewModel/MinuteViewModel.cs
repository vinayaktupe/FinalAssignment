using FinalAssignment.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalAssignment.Services.ViewModel
{
    public class MinuteViewModel
    {
        public List<Employee> Employee { set; get; }

        public MinuteType MinuteType { set; get; }

        public int Topic { set; get; }

        public DateTime Date { set; get; }

        public int CrewID { set; get; }

        public bool ApprovalStatus { set; get; }

        public bool ApprovalHistory { set; get; }

    }
}
