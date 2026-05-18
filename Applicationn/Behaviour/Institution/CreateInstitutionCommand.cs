using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record CreateInstitutionCommand : IRequest<ResponseContract<InstitutionEntity>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long Longitude { get; set; }
        public long Latitude { get; set; }
        public string Address { get; set; }
        public InstitutionTypeEnum Type { get; set; }
        public int? AveragePrice { get; set; }
        public string? WebUrl { get; set; }
    }

    public class CreateInstitutionCommandValidator : AbstractValidator<CreateInstitutionCommand>
    {
        public CreateInstitutionCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(200).WithMessage("Название не более 200 символов");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Адрес обязателен")
                .MaximumLength(500).WithMessage("Адрес не более 500 символов");

            RuleFor(x => x.Latitude)
                .Must(x => x >= 598000 && x <= 601000)
                .WithMessage("Широта должна быть в пределах Санкт-Петербурга");

            RuleFor(x => x.Longitude)
                .Must(x => x >= 301000 && x <= 306000)
                .WithMessage("Долгота должна быть в пределах Санкт-Петербурга");

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

    public sealed class CreateInstitutionCommandHandler(IInstitutionRepository institutionRepository) : IRequestHandler<CreateInstitutionCommand, ResponseContract<InstitutionEntity>>
    {
        public async Task<ResponseContract<InstitutionEntity>> Handle(CreateInstitutionCommand request, CancellationToken cancellationToken)
        {
            InstitutionEntity institution = await institutionRepository.GetByIdAsync(request.Id);

            if (institution == null)
            {
                return new ResponseContract<InstitutionEntity>(ErrorCodes.InstitutionNotFound);
            }

            InstitutionEntity entity = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Longitude = request.Longitude,
                Latitude = request.Latitude,
                Address = request.Address,
                Type = request.Type,
                AveragePrice = request.AveragePrice,
                WebUrl = request.WebUrl,
                Rating = 0,
                PhotoUrls = new List<string>(),
                Labels = new List<LabelTypeEnum>()
            };

            await institutionRepository.AddAsync(entity);

            return new ResponseContract<InstitutionEntity>(entity);
        }
    }
}