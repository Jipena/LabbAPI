using System.ComponentModel.DataAnnotations;

namespace LabbAPI.Models.DTO
{
    public class WebURLDto
    {
        [Required]
        public int FkPersonId { get; set; }
        [Required]
        public int FkInterestId { get; set; }
        [Required]
        public string WebLink { get; set; }
    }
}
