using FluentValidation;
using Movies.ItAcademy.API.Models.Requests.Account;

namespace Movies.ItAcademy.API.Infrastructure.Validators
{
    public class LoginRequestValidator: AbstractValidator<AccountLogInRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Password)
               .Matches("^(?=.*?[a-z])(?=.*?[0-9]).{6,50}$");

            RuleFor(x => x.UserName)
                    .Matches("^(?=.{2,30}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$");
        }
       
    }
}
