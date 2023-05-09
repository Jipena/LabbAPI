using System.ComponentModel.DataAnnotations;

namespace LabbAPI.Models.DTO
{
    public class PersonUpdateDto
    {
        [Required]
        public int PersonId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNr { get; set; }
    }
}
