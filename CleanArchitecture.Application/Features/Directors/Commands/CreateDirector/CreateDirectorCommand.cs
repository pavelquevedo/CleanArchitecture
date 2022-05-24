using MediatR;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int VideoId { get; set; }

    }
}
