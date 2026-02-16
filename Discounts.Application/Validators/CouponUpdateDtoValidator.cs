using Discounts.Application.DTOs.Request;
using FluentValidation;

namespace Discounts.Application.Validators;

public class CouponUpdateDtoValidator : AbstractValidator<CouponUpdateDto>
{
    public CouponUpdateDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(50).WithMessage("Code must not exceed 50 characters.");

        RuleFor(x => x.Discount)
            .GreaterThan(0).WithMessage("Discount must be greater than 0.");

        RuleFor(x => x.OfferId)
            .GreaterThan(0).WithMessage("Offer is required.");
    }
}
