using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabbAPI.Models.DTO
{
    public class PersonInterestDto
    {
        [Required]
        public int FkPersonId { get; set; }
        [Required]
        public int FkInterestId { get; set; }
    }
}
