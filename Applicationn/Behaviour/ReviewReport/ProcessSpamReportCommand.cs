using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.ReviewReport
{
    public record ProcessSpamReportCommand : IRequest<ResponseContract<Unit>>
    {
        public Guid Id { get; set; }
    }
    public class ProcessSpamReportCommandValidator : AbstractValidator<ProcessSpamReportCommand>
    {
        public ProcessSpamReportCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id жалобы обязателен");
        }
    }
    public sealed class ProcessSpamReportCommandHandler(ISpamReportRepository spamReportRepository, IReviewRepository reviewRepository) : IRequestHandler<ProcessSpamReportCommand, ResponseContract<Unit>>
    {
        public async Task<ResponseContract<Unit>> Handle(ProcessSpamReportCommand request, CancellationToken cancellationToken)
        {
            ReviewEntity review = await reviewRepository.GetByIdAsync(request.Id);

            if (review == null)
            {
                return new ResponseContract<Unit>(ErrorCodes.ReviewNotFound);
            }

            if (review.IsBanned)
            {
                return new ResponseContract<Unit>(ErrorCodes.ReviewIsBanned);
            }

            spamReportRepository.MarkAsProcessedAsync(request.Id);

            return new ResponseContract<Unit>(Unit.Value);
        }


    }
}