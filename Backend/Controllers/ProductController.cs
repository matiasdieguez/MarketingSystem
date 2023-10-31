using MarketingSystem.Backend.Models;
using MarketingSystem.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketingSystem.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly MarketingSystemDbContext _context;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger,
                                 MarketingSystemDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            try
            {
                var data = await _context.Products.Where(x => x.Active)
                                                   .ToListAsync();

                return data.Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsAlcoholic = x.IsAlcoholic,
                    Price = x.Price,
                    Active = x.Active
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(GetById));
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(nameof(GetById))]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            try
            {
                var data = await _context.Products.SingleOrDefaultAsync(x => x.Id == id && x.Active);

                return Ok(new ProductDto
                {
                    Id = data.Id,
                    Name = data.Name,
                    Price = data.Price,
                    Active = data.Active,
                    IsAlcoholic = data.IsAlcoholic
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(GetById));
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<ActionResult<bool>> Post(ProductDto newValueDto)
        {
            try
            {
                var newValueModel = new Product
                {
                    Id = newValueDto.Id,
                    Name = newValueDto.Name,
                    Active = newValueDto.Active,
                    IsAlcoholic = newValueDto.IsAlcoholic,
                    Price = newValueDto.Price
                };

                await _context.Products.AddAsync(newValueModel);
                await _context.SaveChangesAsync();
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut()]
        public async Task<ActionResult<bool>> Put(ProductDto newValueDto)
        {
            try
            {
                var dbValue = await _context.Products.SingleOrDefaultAsync(x => x.Id == newValueDto.Id);

                dbValue.Id = newValueDto.Id;
                dbValue.Name = newValueDto.Name;
                dbValue.Price = newValueDto.Price;
                dbValue.IsAlcoholic = newValueDto.IsAlcoholic;
                dbValue.Active = newValueDto.Active;

                _context.Products.Update(dbValue);
                await _context.SaveChangesAsync();

                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(Put));
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete()]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var dbValue = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);

                dbValue.Active = false;

                _context.Products.Update(dbValue);
                await _context.SaveChangesAsync();

                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(Delete));
                return BadRequest(ex.Message);
            }
        }
    }
}
