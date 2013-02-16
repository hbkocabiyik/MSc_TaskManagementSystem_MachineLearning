using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TaskManagement.Web.Models.Models;

namespace TaskManagement.Web.Models
{
    public class EmployeeSkill : IEntity
    {
        [ScaffoldColumn(false)]
        public virtual int ID { get; set; }

        [DisplayName("Employee")]
        public virtual int EmployeeID { get; set; }

        [DisplayName("Skill")]
        public virtual int SkillID { get; set; }

        [Required]
        public virtual int Experience { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
