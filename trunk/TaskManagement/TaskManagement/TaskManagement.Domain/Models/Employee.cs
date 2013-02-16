using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TaskManagement.Web.Models.Models;

namespace TaskManagement.Web.Models
{
    public class Employee : IEntity
    {
        [ScaffoldColumn(false)]
        public virtual int ID { get; set; }

        [Required]
        public virtual string Login { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Surname { get; set; }

        [Required]
        public virtual int Role { get; set; }

        [Required]
        [DisplayName("Employment Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public virtual DateTime EmploymentDate { get; set; }

        [Required]
        [DisplayName("Position")]
        public virtual int PositionLevel { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }

    public enum Role
    {
        Manager,
        Engineer
    }
}
