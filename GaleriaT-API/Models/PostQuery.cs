using Microsoft.AspNetCore.Mvc;

namespace GaleriaT_API.Models
{
    public class PostQuery
    {
        public string SearchPhrase { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }

    }
}
