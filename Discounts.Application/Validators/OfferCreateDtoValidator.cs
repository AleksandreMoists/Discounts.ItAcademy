using Discounts.Application.DTOs.Request;
using FluentValidation;

namespace Discounts.Application.Validators;

public class OfferCreateDtoValidator : AbstractValidator<OfferCreateDto>
{
    public OfferCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.OriginalPrice)
            .GreaterThan(0).WithMessage("Original price must be greater than 0.");

        RuleFor(x => x.DiscountPrice)
            .GreaterThan(0).WithMessage("Discount price must be greater than 0.")
            .LessThan(x => x.OriginalPrice).WithMessage("Discount price must be less than original price.");

        RuleFor(x => x.TotalCoupons)
            .GreaterThan(0).WithMessage("Total coupons must be greater than 0.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Category is required.");

        RuleFor(x => x.StartDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Start date cannot be in the past.");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate).WithMessage("End date must be after start date.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
    }
}
