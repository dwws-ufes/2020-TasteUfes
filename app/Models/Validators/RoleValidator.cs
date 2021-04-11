using FluentValidation;

namespace TasteUfes.Models.Validators
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(f => f.Name)
                .NotEmpty();
        }
    }
}