using Discounts.Application.DTOs.Request;
using FluentValidation;

namespace Discounts.Application.Validators;

public class ReservationUpdateDtoValidator : AbstractValidator<ReservationUpdateDto>
{
    public ReservationUpdateDtoValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
}
