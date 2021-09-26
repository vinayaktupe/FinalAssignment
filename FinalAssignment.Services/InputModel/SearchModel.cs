using FinalAssignment.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalAssignment.Services.InputModel
{
    public class SearchModel
    {
        public string Crew { get; set; }

        public int Supervisor { get; set; }

        public MinuteType Type { get; set; }
        public Month Month { get; set; }
        public int Year { get; set; }

    }
}
