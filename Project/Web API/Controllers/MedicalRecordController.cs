namespace WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Services;
    using Models;
    using Dtos;
    
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordController(IMedicalRecordService service) : ControllerBase
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
        public async Task<IActionResult> Create(MedicalRecordCreateDto entity)
        {
            var medicalRecord = await service.CreateAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = medicalRecord.Id },entity);
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update(long id, MedicalRecordUpdateDto entity)
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