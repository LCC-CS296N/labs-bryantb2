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
        //[Consumes("application/json")]
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

        /*[HttpPost]
        public String AddUser([FromBody] CreateUserViewModel userViewModel)
        {
            return "hello world";
        }*/

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = userRepo.GetUsers();
            if (users.Count != 0 && users != null)
                return Ok(users);
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        //[Consumes("application/json")]
        public IActionResult DeleteUserById([FromRoute] int id)
        {
            var deletedUser = userRepo.RemoveUserfromRepo(id);
            //Console.WriteLine("logging deleted user");
            //Console.WriteLine(deletedUser);
            if (deletedUser != null)
                return Ok();
            else
                return NotFound();
        }

        [HttpPut("{id}")]
        //[Consumes("application/json")]
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
                return BadRequest();
            }
        }

    }
}