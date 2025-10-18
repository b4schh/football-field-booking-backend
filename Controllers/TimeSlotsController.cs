using FootballField.API.Dtos;
using FootballField.API.Dtos.TimeSlot;
using FootballField.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballField.API.Controllers
{
    [ApiController]
    [Route("api/timeslots")]
    public class TimeSlotsController : ControllerBase
    {
        private readonly ITimeSlotService _timeSlotService;

        public TimeSlotsController(ITimeSlotService timeSlotService)
        {
            _timeSlotService = timeSlotService;
        }

        /// <summary>
        /// Get all time slots
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var timeSlots = await _timeSlotService.GetAllTimeSlotsAsync();
            return Ok(ApiResponse<IEnumerable<TimeSlotDto>>.Ok(timeSlots, "Lấy danh sách khung giờ thành công"));
        }

        /// <summary>
        /// Get time slot by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var timeSlot = await _timeSlotService.GetTimeSlotByIdAsync(id);
            if (timeSlot == null)
                return Ok(ApiResponse<string>.Fail("Không tìm thấy khung giờ", 404));

            return Ok(ApiResponse<TimeSlotDto>.Ok(timeSlot, "Lấy thông tin khung giờ thành công"));
        }

        /// <summary>
        /// Get time slots by field ID
        /// </summary>
        [HttpGet("field/{fieldId}")]
        public async Task<IActionResult> GetByFieldId(int fieldId)
        {
            var timeSlots = await _timeSlotService.GetTimeSlotsByFieldIdAsync(fieldId);
            return Ok(ApiResponse<IEnumerable<TimeSlotDto>>.Ok(timeSlots, "Lấy danh sách khung giờ thành công"));
        }

        /// <summary>
        /// Create new time slot
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> Create([FromBody] CreateTimeSlotDto createTimeSlotDto)
        {
            var created = await _timeSlotService.CreateTimeSlotAsync(createTimeSlotDto);
            return Ok(ApiResponse<TimeSlotDto>.Ok(created, "Tạo khung giờ thành công", 201));
        }

        /// <summary>
        /// Update time slot
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTimeSlotDto updateTimeSlotDto)
        {
            var existing = await _timeSlotService.GetTimeSlotByIdAsync(id);
            if (existing == null)
                return Ok(ApiResponse<string>.Fail("Không tìm thấy khung giờ", 404));

            await _timeSlotService.UpdateTimeSlotAsync(id, updateTimeSlotDto);
            return Ok(ApiResponse<string>.Ok(null, "Cập nhật khung giờ thành công"));
        }

        /// <summary>
        /// Delete time slot
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _timeSlotService.GetTimeSlotByIdAsync(id);
            if (existing == null)
                return Ok(ApiResponse<string>.Fail("Không tìm thấy khung giờ", 404));

            await _timeSlotService.DeleteTimeSlotAsync(id);
            return Ok(ApiResponse<string>.Ok(null, "Xóa khung giờ thành công"));
        }
    }
}
