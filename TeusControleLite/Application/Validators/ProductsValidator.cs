using FluentValidation;
using TeusControleLite.Domain.Models;

namespace TeusControleLite.Application.Validators
{
    /// <summary>
    /// Validação para objeto do tipo usuário
    /// </summary>
    public class ProductsValidator : AbstractValidator<Products>
    {
        public ProductsValidator()
        {
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Please enter the description.")
                .NotNull().WithMessage("Please enter the description.");
        }
    }
}
