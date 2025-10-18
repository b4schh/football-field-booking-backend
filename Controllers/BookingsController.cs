using FootballField.API.Dtos;
using FootballField.API.Dtos.Booking;
using FootballField.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FootballField.API.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// Get all bookings with pagination
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var (bookings, totalCount) = await _bookingService.GetPagedBookingsAsync(pageIndex, pageSize);
            var response = new ApiPagedResponse<BookingDto>(bookings, pageIndex, pageSize, totalCount, "Lấy danh sách đặt sân thành công");
            return Ok(response);
        }

        /// Get booking by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
                return Ok(ApiResponse<string>.Fail("Không tìm thấy đặt sân", 404));

            return Ok(ApiResponse<BookingDto>.Ok(booking, "Lấy thông tin đặt sân thành công"));
        }

        /// Get bookings by customer ID
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(int customerId)
        {
            var bookings = await _bookingService.GetBookingsByCustomerIdAsync(customerId);
            return Ok(ApiResponse<IEnumerable<BookingDto>>.Ok(bookings, "Lấy danh sách đặt sân thành công"));
        }

        /// Get bookings by owner ID
        [HttpGet("owner/{ownerId}")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> GetByOwnerId(int ownerId)
        {
            var bookings = await _bookingService.GetBookingsByOwnerIdAsync(ownerId);
            return Ok(ApiResponse<IEnumerable<BookingDto>>.Ok(bookings, "Lấy danh sách đặt sân thành công"));
        }

        /// Check if time slot is available
        [HttpGet("check-availability")]
        public async Task<IActionResult> CheckAvailability([FromQuery] int fieldId, [FromQuery] DateTime bookingDate, [FromQuery] int timeSlotId)
        {
            var isAvailable = await _bookingService.IsTimeSlotAvailableAsync(fieldId, bookingDate, timeSlotId);
            return Ok(ApiResponse<bool>.Ok(isAvailable, isAvailable ? "Khung giờ còn trống" : "Khung giờ đã được đặt"));
        }

        /// Create new booking
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingDto createBookingDto)
        {
            // Check if time slot is available
            var isAvailable = await _bookingService.IsTimeSlotAvailableAsync(
                createBookingDto.FieldId, 
                createBookingDto.BookingDate, 
                createBookingDto.TimeSlotId);
            
            if (!isAvailable)
                return Ok(ApiResponse<string>.Fail("Khung giờ đã được đặt", 400));

            var created = await _bookingService.CreateBookingAsync(createBookingDto);
            return Ok(ApiResponse<BookingDto>.Ok(created, "Đặt sân thành công", 201));
        }

        /// Update booking
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookingDto updateBookingDto)
        {
            var existing = await _bookingService.GetBookingByIdAsync(id);
            if (existing == null)
                return Ok(ApiResponse<string>.Fail("Không tìm thấy đặt sân", 404));

            await _bookingService.UpdateBookingAsync(id, updateBookingDto);
            return Ok(ApiResponse<string>.Ok(null, "Cập nhật đặt sân thành công"));
        }

        /// Cancel booking
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            var existing = await _bookingService.GetBookingByIdAsync(id);
            if (existing == null)
                return Ok(ApiResponse<string>.Fail("Không tìm thấy đặt sân", 404));

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return Ok(ApiResponse<string>.Fail("Unauthorized", 401));

            await _bookingService.CancelBookingAsync(id, userId);
            return Ok(ApiResponse<string>.Ok(null, "Hủy đặt sân thành công"));
        }
    }
}
