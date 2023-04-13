using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Btb.Api.Source.Domain.Entities
{
    public class CustomerDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerDetailID { get; set; }

        [Required]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [Required]
        [StringLength(50)]
        public string MetaKey { get; set; }

        [Required]
        public string MetaValue { get; set; }
    }
}
