using AgriTourism.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agritourism.AggregateRoot.Validations
{
    public static class ModelValidator
    {
        public static bool ValidateReview(ReviewDTO reviewDTO)
        {
            ReviewValidator validationRules = new ReviewValidator();
            var result=validationRules.Validate(reviewDTO);
            return result.IsValid;
        }
        public static bool ValidateUser(UserDTO userDTO)
        {
            UserValidator validationRules = new UserValidator();
            var result=validationRules.Validate(userDTO);
            return result.IsValid;
        }
    }
}
