using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Domain.Entities.ViewModels
{
    public class CreateSaleViewModel
    {
        public Guid SalesRepId { get; set; }
        public Guid LocationId { get; set; }
        public List<CreateSaleItemViewModel> SaleItems { get; set; } = new List<CreateSaleItemViewModel>();
    }
}
