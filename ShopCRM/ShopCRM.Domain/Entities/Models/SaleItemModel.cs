using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Domain.Entities.Models
{
    public class SaleItemModel
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double TotalAmount { get; set; }
        public Guid ProductId { get; set; }
        public ProductModel Product { get; set; }
        public Guid SaleId { get; set; }
        public SaleModel Sale { get; set; }
    }
}
