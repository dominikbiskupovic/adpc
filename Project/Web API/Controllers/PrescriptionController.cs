namespace WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Services;
    using Models;
    using Dtos;
    
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController(IPrescriptionService service) : ControllerBase
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
        public async Task<IActionResult> Create(PrescriptionCreateDto entity)
        {
            var prescription = await service.CreateAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = prescription.Id }, entity);
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update(long id, PrescriptionUpdateDto entity)
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