using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Review
{
    public record CreateReviewCommand : IRequest<ResponseContract<ReviewEntity>>
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
    }
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId обязателен");

            RuleFor(x => x.InstitutionId)
                .NotEmpty().WithMessage("InstitutionId обязателен");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Комментарий обязателен")
                .MaximumLength(10000).WithMessage("Комментарий не более 10.000 символов");

            RuleFor(x => x.Score)
                .InclusiveBetween(1, 5).WithMessage("Оценка должна быть от 1 до 5");
        }
    }

    public sealed class CreateReviewCommandHandler(
        IReviewRepository reviewRepository,
        IUserRepository userRepository,
        IInstitutionRepository institutionRepository) : IRequestHandler<CreateReviewCommand, ResponseContract<ReviewEntity>>
    {
        public async Task<ResponseContract<ReviewEntity>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = await userRepository.GetByIdAsync(request.UserId);
            InstitutionEntity institution = await institutionRepository.GetByIdAsync(request.InstitutionId);

            if (user == null)
            {
                return new ResponseContract<ReviewEntity>(ErrorCodes.UserNotFound);
            }

            if (user.IsBanned)
            {
                return new ResponseContract<ReviewEntity>(ErrorCodes.UserIsBanned);
            }

            if (user.IsMuted)
            {
                return new ResponseContract<ReviewEntity>(ErrorCodes.UserIsMuted);
            }

            if (institution == null)
            {
                return new ResponseContract<ReviewEntity>(ErrorCodes.InstitutionNotFound);
            }

            ReviewEntity entity = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                InstitutionId = request.InstitutionId,
                Comment = request.Comment,
                Score = request.Score,
                CreatedAt = DateTime.UtcNow
            };

            await reviewRepository.AddAsync(entity);

            return new ResponseContract<ReviewEntity>(entity);
        }
    }
}