using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Domain.Entities.ViewModels
{
    public class SaleViewModel
    {
        public double TotalAmount { get; set; }
        public DateTime SaleDate { get; set; }
        public List<SaleItemViewModel> SaleItems { get; set; }
        public string SalesRepName { get; set; }
        public string SalesRepSurname { get; set; }
        public string Location { get; set; }
    }
}

