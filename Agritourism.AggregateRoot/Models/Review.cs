using Agritourism.AggregateRoot.Validations;
using AgriTourism.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agritourism.AggregateRoot.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        
        public int UserId { get; set; } //foreign key 
        public int Rating { get; set; }
        public User User { get; set; }
        //public User Username { get; set; }
        public required string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        
        // Mapping to DTO
        public static ReviewDTO ToDTO(Review review )
        {
            return new ReviewDTO
            {
                
                Id = review.Id,
                Comment = review.Comment,
                CreatedAt = DateTime.Now,
                UserId = review.UserId,
                Rating = review.Rating,
                Username = review.User.Username


            };
        }
       
        // Mapping from DTO
        public static Review FromDTO(ReviewDTO dto)
        {
            return new Review //need to use this
            {
                Id = dto.Id,
                UserId = dto.UserId,  // Set the UserId from the User object
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.Now
            };
        }

        public static bool Validate(ReviewDTO dto)
        {
            return ModelValidator.ValidateReview(dto);
        }
    }
}
