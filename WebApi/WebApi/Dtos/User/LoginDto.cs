using FluentValidation;

namespace WebApi.Dtos.User
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(u => u.Username).NotEmpty().MinimumLength(4).MaximumLength(20);
            RuleFor(u => u.Password).NotEmpty().MinimumLength(6).MaximumLength(16);
        }
    }
}
