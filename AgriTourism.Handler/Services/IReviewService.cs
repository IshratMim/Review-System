using AgriTourism.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriTourism.Handler.Services
{
    public interface IReviewService
    {
       public Task<int> CreateUserAsync(UserDTO userDTO);
      public  Task<int> CreateReviewAsync(ReviewDTO reviewDTO);
      public  Task<int> UpdateReviewAsync(ReviewDTO reviewDTO);
      public  Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync();
     public   Task<IEnumerable<ReviewDTO>> SearchReviewsByUsernameAsync(string username);
      public  Task<int> DeleteReviewAsync(ReviewDTO reviewDTO);
    }
}
