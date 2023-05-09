using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LabbAPI.Models
{
    public class WebURL
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WebURLId { get; set; }
        public string WebLink { get; set; }
        [ForeignKey("Person")]
        public int FkPersonId { get; set; }
        public Person Person { get; set; }
        [ForeignKey("Interest")]
        public int FkInterestId { get; set; }
        public Interest Interest { get; set; }
    }
}
