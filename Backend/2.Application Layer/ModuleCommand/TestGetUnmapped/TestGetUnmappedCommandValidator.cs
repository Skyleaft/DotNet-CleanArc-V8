using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.ModuleCommand.TestGetUnmapped
{
    public class TestGetUnmappedCommandValidator : AbstractValidator<TestGetUnmappedCommand>
    {
        public TestGetUnmappedCommandValidator()
        {
        }
    }
}
