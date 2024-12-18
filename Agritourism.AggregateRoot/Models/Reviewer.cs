using AgriTourism.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agritourism.AggregateRoot.Models
{
    public class Reviewer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public static Reviewer FromDTO(ReviewerDTO dto)
        {
            return new Reviewer //need to use this
            {
                Id = dto.Id,
                Name = dto.Name,  // Set the UserId from the User object
                Password = dto.Password,
                Email = dto.Email,
                Phone = dto.Phone
            };
        }
    }
}
