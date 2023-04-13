using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Btb.Api.Source.Domain.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        [Required]
        public int CustomerType { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;

        [Required]
        public bool IsActive { get; set; } = true;

        public virtual ICollection<CustomerDetail> CustomerDetails { get; set; } = new HashSet<CustomerDetail>();
        public virtual ICollection<Dependent> Dependents { get; set; } = new HashSet<Dependent>();
    }
}
