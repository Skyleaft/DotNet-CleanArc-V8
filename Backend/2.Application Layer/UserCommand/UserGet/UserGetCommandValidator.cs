using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.UserCommand.UserGet
{
    public class UserGetCommandValidator : AbstractValidator<UserGetCommand>
    {
        public UserGetCommandValidator()
        {
        }
    }
}
