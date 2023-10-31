using MarketingSystem.Backend.Models;
using MarketingSystem.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketingSystem.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly MarketingSystemDbContext _context;
        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger,
                                 MarketingSystemDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<ActionResult<List<ContactDto>>> GetAll()
        {
            try
            {
                var data = await _context.Contacts.Where(x => x.Active)
                                                   .ToListAsync();
                return data.Select(x=>new ContactDto
                {
                    Id = x.Id,
                    Active = x.Active,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(GetById));
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(nameof(GetById))]
        public async Task<ActionResult<ContactDto>> GetById(int id)
        {
            try
            {
                var data = await _context.Contacts.SingleOrDefaultAsync(x => x.Id == id && x.Active);

                return Ok(new ContactDto 
                {
                    Id = data.Id,
                    Active = data.Active,
                    Email = data.Email,
                    FirstName = data.FirstName,
                    LastName= data.FirstName
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(GetById));
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post(ContactDto newValueDto)
        {
            try
            {
                var newValueModel = new Contact 
                {
                    Id = newValueDto.Id,
                    Active = newValueDto.Active,
                    Email = newValueDto.Email,
                    FirstName = newValueDto.FirstName,
                    LastName = newValueDto.LastName
                };

                await _context.Contacts.AddAsync(newValueModel);
                var result = await _context.SaveChangesAsync();
                return Ok(result > 0);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);                
            }
        }

        [HttpPut()]
        public async Task<ActionResult<bool>> Put(ContactDto newValueDto)
        {
            try
            {
                var dbValue = await _context.Contacts.SingleOrDefaultAsync(x => x.Id == newValueDto.Id);

                dbValue.Id = newValueDto.Id;
                dbValue.FirstName = newValueDto.FirstName;
                dbValue.LastName = newValueDto.LastName;
                dbValue.Email = newValueDto.Email;
                dbValue.Active = newValueDto.Active;

                _context.Contacts.Update(dbValue);
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
                var dbValue = await _context.Contacts.SingleOrDefaultAsync(x => x.Id == id);

                dbValue.Active = false;

                _context.Contacts.Update(dbValue);
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
