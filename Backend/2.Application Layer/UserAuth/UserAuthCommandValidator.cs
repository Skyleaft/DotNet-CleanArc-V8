using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.UserAuth
{
    public class UserAuthCommandValidator : AbstractValidator<UserAuthCommand>
    {
        public UserAuthCommandValidator()
        {
        }
    }
}
