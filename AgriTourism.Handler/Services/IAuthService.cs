using AgriTourism.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriTourism.Handler.Services
{
    public interface IAuthService
    {
        
              Task<int> AddReviewer(ReviewerDTO reviewerDTO);
              Task<string> Login(LoginDTO loginDTO);
        
    }
}
