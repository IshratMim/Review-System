using AgriTourism.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agritourism.AggregateRoot.Validations
{
    public class ReviewValidator: AbstractValidator<ReviewDTO>
    {
        public ReviewValidator() 
        {
            RuleFor(p => p.Rating <= 5).NotEmpty().WithMessage("Rating must be within number 5");
        }

    }
}
