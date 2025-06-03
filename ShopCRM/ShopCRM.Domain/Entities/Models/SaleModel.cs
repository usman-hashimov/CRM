using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Domain.Entities.Models
{
    public class SaleModel
    {
        public Guid Id { get; set; }
        public double TotalAmount { get; set; } = 0;
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public List<SaleItemModel> SaleItems { get; set; }
        public Guid SalesRepId { get; set; }
        public SalesRepModel SalesRep { get; set; }
        public Guid LocationId { get; set; }
        public LocationModel Location { get; set; }
    }
}
