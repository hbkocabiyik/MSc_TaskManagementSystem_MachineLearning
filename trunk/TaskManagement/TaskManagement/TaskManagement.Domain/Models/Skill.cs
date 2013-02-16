using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaskManagement.Web.Models.Models;

namespace TaskManagement.Web.Models
{
    public class Skill : IEntity
    {
        [ScaffoldColumn(false)]
        public virtual int ID { get; set; }

        [Required]
        public virtual string Name { get; set; }

        public virtual ICollection<TaskSkill> AreaSkills { get; set; }
        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
