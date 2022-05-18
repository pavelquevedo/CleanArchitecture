using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandValidator : AbstractValidator<UpdateStreamerCommand>
    {
        public UpdateStreamerCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().WithMessage("{Name} doesn't allow null values");

            RuleFor(p => p.Url)
                .NotNull().WithMessage("{Url} doesn't allow null values");
        }
    }
}
