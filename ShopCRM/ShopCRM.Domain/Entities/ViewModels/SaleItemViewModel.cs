using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Domain.Entities.ViewModels
{
    public class SaleItemViewModel
    {
        public int Quantity { get; set; }
        public double TotalAmount { get; set; }
        public string ProductName { get; set; }
    }
}
