using Microsoft.AspNetCore.Mvc;
using WebApplication1.BL;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("email/{email}/password/{password}")]
        public User GetUserByEmailAndPassword(string email,string password)
        {
            User user = new User();
            return user.GetUserByEmailAndPassword(email,password);

        }

        // GET api/<UsersController>/5
        [HttpGet("allUsers")]
        public IEnumerable<User> GetAllUsers()
        {
            User users = new User();
            return users.GetAllUsers();
        }

        [HttpGet("getById")]
        public User GetUserById([FromQuery] int id)
        {
            User user = new User();
            return user.GetUserById(id);
        }
        // POST api/<UsersController>
        [HttpPost]
        public User Post([FromBody] User user)
        {
            int numEffected=user.Insert();
            if(numEffected==-1)
                return user;
            return null;
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public int UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return -1;
            }
            int numEffected = user.UpdateUser(id,user);
            return numEffected;
        }
        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            User user = new User();
            return user.Delete(id);
        }
    }
}
