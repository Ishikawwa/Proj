using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record GetInstitutionByIdCommand : IRequest<ResponseContract<InstitutionEntity>>
    {
        public Guid Id { get; set; }
    }

    public class GetInstitutionByIdCommandValidator : AbstractValidator<GetInstitutionByIdCommand>
    {
        public GetInstitutionByIdCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id заведения обязателен");
        }
    }

    public sealed class GetInstitutionByIdQueryHandler(IInstitutionRepository repository) : IRequestHandler<GetInstitutionByIdCommand, ResponseContract<InstitutionEntity>>
    {
        public async Task<ResponseContract<InstitutionEntity>> Handle(GetInstitutionByIdCommand request, CancellationToken cancellationToken)
        {
            InstitutionEntity institution = await repository.GetByIdAsync(request.Id);
            if (institution == null)
            {
                return new ResponseContract<InstitutionEntity>(ErrorCodes.InstitutionNotFound);
            }

            return new ResponseContract<InstitutionEntity>(institution);
        }
    }
}