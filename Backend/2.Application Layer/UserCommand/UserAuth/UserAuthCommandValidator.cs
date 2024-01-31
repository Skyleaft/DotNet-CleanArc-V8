using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.UserCommand.UserAuth
{
    public class UserAuthCommandValidator : AbstractValidator<UserAuthCommand>
    {
        public UserAuthCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3);
            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(8);
        }
    }
}
