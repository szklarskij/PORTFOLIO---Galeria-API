using FluentValidation;
using GaleriaT_API.Entities;

namespace GaleriaT_API.Models.Validators
{
    public class GalleryQueryValidator : AbstractValidator<GalleryQuery>
    {

        private string[] allowedSortByColumnNames = { 
            nameof(GalleryPost.Id), nameof(GalleryPost.Title), nameof(GalleryPost.DateOfWork), nameof(GalleryPost.CreatedDate), nameof(GalleryPost.SizeMultiplied)
        };
        public GalleryQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);

            RuleFor(r => r.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(", ", allowedSortByColumnNames)}]");
        }
    }
}
