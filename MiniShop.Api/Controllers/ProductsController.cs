using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniShop.Api.Application.DTOs;
using MiniShop.Api.Infrastructure.Persistence;

namespace MiniShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly MiniShopDbContext _db;
        private readonly IValidator<CreateProductDto> _validator;

        public ProductsController(MiniShopDbContext db, IValidator<CreateProductDto> validator)
        {
            _db = db;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> Get(CancellationToken cancellationToken)
        {
            return await _db.Products.AsNoTracking().Select(p => new ProductDto(p.Id, p.Name, p.UnitPrice, p.IsActive)).ToListAsync(cancellationToken);

        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create(CreateProductDto dto, CancellationToken ct)
        {
            var result = await _validator.ValidateAsync(dto, ct);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return ValidationProblem(ModelState);
            }
            var entity = new Domain.Entities.Product
            {
                Name = dto.Name,
                UnitPrice = dto.UnitPrice
            };

            _db.Products.Add(entity);
            await _db.SaveChangesAsync(ct);
            var model = new ProductDto(entity.Id, entity.Name, entity.UnitPrice, entity.IsActive);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, model);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetById(int id, CancellationToken ct)
        {
            var p = await _db.Products.FindAsync(new object?[] { id }, ct);
            return p is null ? NotFound() : new ProductDto(p.Id, p.Name, p.UnitPrice, p.IsActive);
        }
    }
}
