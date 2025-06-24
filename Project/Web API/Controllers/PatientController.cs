namespace WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Text;
    using Services;
    using Models;
    using Dtos;

    [ApiController]
    [Route("api/[controller]")]
    public class PatientController(IPatientService service) : ControllerBase
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
        public async Task<IActionResult> Create(PatientCreateDto entity)
        {
            var patient = await service.CreateAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = patient.Id }, entity);
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update(long id, PatientUpdateDto entity)
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

        [HttpGet("[action]")]
        public async Task<IActionResult> ExportToCsv()
        {
            var list = await service.GetAllAsync();
            var sb = new StringBuilder();
            sb.AppendLine("Id, PersonalId, Name, Surname, DateOfBirth, Sex");
            foreach (var p in list)
            {
                sb.AppendLine($"{p.Id},{p.PersonalId},{p.Name},{p.Surname},{p.DateOfBirth:yyyy-MM-dd},{p.Sex}");
            }
            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", "patients.csv");
        }
    }
}