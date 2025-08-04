using System.Collections.Generic;

namespace WebApplication1
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        public User GetBy(string email);
        void Add(User user);
    }
}