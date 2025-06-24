namespace WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Services;
    using Models;
    using Dtos;

    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController(IDoctorService service) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll() => Ok(await service.GetAllAsync());

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var entity = await service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(DoctorCreateDto entity)
        {
            var doctor = await service.CreateAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = doctor.Id },entity);
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update(long id, DoctorUpdateDto entity)
        {
            await service.UpdateAsync(id, entity);
            return NoContent();
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await service.DeleteAsync(id);
            return NoContent();
        }
    }
}