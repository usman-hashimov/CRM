using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Domain.Entities.ViewModels
{
    public class CreateSaleItemViewModel
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }

    }
}
