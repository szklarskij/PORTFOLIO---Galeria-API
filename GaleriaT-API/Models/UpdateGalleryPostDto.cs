using System.ComponentModel.DataAnnotations;

namespace GaleriaT_API.Models
{
    public class UpdateGalleryPostDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        public string Text { get; set; }
        [Required]
        public DateTime DateOfWork { get; set; }
        public int Price { get; set; }
        public string Size { get; set; }
        public int SizeMultiplied { get; set; }

        public string Technique { get; set; }
        public string Tag { get; set; }
    }
}
