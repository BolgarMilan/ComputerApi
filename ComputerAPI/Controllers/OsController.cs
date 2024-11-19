using ComputerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace ComputerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OsController : ControllerBase
    {
        private readonly ComputerContext computerContext;

        public OsController(ComputerContext computerContext)
        {
            this.computerContext = computerContext;
        }

        [HttpPost]
        public async Task<ActionResult<OSystem>> Post(CreateOsDto createOsDto)
        {
            var os = new OSystem()
            {
                Id = new Guid(),
                Name = createOsDto.Name,
                CreatedTime = DateTime.Now
            };

            if (os != null)
            {
                await computerContext.Os.AddAsync(os);
                await computerContext.SaveChangesAsync();
                return StatusCode(201, os);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<OSystem>> Get()
        {
            return Ok(await computerContext.Os.ToListAsync());
        }
    }
}
