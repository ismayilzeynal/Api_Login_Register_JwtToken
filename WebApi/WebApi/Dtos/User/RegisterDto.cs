using FluentValidation;

namespace WebApi.Dtos.User
{
    public class RegisterDto
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }

    public class RegisterDtoValidator:AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(u=>u.Fullname).NotEmpty().MinimumLength(5).MaximumLength(32);
            RuleFor(u => u.Username).NotEmpty().MinimumLength(4).MaximumLength(20);
            RuleFor(u => u.Password).NotEmpty().MinimumLength(6).MaximumLength(16);
            RuleFor(u => u.RePassword).NotEmpty().MinimumLength(6).MaximumLength(16);

            RuleFor(u => u).Custom((u, context) =>
            {
                if(u.Password!=u.RePassword)
                {
                    context.AddFailure("Password", "Does not match");
                }
            });


        }
    }
}
