using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record UpdateInstitutionCommand : IRequest<ResponseContract<Unit>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public InstitutionTypeEnum Type { get; set; }
        public int? AveragePrice { get; set; }
        public string? WebUrl { get; set; }
    }

    public class UpdateInstitutionCommandValidator : AbstractValidator<UpdateInstitutionCommand>
    {
        public UpdateInstitutionCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id заведения обязателен");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(200).WithMessage("Название не более 200 символов");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Адрес обязателен")
                .MaximumLength(500).WithMessage("Адрес не более 500 символов");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Недопустимый тип заведения");

            RuleFor(x => x.AveragePrice)
                .GreaterThan(0).When(x => x.AveragePrice.HasValue)
                .WithMessage("Средний чек должен быть положительным");

            RuleFor(x => x.WebUrl)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.WebUrl))
                .WithMessage("WebUrl некорректный");
        }
    }

    public sealed class UpdateInstitutionCommandHandler(IInstitutionRepository institutionRepository) : IRequestHandler<UpdateInstitutionCommand, ResponseContract<Unit>>
    {
        public async Task<ResponseContract<Unit>> Handle(UpdateInstitutionCommand request, CancellationToken cancellationToken)
        {
            InstitutionEntity? institution = await institutionRepository.GetByIdAsync(request.Id);
            if (institution == null)
            {
                return new ResponseContract<Unit>(ErrorCodes.InstitutionNotFound);
            }


            institution.Name = request.Name;
            institution.Address = request.Address;
            institution.Type = request.Type;
            institution.AveragePrice = request.AveragePrice;
            institution.WebUrl = request.WebUrl;

            await institutionRepository.UpdateAsync(institution);

            return new ResponseContract<Unit>(Unit.Value);
        }
    }
}