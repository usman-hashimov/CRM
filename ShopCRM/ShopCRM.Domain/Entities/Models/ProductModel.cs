using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Domain.Entities.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        public List<SaleItemModel> SaleItems { get; set; }
    }
}
