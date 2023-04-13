using Btb.Api.Source.Domain.Enums;

namespace Btb.Api.Source.Domain.Features.GetCustomers
{
    public class GetCustomersResult
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();

        public class Customer
        {
            public int CustomerId { get; set; }
            public CustomerType CustomerType { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedDate { get; set; }   

            public List<CustomerDetail> CustomerDetails { get; set; } = new List<CustomerDetail>();

            public class CustomerDetail
            {
                public int CustomerDetailId { get; set; }
                public int CustomerId { get; set; } 
                public string MetaKey { get; set; }
                public string MetaValue { get; set; }
            }

            public List<Dependent> Dependents { get; set; } = new List<Dependent>();

            public class Dependent
            {
                public int DependentId { get; set; }
                public List<DependentDetail> DependentDetails { get; set; }
                public bool IsActive { get; set; }
                public DateTime CreatedDate { get; set; }

                public class DependentDetail
                {
                    public int DependentDetailId { get; set; }
                    public int DependentId { get; set; }    
                    public string MetaKey { get; set; }
                    public string MetaValue { get; set; }
                }
            }
        }
    }
}
