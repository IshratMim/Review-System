using Agritourism.AggregateRoot.Models;
using AgriTourism.DTO;
using AgriTourism.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AgriTourism.Handler.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration configuration;
        private readonly IGenericRepository<Reviewer> _reviewerRepository;

        public AuthService(ApplicationDbContext context, IConfiguration configuration, IGenericRepository<Reviewer> reviewerRepo)
        {
            _context = context;
            this.configuration = configuration;
            _reviewerRepository = reviewerRepo;
        }
        public async Task<int> AddReviewer(ReviewerDTO reviewerDTO)
        {
            var reviewer= Reviewer.FromDTO(reviewerDTO);
            return await _reviewerRepository.AddAsync(reviewer);
        }

        public async Task<string> Login(LoginDTO loginDTO)
        {
            if (loginDTO.Email != null && loginDTO.Password != null)
            {
                
                var reviewer = (await _reviewerRepository.GetAllAsync()).FirstOrDefault(r => r.Email == loginDTO.Email && r.Password == loginDTO.Password);
                // Ensure none of the critical reviewer properties are null
                
                if (reviewer != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                        new Claim("Id", reviewer.Id.ToString()),
                        new Claim("Name", reviewer.Name),
                        new Claim("Email", reviewer.Email),
                    };
                    var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                    var SignIn=new SigningCredentials(Key,SecurityAlgorithms.HmacSha256);
                    var Token= new JwtSecurityToken(
                        configuration["Jwt:Issuer"],
                        configuration["Jwt:Audience"],
                        claims,
                        expires:DateTime.UtcNow.AddHours(10),
                        signingCredentials:SignIn);
                    var jwtToken=new JwtSecurityTokenHandler().WriteToken(Token);
                    return jwtToken;


                }
                else
                {
                    throw new Exception("user is not valid");
                }

            }
            else
            {
                throw new Exception("credentials are not valid");
            }

        }
    }
}
