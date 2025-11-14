using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Api.Application.DTOs;
using MiniShop.Api.Application.Services.Interfaces;

namespace MiniShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _svc;
        private readonly IValidator<CreateOrderDto> _validator;


        public OrdersController(IOrderService svc, IValidator<CreateOrderDto> validator)
        { _svc = svc; _validator = validator; }


        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> Create(CreateOrderDto dto, CancellationToken ct)
        {
            var vr = await _validator.ValidateAsync(dto, ct);

            if (!vr.IsValid)
            {
                foreach (var error in vr.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return ValidationProblem(ModelState); // ⬅ اینجا هم همین
            }

            var result = await _svc.CreateAsync(dto, ct);
            return Created($"/api/orders/{result.Id}", result);
        }


        [HttpGet("health")]
        public ActionResult Health() => Ok(new { ok = true, time = DateTime.UtcNow });
    }
}
