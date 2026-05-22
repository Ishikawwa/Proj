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
            SpamReportEntity report = await spamReportRepository.GetByIdAsync(request.Id);

            if (report == null)
            {
                return new ResponseContract<Unit>(ErrorCodes.ReviewNotFound);
            }

            await spamReportRepository.MarkAsProcessedAsync(request.Id);

            return new ResponseContract<Unit>(Unit.Value);
        }


    }
}