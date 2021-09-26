using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FinalAssignment.DAL.Data.Models
{
    public class Minute
    {

        public Minute()
        {
            SupervisorID = new List<Supervisor>();
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int ID { set; get; }

        [Required]
        public List<Supervisor> SupervisorID { set; get; }

        [Required(ErrorMessage = "Please Select Minute Type")]
        public MinuteType MinuteType { set; get; }

        [Required(ErrorMessage = "Please provide Topic")]
        public int Topic { set; get; }

        [Required(ErrorMessage = "Please Provide Date")]
        public DateTime Date { set; get; }

        [Required]
        public int CrewID { set; get; }

        public bool ApprovalStatus { set; get; }

        public bool ApprovalHistory { set; get; }

        public DateTime? CreatedAt { set; get; }

        public DateTime? UpdatedAt { set; get; }

        public bool? IsActive { set; get; }

        [ForeignKey(nameof(SupervisorID))]
        [InverseProperty("Minutes")]
        public virtual ICollection<Employee> Employees { set; get; }

        [ForeignKey(nameof(CrewID))]
        [InverseProperty("Minutes")]
        public virtual Crew Crews { set; get; }
    }

    public enum MinuteType
    {
        Weekly_Crew_Meeting,
        Evacuation_Drill,
        EFAP,
        FirstAid
    }

    public class Supervisor
    {
        public int ID { get; set; }
        public int SupervisorID { get; set; }
    }
}
