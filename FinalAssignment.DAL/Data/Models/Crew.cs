using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FinalAssignment.DAL.Data.Models
{
    public class Crew
    {

        public Crew()
        {
            Minutes = new HashSet<Minute>();
        }

        [Key]
        public int ID { set; get; }

        [Required(ErrorMessage = "Please provide name of Crew")]
        public string Name { set; get;}

        public DateTime? CreatedAt { set; get;}

        public DateTime? UpdatedAt { set; get;}

        public bool? IsActive { set; get;}

        [InverseProperty("Crews")]
        public virtual ICollection<Minute> Minutes { set; get; }
    }
}
