using System;
using System.Collections.Generic;
using System.Text;
using OrderApi.Services.v1.Models;

namespace OrderApi.Service.v1.Services
{
    public interface ICustomerNameUpdateService
    {
        void UpdateCustomerNameInOrders(UpdateCustomerFullNameModel updateCustomerFullNameModel);
    }
}