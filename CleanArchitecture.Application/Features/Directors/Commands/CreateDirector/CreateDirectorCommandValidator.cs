using FluentValidation;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotNull().WithMessage("{FirstName} can't be null");
            RuleFor(p => p.LastName)
                .NotNull().WithMessage("{FirstName} can't be null");
        }
    }
}
