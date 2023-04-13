using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Btb.Api.Source.Domain.Entities
{
    public class DependentDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DependentDetailID { get; set; }

        [Required]
        public int DependentID { get; set; }
        public virtual Dependent Dependent { get; set; }

        [Required]
        [StringLength(50)]
        public string MetaKey { get; set; }

        [Required]
        public string MetaValue { get; set; }
    }
}
