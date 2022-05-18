using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandHandler : IRequestHandler<UpdateStreamerCommand>
    {
        private readonly IStreamerRepository _streamerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateStreamerCommandHandler> _logger;

        public UpdateStreamerCommandHandler(IStreamerRepository streamerRepository, IMapper mapper, ILogger<UpdateStreamerCommandHandler> logger)
        {
            _streamerRepository = streamerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToUpdate = await _streamerRepository.GetByIdAsync(request.Id);

            if (streamerToUpdate != null)
            {
                _mapper.Map(request, streamerToUpdate, typeof(UpdateStreamerCommand), typeof(Streamer));
                await _streamerRepository.UpdateAsync(streamerToUpdate);

                _logger.LogInformation($"Update operation was succesfully completed on Streamer {request.Id}");

                return Unit.Value;
            }
            else
            {
                _logger.LogError($"Streamer with Id {request.Id} was not found");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }           
        }
    }
}
