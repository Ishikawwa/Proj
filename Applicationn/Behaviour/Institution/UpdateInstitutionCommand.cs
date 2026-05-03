using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record UpdateInstitutionCommand : IRequest
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

    public sealed class UpdateInstitutionCommandHandler(IInstitutionRepository repository) : IRequestHandler<UpdateInstitutionCommand>
    {
        public async Task Handle(UpdateInstitutionCommand request, CancellationToken cancellationToken)
        {
            InstitutionEntity? entity = await repository.GetByIdAsync(request.Id);
            if (entity == null)
                throw new InvalidOperationException($"Заведение с Id={request.Id} не найдено");

            entity.Name = request.Name;
            entity.Address = request.Address;
            entity.Type = request.Type;
            entity.AveragePrice = request.AveragePrice;
            entity.WebUrl = request.WebUrl;

            await repository.UpdateAsync(entity);
        }
    }
}