using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogEngineProject.Models;
using BlogEngineProject.Repositories;

namespace BlogEngineProject.Controllers
{
    public class HomeController : Controller
    {
        // DEPENDENCY INJECTION
        IUserRepo userRepo;
        IThreadRepo threadRepo;
        public HomeController(IUserRepo u, IThreadRepo t)
        {
            userRepo = u;
            threadRepo = t;
        }

        public IActionResult Index(int category=-1)
        {
            // Get thread objects
            // check if search results have already been queued
            // Pass thread list into Home
            List<Thread> threadList;
            if (category != -1)
            {
                threadList = threadRepo.GetCategoryOfThreads(category);
            }
            else
            {
                threadList = threadRepo.GetThreads();
            }
            return View("Home",threadList);
        }

        public IActionResult ReplyToPost()
        {
            return View();
        }

        public IActionResult ThreadSearch()
        {
            return View();
        }

        public IActionResult ThreadSearchResults(string searchString = "")
        {
            List<Thread> searchResults = null;
            if (searchString != "")
            {
                // called the repo search function
                searchResults = userRepo.SearchForUsersAndThreads(searchString);
            }
            ViewBag.SearchQuery = searchString;
            return View(searchResults);
        }

        public IActionResult ViewBlog(int threadID = -1)
        {
            Thread searchResult;
            if(threadID != -1)
            {
                // search for the thread by name
                searchResult = threadRepo.GetThreadById(threadID);
            }
            else
            {
                // returns to thread page if no parameter values are found
                return View("Index");
            }
            return View(searchResult);
        }
    }
}
