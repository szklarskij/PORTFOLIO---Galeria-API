using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GaleriaT_API.Entities
{
    public class GalleryPost
    {

        public int Id { get; set; }
       

        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string SmallImageUrl { get; set; }
        public string ThumbImageUrl { get; set; }
        public string ImageId { get; set; }
        public string Text { get; set; }
        public int Price { get; set; }
        public string Size { get; set; }
        public int SizeMultiplied { get; set; }
        public string Technique { get; set; }
        public string Tag { get; set; }
 



        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime DateOfWork { get; set; }

    }
}
