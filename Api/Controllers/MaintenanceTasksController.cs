using Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.MaintenanceTasks;

namespace Equipment.Api.Controllers
{
    [ApiController]
    [Route("api/maintenancetasks")]
    public class MaintenanceTasksController : ControllerBase
    {
        private readonly IMaintenanceTaskService _service;

        public MaintenanceTasksController(IMaintenanceTaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetMaintenanceTaskDto>>> GetAll()
        {
            var tasks = await _service.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetMaintenanceTaskDto>> GetById(int id)
        {
            var task = await _service.GetByIdAsync(id);
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<GetMaintenanceTaskDto>> Create([FromBody] UpSertMaintenanceTaskDto dto)
        {
            if (dto == null)
                return BadRequest("MaintenanceTask data is required.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetMaintenanceTaskDto>> Update(int id, [FromBody] UpSertMaintenanceTaskDto dto)
        {
            if (dto == null)
                return BadRequest("MaintenanceTask data is required.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var updated = await _service.UpdateAsync(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
} 