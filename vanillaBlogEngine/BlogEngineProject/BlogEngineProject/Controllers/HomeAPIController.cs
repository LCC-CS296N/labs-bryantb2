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
    public class HomeAPIController : ControllerBase
    {
        // DEPENDENCY INJECTION
        IUserRepo userRepo;
        IThreadRepo threadRepo;
        public HomeAPIController(IUserRepo u, IThreadRepo t)
        {
            userRepo = u;
            threadRepo = t;
        }

        [HttpGet("{category}")]
        [Consumes("application/json")]
        public IActionResult GetThreadsByCategory(int category)
        {
            // Get thread objects
            // check if search results have already been queued
            // Pass thread list into Home
            List<Thread> threadList;
            threadList = threadRepo.GetCategoryOfThreads(category);
            if (threadList.Count != 0 && threadList != null)
                return Ok(threadList);
            else
                return NotFound();
            //return View("Home", threadList);
        }

        [HttpGet("{searchString}")]
        [Consumes("application/json")]
        public IActionResult FindThreadsByKeyword(string searchString)
        {
            List<Thread> searchResults = null;
            // called the repo search function
            searchResults = userRepo.SearchForUsersAndThreads(searchString);
            if (searchResults != null && searchResults.Count != 0)
                return Ok(searchResults);
            else
                return NotFound();
            //ViewBag.SearchQuery = searchString;
            //return View(searchResults);
        }

        [HttpGet("{threadID}")]
        [Consumes("application/json")]
        public IActionResult FindThreadById(int threadID)
        {
            Thread searchResult;
            // search for the thread by name
            searchResult = threadRepo.GetThreadById(threadID);
            if (searchResult != null)
                return Ok(searchResult);
            else
                return NotFound();
            /*else
            {
                // returns to thread page if no parameter values are found
                return View("Index");
            }
            return View(searchResult);*/
        }


    }
}