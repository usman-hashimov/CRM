using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Domain.Entities.Models
{
    public class UserModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public DateTime LastLogin { get; set; } = DateTime.UtcNow;
        public Guid LocationId { get; set; }
        public LocationModel Location { get; set; }
        public List<InteractionModel> Interactions { get; set; }
        public SalesRepModel SalesRep { get; set; }
        public List<TaskModel> Tasks { get; set; }
    }
}
