using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Domain.Entities.Models
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Active";
        public Guid AssignedTo { get; set; }
        public UserModel User { get; set; }
    }
}
