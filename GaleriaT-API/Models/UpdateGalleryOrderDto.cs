using System.ComponentModel.DataAnnotations;

namespace GaleriaT_API.Models
{
    public class UpdateGalleryOrderDto
    {
        [Required]
        public int IdToSwap { get; set; }
    }
}
