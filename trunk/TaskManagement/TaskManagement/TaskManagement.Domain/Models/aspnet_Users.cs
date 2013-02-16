using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Web.Models
{
    public class aspnet_Users
    {
        [Key]
        public Guid UserID { get; set; }

        public string UserName { get; set; }
    }
}