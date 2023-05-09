using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabbAPI.Models
{
    public class Interest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InterestId { get; set; }
        [Required]
        public string Title { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
