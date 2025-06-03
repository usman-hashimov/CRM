using FluentValidation;
using ShopCRM.Application.UseCases.User.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.Validators
{
    public class UserValidator : AbstractValidator<CreateUserCommand>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Must(isValidDomain);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[A-Z]")
                .Matches("[a-z]")
                .Matches("[0-9]");

        }

        public bool isValidDomain(string email)
        {
            var domainpart = email.Split('@').LastOrDefault();
            return domainpart != null && domainpart.Contains('.');
        }
    }
}
