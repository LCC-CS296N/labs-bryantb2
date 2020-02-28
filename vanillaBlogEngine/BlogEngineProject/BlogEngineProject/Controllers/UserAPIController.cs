using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogEngineProject.Models;
using BlogEngineProject.Repositories;

namespace BlogEngineProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAPIController : ControllerBase
    {
        // DEPENDENCY INJECTION
        IUserRepo userRepo;
        IThreadRepo threadRepo;
        public UserAPIController(IUserRepo u, IThreadRepo t)
        {
            userRepo = u;
            threadRepo = t;
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] CreateUserViewModel userViewModel)
        {
            if(userViewModel != null)
            {
                StandardUser user = new StandardUser()
                {
                    Name = userViewModel.Name,
                    Password = userViewModel.Password,
                    ConfirmPassword = userViewModel.Password,
                    DateJoined = DateTime.Now,
                };
                userRepo.AddUsertoRepo(user);
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = userRepo.GetUsers();
            if (users.Count != 0 && users != null)
                return Ok(users);
            else
                return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetUserByID(int id)
        {
            var user = userRepo.GetUserById(id);
            if (user != null)
                return Ok(user);
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserById([FromRoute] int id)
        {
            var deletedUser = userRepo.RemoveUserfromRepo(id);
            if (deletedUser != null)
                return Ok();
            else
                return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserById([FromBody] CreateUserViewModel userViewModel, int id)
        {
            var deletedUser = userRepo.RemoveUserfromRepo(id);
            if(deletedUser != null)
            {
                var dateJoined = deletedUser.DateJoined;
                StandardUser user = new StandardUser()
                {
                    Name = userViewModel.Name,
                    Password = userViewModel.Password,
                    ConfirmPassword = userViewModel.Password,
                    DateJoined = dateJoined,
                };
                userRepo.AddUsertoRepo(user);
                if (user != null)
                    return Ok(user);
                else
                    return NotFound();
            }
            else
            {
                return Ok(); // means user is gone
            }
        }

    }
}