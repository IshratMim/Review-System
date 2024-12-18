using Agritourism.AggregateRoot.Models;
using AgriTourism.DTO;
using AgriTourism.Handler.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriTourism.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // Get all reviews or filtered by username
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviews(string? username)
        {
            IEnumerable<ReviewDTO> reviews;
            if (username!=null)
            {
                reviews = await _reviewService.SearchReviewsByUsernameAsync(username);
                if (!reviews.Any())
                {
                    return NotFound($"No reviews found for username: {username}");
                }
            }
            else
            {
                reviews = await _reviewService.GetAllReviewsAsync();
            }

            return Ok(reviews);
        }
        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO userDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            int result = await _reviewService.CreateUserAsync(userDTO);

            if (result > 0)
            {
                return Ok(new { message = "User added successfully" });
            }

            return BadRequest("Failed to add review");
        }


        // Add a new review
        [HttpPost]
        [Route("AddReview")]
        public async Task<IActionResult> AddReview([FromBody] ReviewDTO reviewDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            int result = await _reviewService.CreateReviewAsync(reviewDto);

            if (result > 0)
            {
                return Ok(new { message = "Review added successfully" });
            }

            return BadRequest("Failed to add review");
        }

        // Get a review by id for editing
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDTO>> GetReview(int id)
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            var reviewDto = reviews.FirstOrDefault(r => r.Id == id);

            if (reviewDto == null)
            {
                return NotFound($"No review found with id: {id}");
            }

            return Ok(reviewDto);
        }

        // Update a review
        [HttpPut]
        public async Task<IActionResult> UpdateReview([FromBody] ReviewDTO reviewDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            int result = await _reviewService.UpdateReviewAsync(reviewDto);

            if (result > 0)
            {
                return Ok(new { message = "Review updated successfully" });
            }

            return NotFound("Review not found or failed to update");
        }

        // Delete a review
        [HttpDelete]
        public async Task<IActionResult> DeleteReview([FromBody] ReviewDTO reviewDto)
        {
            int result = await _reviewService.DeleteReviewAsync(reviewDto);

            if (result > 0)
            {
                return Ok(new { message = "Review deleted successfully" });
            }

            return NotFound("Review not found or failed to delete");
        }
    }
}
