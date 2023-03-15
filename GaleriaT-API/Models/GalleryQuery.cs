using Microsoft.AspNetCore.Mvc;

namespace GaleriaT_API.Models
{
    public class GalleryQuery
    {
        public string SearchPhrase { get; set; }
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }


    }
}
