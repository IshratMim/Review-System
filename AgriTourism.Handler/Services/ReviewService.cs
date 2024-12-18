using Agritourism.AggregateRoot.Models;
using AgriTourism.DTO;
using AgriTourism.Repository;

namespace AgriTourism.Handler.Services
{

    public class ReviewService : IReviewService
    {
        private readonly UserRepository userRepository;
        private readonly ReviewRepository reviewRepository;
        private readonly IGenericRepository<Review> _reviewRepository;
        private readonly IGenericRepository<User> _userRepository;

        public ReviewService(IGenericRepository<Review> reviewRepo, IGenericRepository<User> userRepo, ReviewRepository reviewRepository, UserRepository userRepository)
        {
            
            _reviewRepository = reviewRepo;
            _userRepository = userRepo;
            this.reviewRepository = reviewRepository;
            this.userRepository = userRepository;
        }
        //SHOWING THE LIST IN INDEX
        public async Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync()
        {
            var reviews = await _reviewRepository
                .GetAllWithIncludesAsync();
            //return (IEnumerable<ReviewDTO>)reviews;

            // Include User with the Review
            var res = new List<ReviewDTO>();
            foreach (var item in reviews)
            {
                res.Add(Review.ToDTO(item));
            }
            return res;
        }

        public async Task<IEnumerable<ReviewDTO>> SearchReviewsByUsernameAsync(string username)
        {
            var reviews = await reviewRepository.GetAllWithUsernameAsync(username) ;
                          
            
            var res = new List<ReviewDTO>();
            foreach (var item in reviews)
            {
                res.Add(Review.ToDTO(item));
            }
            return res;
        }
        public async Task<int> CreateUserAsync(UserDTO userDTO)
        {
            int result = 0;
            if(User.Validate(userDTO))
            {
                var user= await userRepository.GetUserForCreate(userDTO.Username);

                //var user = (await _userRepository.GetAllAsync())
                //            .FirstOrDefault(u => u.Username == userDTO.Username);

                if (user == null)
                {
                   
                    user = User.FromDTO(userDTO);

                    result = await _userRepository.AddAsync(user);
                }
            }
            return result;
            
        }

            public async Task<int> CreateReviewAsync(ReviewDTO reviewDTO)
            {
                int result = 0;
                if (Review.Validate(reviewDTO)) {
                    

                    var review = Review.FromDTO(reviewDTO);

                    result += await _reviewRepository.AddAsync(review);
                }


                return result;
            }
            public async Task<int> UpdateReviewAsync(ReviewDTO reviewDTO)
            {

                int result = 0;

                //if (review == null) return 0;

                /*review.Rating = reviewDTO.Rating;
                review.Comment = reviewDTO.Comment;*/
                if (Review.Validate(reviewDTO))
                {
                    Review review2 = Review.FromDTO(reviewDTO);
                    result += await _reviewRepository.UpdateAsync(review2);

                }
                return result;


            }





            public async Task<int> DeleteReviewAsync(ReviewDTO reviewDTO)
            {
                int result = 0;
                if (Review.Validate(reviewDTO))
                {
                    var review = Review.FromDTO(reviewDTO);
                    result += await _reviewRepository.DeleteAsync(review);
                }

                return result;

            }
        


    }
}




