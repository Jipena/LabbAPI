using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace LabbAPI.Models
{
    public class PersonInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonInterestId { get; set; }
        [ForeignKey("Person")]
        public int FkPersonId { get; set; }
        public Person Person { get; set; }
        [ForeignKey("Interest")]
        public int FkInterestId { get; set; }
        public Interest Interest { get; set; }
    }
}
