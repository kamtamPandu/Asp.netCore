using System.Collections.Generic;

namespace WebApplication1
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}

