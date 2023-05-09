using System.ComponentModel.DataAnnotations;

namespace LabbAPI.Models.DTO
{
    public class InterestDto
    {
        public int InterestId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
