using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TaskManagement.Web.Models.Models;

namespace TaskManagement.Web.Models
{
    public class TaskSkill : IEntity
    {
        [ScaffoldColumn(false)]
        public virtual int ID { get; set; }

        [DisplayName("Task")]
        public virtual int TaskID { get; set; }

        [DisplayName("Skill")]
        public virtual int SkillID { get; set; }

        public virtual Task Task { get; set; }
        public virtual Skill Skill { get; set; }
    }
}