using AutoMapper;
using GaleriaT_API.Entities;
using GaleriaT_API.Models;

namespace GaleriaT_API
{
    public class GalleryMappingProfile : Profile
    {
        public GalleryMappingProfile()
        {
            CreateMap<GalleryPost, GalleryPostDto>();
            CreateMap<GalleryPost, ThumbnailGalleryDto>();

            //.ForMember(m => m.Title, c => c.MapFrom(s => s.Title))
            //.ForMember(m => m.Text, c => c.MapFrom(s => s.Text))
            //.ForMember(m => m.ImageId, c => c.MapFrom(s => s.ImageId))
            //.ForMember(m => m.CreatedDate, c => c.MapFrom(s => s.CreatedDate));

            CreateMap<CreateGalleryPostDto, GalleryPost>();

        }
    }
}
