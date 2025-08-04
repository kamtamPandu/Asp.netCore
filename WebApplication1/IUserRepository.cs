using System.Collections.Generic;

namespace WebApplication1
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        void Add(User user);
    }
}