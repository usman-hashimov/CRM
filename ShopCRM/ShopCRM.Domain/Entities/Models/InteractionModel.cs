using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Domain.Entities.Models
{
    public class InteractionModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; }
        public Guid UserId { get; set; }
        public UserModel User { get; set; }
    }
}
