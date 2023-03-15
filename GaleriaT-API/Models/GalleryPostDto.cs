namespace GaleriaT_API.Models
{
    public class GalleryPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbImageUrl { get; set; }
        public string SmallImageUrl { get; set; }
        public string Text { get; set; }
        public int Price { get; set; }
        public string Size { get; set; }
        public int SizeMultiplied { get; set; }

        public string Technique { get; set; }
        public string Tag { get; set; }
        public Array IdArray { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateOfWork { get; set; }
    }
}