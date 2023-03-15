using FluentValidation;

namespace GaleriaT_API.Models.Validators
{
    public class UpdateAdminPasswordDtoValidator : AbstractValidator<AdminPasswordDto>
    {
        public UpdateAdminPasswordDtoValidator()
        {
            RuleFor(x=>x.Password).NotEmpty().MinimumLength(6);
        }
    }
}
