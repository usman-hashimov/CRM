using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.Services
{
    public interface ITokenService
    {
        public string GenerateToken(UserModel user);
    }
}
