using FluentValidation;

namespace TasteUfes.Models.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty();

            RuleFor(u => u.LastName)
                .NotEmpty();

            RuleFor(u => u.Username)
                .NotEmpty();

            RuleSet("credentials", () =>
            {
                RuleFor(u => u.Password)
                    .NotEmpty();

                RuleFor(u => u.Email)
                    .NotEmpty()
                    .EmailAddress();
            });
        }
    }
}