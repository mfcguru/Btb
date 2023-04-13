using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Btb.Api.Source.Domain.Entities
{
    public class Dependent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DependentID { get; set; }

        [Required]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;

        [Required]
        public bool IsActive { get; set; } = true;

        public virtual ICollection<DependentDetail> DependentDetails { get; set; } = new HashSet<DependentDetail>();    
    }
}
