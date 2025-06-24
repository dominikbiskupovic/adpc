namespace WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Services;
    using Dtos;
    using Data;

    [ApiController]
    [Route("api/[controller]")]
    public class CheckupImageController(ICheckupImageService service, IStorageService storageService) : ControllerBase
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
        public async Task<IActionResult> Create(CheckupImageCreateDto entity)
        {
            var checkupImage = await service.CreateAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = checkupImage.Id }, entity);
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update(long id, CheckupImageUpdateDto entity)
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
        
        [HttpPost("upload/{checkupId}")]
        public async Task<IActionResult> Upload(long checkupId, IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return BadRequest();

            await storageService.EnsureBucketExistsAsync();

            using var stream = file.OpenReadStream();
            var objectName = $"{checkupId}/{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var url = await storageService.UploadFileAsync(stream, objectName, file.ContentType);

            var dto = new CheckupImageCreateDto
            {
                CheckupId = checkupId,
                FileUrl = url
            };

            var created = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
    }
}