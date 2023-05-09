using System.ComponentModel.DataAnnotations;

namespace LabbAPI.Models.DTO
{
    public class InterestUpdateDto
    {
        [Required]
        public int InterestId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
