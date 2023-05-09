using System.ComponentModel.DataAnnotations;

namespace LabbAPI.Models.DTO
{
    public class PersonCreateDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNr { get; set; }
    }
}
