using Agritourism.AggregateRoot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriTourism.Repository
{
    public interface IUserRepository 
    {
        Task<User> GetUserForCreate(string username);
    }
}
