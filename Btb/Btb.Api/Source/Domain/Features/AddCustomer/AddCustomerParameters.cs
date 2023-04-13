using Btb.Api.Source.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Btb.Api.Source.Domain.Features.AddCustomer
{
    public class AddCustomerParameters
    {
        [Required]
        public CustomerType CustomerType { get; set; }

        public List<CustomerDetail> CustomerDetails { get; set; } = new List<CustomerDetail>();

        public class CustomerDetail
        {
            [Required]
            public string MetaKey { get; set; }

            [Required]
            public string MetaValue { get; set; }
        }

        public List<Dependent> Dependents { get; set; } = new List<Dependent>();

        public class Dependent
        {
            public List<DependentDetail> DependentDetails { get; set; }

            public class DependentDetail
            {
                [Required]
                public string MetaKey { get; set; }

                [Required]
                public string MetaValue { get; set; }
            }
        }
    }
}
