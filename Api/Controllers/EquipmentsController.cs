using Services;
using Microsoft.AspNetCore.Mvc;

namespace Equipment.Api.Controllers
{
    [ApiController]
    [Route("api/equipment")]
    public class EquipmentsController : ControllerBase
    {
        private readonly EquipmentService _service;

        public EquipmentsController(EquipmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetEquipmentDto>>> GetAll()
        {
            var equipments = await _service.GetAllAsync();
            return Ok(equipments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetEquipmentDto>> GetById(int id)
        {
            var equipment = await _service.GetByIdAsync(id);
            return Ok(equipment);
        }

        [HttpPost]
        public async Task<ActionResult<GetEquipmentDto>> Create([FromBody] UpSertEquipmentDto equipment)
        {
            if (equipment == null)
                return BadRequest("Equipment data is required.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var created = await _service.CreateAsync(equipment);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetEquipmentDto>> Update(int id, [FromBody] UpSertEquipmentDto equipment)
        {
            if (equipment == null)
                return BadRequest("Equipment data is required.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var updated = await _service.UpdateAsync(id, equipment);
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