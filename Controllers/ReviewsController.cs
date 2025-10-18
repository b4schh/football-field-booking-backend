using FootballField.API.Dtos;
using FootballField.API.Dtos.Review;
using FootballField.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballField.API.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>
        /// Get all reviews
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(ApiResponse<IEnumerable<ReviewDto>>.Ok(reviews, "Lấy danh sách đánh giá thành công"));
        }

        /// <summary>
        /// Get review by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
                return Ok(ApiResponse<string>.Fail("Không tìm thấy đánh giá", 404));

            return Ok(ApiResponse<ReviewDto>.Ok(review, "Lấy thông tin đánh giá thành công"));
        }

        /// <summary>
        /// Get reviews by field ID
        /// </summary>
        [HttpGet("field/{fieldId}")]
        public async Task<IActionResult> GetByFieldId(int fieldId)
        {
            var reviews = await _reviewService.GetReviewsByFieldIdAsync(fieldId);
            return Ok(ApiResponse<IEnumerable<ReviewDto>>.Ok(reviews, "Lấy danh sách đánh giá thành công"));
        }

        /// <summary>
        /// Get average rating by field ID
        /// </summary>
        [HttpGet("field/{fieldId}/average-rating")]
        public async Task<IActionResult> GetAverageRating(int fieldId)
        {
            var avgRating = await _reviewService.GetAverageRatingByFieldIdAsync(fieldId);
            return Ok(ApiResponse<double>.Ok(avgRating, "Lấy điểm trung bình thành công"));
        }

        /// <summary>
        /// Create new review
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateReviewDto createReviewDto)
        {
            var created = await _reviewService.CreateReviewAsync(createReviewDto);
            return Ok(ApiResponse<ReviewDto>.Ok(created, "Tạo đánh giá thành công", 201));
        }

        /// <summary>
        /// Update review
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateReviewDto updateReviewDto)
        {
            var existing = await _reviewService.GetReviewByIdAsync(id);
            if (existing == null)
                return Ok(ApiResponse<string>.Fail("Không tìm thấy đánh giá", 404));

            await _reviewService.UpdateReviewAsync(id, updateReviewDto);
            return Ok(ApiResponse<string>.Ok(null, "Cập nhật đánh giá thành công"));
        }

        /// <summary>
        /// Soft delete review
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _reviewService.GetReviewByIdAsync(id);
            if (existing == null)
                return Ok(ApiResponse<string>.Fail("Không tìm thấy đánh giá", 404));

            await _reviewService.SoftDeleteReviewAsync(id);
            return Ok(ApiResponse<string>.Ok(null, "Xóa đánh giá thành công"));
        }
    }
}
