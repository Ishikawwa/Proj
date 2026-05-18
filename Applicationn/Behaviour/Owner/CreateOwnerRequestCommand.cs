using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.OwnerRequest
{
    public record CreateOwnerRequestCommand : IRequest<ResponseContract<OwnerRequestEntity>>
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Comment { get; set; }
    }
    public class CreateOwnerRequestCommandValidator : AbstractValidator<CreateOwnerRequestCommand>
    {
        public CreateOwnerRequestCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId обязателен");

            RuleFor(x => x.InstitutionId)
                .NotEmpty().WithMessage("InstitutionId обязателен");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Комментарий обязателен")
                .MaximumLength(10000).WithMessage("Комментарий не более 10.000 символов");
        }
    }

    public sealed class CreateOwnerRequestCommandHandler(
        IOwnerRequestRepository repository,
        IUserRepository userRepository) : IRequestHandler<CreateOwnerRequestCommand, ResponseContract<OwnerRequestEntity>>
    {
        public async Task<ResponseContract<OwnerRequestEntity>> Handle(CreateOwnerRequestCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = await userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                return new ResponseContract<OwnerRequestEntity>(ErrorCodes.UserNotFound);
            }

            if (user.IsBanned)
            {
                return new ResponseContract<OwnerRequestEntity>(ErrorCodes.UserIsBanned);
            }

            OwnerRequestEntity entity = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                InstitutionId = request.InstitutionId,
                Comment = request.Comment,
            };

            await repository.AddAsync(entity);

            return new ResponseContract<OwnerRequestEntity>(entity);
        }
    }
}