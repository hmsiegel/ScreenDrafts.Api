namespace ScreenDrafts.Api.Application.Authentication.Users;
public class RegisterUserRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterUserRequestValidator(IUserService userService)
    {
        RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
                .WithMessage("Email is invalid")
            .MustAsync(async (email, _) => !await userService.ExistsWithEmailAsync(email))
                .WithMessage("Email already exists");

        RuleFor(x => x.UserName).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MustAsync(async (userName, _) => !await userService.ExistsWithNameAsync(userName))
                .WithMessage("Username already exists");

        RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters")
            .Matches("[A-Z]")
                .WithMessage("Password must contain at least 1 uppercase letter")
            .Matches("[a-z]")
                .WithMessage("Password must contain at least 1 lowercase letter")
            .Matches("[0-9]")
                .WithMessage("Password must contain at least 1 number")
            .Matches("[^a-zA-Z0-9]")
                .WithMessage("Password must contain at least 1 special character");

        RuleFor(x => x.PhoneNumber).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Matches(@"^\+[1-9]\d{1,14}$")
                .WithMessage("Phone number is invalid")
            .MustAsync(async (phoneNumber, _) => !await userService.ExistsWithPhoneNumberAsync(phoneNumber))
                .WithMessage("Phone number already exists");

        RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(50)
                .WithMessage("First name must not exceed 50 characters");

        RuleFor(x => x.LastName).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(50)
                .WithMessage("Last name must not exceed 50 characters");

        RuleFor(x => x.ConfirmPassword).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Equal(x => x.Password)
                .WithMessage("Passwords must match");
    }
}
