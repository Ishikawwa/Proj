using MediatR;

namespace Application.Behaviour.Institution
{
    public record DeleteInstitutionCommand : IRequest
    {
        public Guid Id { get; set; }
        public bool IsArchive { get; set; }
    }

    public sealed class DeleteInstitutionCommandHandler(IInstitutionRepository repository) : IRequestHandler<DeleteInstitutionCommand>
    {
        public async Task Handle(DeleteInstitutionCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteAsync(request.Id);
        }
    }
}