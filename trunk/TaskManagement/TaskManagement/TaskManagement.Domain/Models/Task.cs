using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TaskManagement.Web.Models.Models;

namespace TaskManagement.Web.Models
{
    public class Task : IEntity
    {
        [ScaffoldColumn(false)]
        public virtual int ID { get; set; }

        [DisplayName("Employee")]
        [Required]
        public virtual int EmployeeID { get; set; }

        [DisplayName("Area")]
        [Required]
        public virtual int AreaID { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Description { get; set; }

        [Required]
        public virtual int Status { get; set; }

        [DisplayName("Estimation")]
        [DisplayFormat(DataFormatString = "{0:F}")]
        [Required]
        public virtual double EstimatedTime { get; set; }

        [DisplayName("Completed")]
        [DisplayFormat(DataFormatString = "{0:F}")]
        [Required]
        public virtual double SpentTime { get; set; }

        [DisplayName("Remaining")]
        [DisplayFormat(DataFormatString = "{0:F}")]
        [Required]
        public virtual double RemainingTime { get; set; }

        [Required]
        public virtual int Priority { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Area Area { get; set; }
        public virtual ICollection<TaskSkill> TaskSkills { get; set; }
    }
}
