using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Domain.Entities.Models
{
    public class LocationModel
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public List<UserModel> Users { get; set; }
        public List<SaleModel> Sales { get; set; }
    }
}
