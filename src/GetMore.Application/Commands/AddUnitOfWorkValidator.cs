using System;
using FluentValidation;

namespace GetMore.Application.Commands
{
    public class AddUnitOfWorkValidator : AbstractValidator<AddUnitOfWorkCommand>
    {
        public AddUnitOfWorkValidator()
        {
            this.RuleFor(x => x.TimeAmount).LessThanOrEqualTo(TimeSpan.FromHours(24));
        }
    }
}