using Discounts.Application.DTOs.Request;
using FluentValidation;

namespace Discounts.Application.Validators;

public class ReservationCreateDtoValidator : AbstractValidator<ReservationCreateDto>
{
    public ReservationCreateDtoValidator()
    {
        RuleFor(x => x.OfferId)
            .GreaterThan(0).WithMessage("Offer is required.");

        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("Customer is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
}
