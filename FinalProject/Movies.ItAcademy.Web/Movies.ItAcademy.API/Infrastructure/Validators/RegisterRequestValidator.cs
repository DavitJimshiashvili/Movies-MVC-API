using FluentValidation;
using Movies.ItAcademy.API.Models.Requests.Account;

namespace Movies.ItAcademy.API.Infrastructure.Validators
{
    public class RegisterRequestValidator:AbstractValidator<AccountRegisterRequest>
    {
        public RegisterRequestValidator()
        {
           
            RuleFor(x => x.FirstName)
               .Matches("[A-Za-z]{2,30}");

            RuleFor(x => x.LastName)
                .MinimumLength(2)
                .MaximumLength(30)
                .Matches("[A-Za-z]");
            RuleFor(x => x.Email)
               .Matches("^(?=.{1,64}@)[A-Za-z0-9_-]+(\\.[A-Za-z0-9_-]+)*@[^-][A-Za-z0-9-]+(\\.[A-Za-z0-9-]+)*(\\.[A-Za-z]{2,})$");

            RuleFor(x => x.Password)
                .Matches("^(?=.*?[a-z])(?=.*?[0-9]).{6,50}$");

            RuleFor(x => x.UserName)
                .Matches("^(?=.{2,30}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9@._]+(?<![_.])$");
        }
    }
}
