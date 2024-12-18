using Agritourism.AggregateRoot.Validations;
using AgriTourism.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agritourism.AggregateRoot.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public ICollection<Review> Reviews { get; set; }

        // Mapping to DTO
        public UserDTO ToDTO()
        {
            return new UserDTO
            {
                Id = this.Id,
                Username = this.Username,
                Email = this.Email
            };
        }

        // Mapping from DTO
        public static User FromDTO(UserDTO dto)
        {
            return new User
            {
                Username = dto.Username,
                Email = dto.Email
            };
        }
        public static bool Validate(UserDTO dto)
        {
            return ModelValidator.ValidateUser(dto);
        }
    }
}
