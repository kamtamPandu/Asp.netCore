using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_userRepository.GetAll());
        }

        [HttpPost]
        public ActionResult<User> Post(User user)
        {
            _userRepository.Add(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }
    }
}

