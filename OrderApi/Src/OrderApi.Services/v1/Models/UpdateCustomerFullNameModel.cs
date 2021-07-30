using System;

namespace OrderApi.Services.v1.Models
{
    public class UpdateCustomerFullNameModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class OrderModel
    {
        public Guid CustomerGuid { get; set; }
        public string CustomerFullName { get; set; }
    }
}
