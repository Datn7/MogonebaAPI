using Microsoft.AspNetCore.Mvc;
using MogonebaAPI.Data;
using MogonebaAPI.Models;

namespace MogonebaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MemoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMemory([FromBody] CreateMemoryDto dto)
        {
            var memory = new Memory
            {
                Title = dto.Title,
                Description = dto.Description,
                Date = dto.Date,
                Location = dto.Location,
                ImageUrl = dto.ImageUrl
            };

            _context.Memories.Add(memory);
            await _context.SaveChangesAsync();
            return Ok(memory);
        }
    }

}
