using FluentValidation;
using MiniShop.Api.Application.DTOs;

namespace MiniShop.Api.Application.Validation
{
    public class CreateOrderValidator: AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.Items).NotEmpty();
            RuleForEach(x=>x.Items).ChildRules(item=>
            {
                item.RuleFor(i => i.ProductId).GreaterThan(0);
                item.RuleFor(i => i.Quantity).GreaterThan(0);
            });
        }
    }
}
