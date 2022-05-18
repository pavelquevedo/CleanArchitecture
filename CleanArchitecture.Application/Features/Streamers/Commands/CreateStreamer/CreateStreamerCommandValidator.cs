using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommandValidator : AbstractValidator<CreateStreamerCommand>
    {
        public CreateStreamerCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{Name} can't be blank")
                .NotNull()
                .MaximumLength(50).WithMessage("{Name} can't exceed 50 characters");

            RuleFor(p => p.Url)
                .NotEmpty().WithMessage("{Url} can't be blank");
        }
    }
}
