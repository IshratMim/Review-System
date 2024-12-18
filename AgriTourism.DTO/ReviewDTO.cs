using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AgriTourism.DTO.UserDTO;

namespace AgriTourism.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } // From User
        public string Email { get; set; }
        public int Rating { get; set; }
        public required string Comment { get; set; }
        public DateTime? CreatedAt { get; set; } 
    }
}
