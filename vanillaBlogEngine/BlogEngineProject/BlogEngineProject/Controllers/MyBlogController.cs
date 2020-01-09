using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogEngineProject.Repositories;
using BlogEngineProject.Models;

namespace BlogEngineProject.Controllers
{
    public class MyBlogController : Controller
    {
        // DEPENDENCY INJECTION
        IUserRepo userRepo;
        IThreadRepo threadRepo;
        public MyBlogController(IUserRepo u, IThreadRepo t)
        {
            userRepo = u;
            threadRepo = t;
        }

        // GENERAL SIGN IN METHODS
        //  ---------------------------------------------------------------------------------------------------->
        //  ---------------------------------------------------------------------------------------------------->
        public IActionResult Index()
        {
            if (TempData["SignInMessage"] != null)
            {
                // if message tempdata entry is not null, pass the message to the view
                ViewBag.ErrorMessage = TempData["SignInMessage"];
            }
            return View("MyBlogSignIn");
        }

        public IActionResult SignUp()
        {
            if (TempData["SignUpMessage"] != null)
            {
                // if message tempdata entry is not null, pass the message to the view
                ViewBag.ErrorMessage = TempData["SignUpMessage"];
            }
            return View("MyBlogSignUp");
        }

        [HttpPost]
        public RedirectToActionResult SignUpRedirect(string username, string password, string ConfirmPassword)
        {
            // validate username and password (not empty AND trim trailing white space)
            // search repo to see if username is already taken
            // check to make sure password and confirmed password fields match
            // build user object
            // add user to repo
            // redirect to myblogpanel
            // else show signup with an invalid username and password error
            var trimmedUsername = "";
            var trimmedPassword = "";
            var trimmedConfirmPassword = "";

            if (username != null && password != null && ConfirmPassword != null)
            {
                trimmedUsername = username.Trim();
                trimmedPassword = password.Trim();
                trimmedConfirmPassword = ConfirmPassword.Trim();
            }
            else
            {
                // create a tempdata entry that contains the message to be returned in the sign up view
                // return signup view because the password/username combo is empty
                TempData["SignUpMessage"] = "Password and/or username fields cannot be empty";
                return RedirectToAction("SignUp");
            }
            if(userRepo.GetUsernameEligibility(trimmedUsername)==false)
            {
                TempData["SignUpMessage"] = "That username is taken :(";
                return RedirectToAction("SignUp");
            }
            if(trimmedPassword != trimmedConfirmPassword)
            {
                TempData["SignUpMessage"] = "Password and/or username fields cannot be empty";
                return RedirectToAction("SignUp");
            }

            User newUser = new User()
            {
                Username = trimmedUsername,
                Password = trimmedConfirmPassword,
                ConfirmPassword = trimmedConfirmPassword,
                DateJoined = DateTime.Now
            };
            userRepo.AddUsertoRepo(newUser);

            TempData["validUsername"] = trimmedUsername;
            return RedirectToAction("MyBlogDashboard");
        }


        public RedirectToActionResult SignInRedirect(string username, string password)
        {
            // validate username and password (not empty AND trim trailing white space)
            // search repo for username
            // if username and password match
            // redirect to myblogpanel
            // else show signup with an invalid username and password error
            var trimmedUsername = "";
            var trimmedPassword = "";

            if (username != null && password != null)
            {
                trimmedUsername = username.Trim();
                trimmedPassword = password.Trim();
            }
            else
            {
                // create a tempdata entry that contains the message to be returned in the sign up view
                // return signup view because the password/username combo is empty
                TempData["SignInMessage"] = "Password and/or username fields cannot be empty";
                return RedirectToAction("Index");
            }

            bool areCredentialsValid = userRepo.CheckUserCredentials(trimmedUsername, trimmedPassword);
            if(areCredentialsValid != true)
            {
                // create a tempdata entry that contains the message to be returned in the sign up view
                // return signup view because the password/username combo is not valid
                TempData["SignInMessage"] = "Password and/or username fields are incorrect. If are not a user already, then sign up!";
                return RedirectToAction("Index");
            }

            TempData["validUsername"] = trimmedUsername;
            return RedirectToAction("MyBlogDashboard");
        }

        public IActionResult MyBlogDashboard()
        {
            // no need to valid tempdata, only time this method gets called is if a valid username is passed
            string username = TempData["validUsername"].ToString();
            User userObject = userRepo.GetUserByUsername(username);

            return View("MyBlogMainPanel", userObject);
        }

        // SIGN IN METHODS FOR COMMENTS
        //  ---------------------------------------------------------------------------------------------------->
        //  ---------------------------------------------------------------------------------------------------->
        /*public RedirectToActionResult ReplySignInRedirect(string username, string password)
        {

        }*/
    



        // THESE METHODS REQUIRE A USER ID FOR ACCESS
        //  ---------------------------------------------------------------------------------------------------->
        //  ---------------------------------------------------------------------------------------------------->
        public IActionResult ReloadBlogDashboard()
        {
            // takes in userId from temp data entry
            // retrieves user object and then passes it into the dashboard view
            int userId = int.Parse(TempData["userId"].ToString());
            User userObject = userRepo.GetUserById(userId);

            return View("MyBlogMainPanel", userObject);
        }

        public IActionResult GettingStarted(string userId="")
        {
            // SPECIAL CASE: this action method needs to accept both get parameters OR tempdata (both of which represent a userId)
            // this is done because a the dashboard view might call this action method, or the BuildThread action method will redirect to it internally
            // Check if userId parameter is empty
            // if empty use tempdata
            // Get user by id
            // Pass in userId to view]
            var userIdString = "";
            if(userId != "")
            {
                userIdString = userId;
            }
            else
            {
                userIdString = TempData["userId"].ToString();
            }

            int userIdAsInt = int.Parse(userIdString);
            User userObject = userRepo.GetUserById(userIdAsInt);

            if (TempData["ThreadCreationMessage"] != null)
            {
                // if message tempdata entry is not null, pass the message to the view
                ViewBag.ErrorMessage = TempData["ThreadCreationMessage"];
            }
            // passing ownedThread and UserID in via viewbag because I will use a tuple in the view
            ViewBag.OwnedThread = userObject.OwnedThread;
            ViewBag.UserID = userObject.UserID;
            return View();
        }

        [HttpPost]
        public RedirectToActionResult BuildThread(string Name, string Category, string Bio, string userId)
        {
            // validate input (not empty AND trim trailing white space)
            // search thread repo and determine if the threadname is in use
            // Build a thread object using the input parameters
            // Get reference to user by id
            // Set the user's owned thread property
            // Add thread to threadRepo
            // make a temp data entry containing the userId
            // redirect to ReloadBlogDashboard action method
            var trimmedThreadname = "";
            var trimmedBio = "";
            if (Name != null && Name != null && Bio != null)
            {
                trimmedThreadname = Name.Trim();
                trimmedBio = Bio.Trim();
            }
            else
            {
                // create a tempdata entry that contains the message to be returned in the getting started view
                // return getting started view because the inputted data is empty
                TempData["ThreadCreationMessage"] = "No fields can be left blank";
                TempData["userId"] = userId;
                return RedirectToAction("GettingStarted");
            }
            if(!(threadRepo.GetThreadnameEligibility(trimmedThreadname) == true))
            {
                TempData["ThreadCreationMessage"] = "Unforunately, that thread name is already taken :(";
                TempData["userId"] = userId;
                return RedirectToAction("GettingStarted");
            }

            int USERID = int.Parse(userId);
            User user = userRepo.GetUserById(USERID);

            Thread newThread = new Thread()
            {
                Name = Name,
                Bio = Bio,
                Category = Category,
                CreatorName = user.Username
            };
            user.OwnedThread = newThread;
            threadRepo.AddThreadtoRepo(newThread);

            TempData["userId"] = userId;
            return RedirectToAction("ReloadBlogDashboard");
        }

        public RedirectToActionResult AddPost(string postTitle, string postContent, string threadId, string userId)
        {
            // add post to thread
            // make a temp data entry containing the userId
            // redirect to ReloadBlogDashboard action method
            Post newPost = new Post()
            {
                Title = postTitle,
                Content = postContent,
                TimeStamp = DateTime.Now
            };
            
            int threadIdAsInt = int.Parse(threadId);
            threadRepo.AddThreadPost(threadIdAsInt, newPost);

            TempData["userId"] = userId;
            return RedirectToAction("ReloadBlogDashboard");
        }

        public RedirectToActionResult RemovePost(string postId, string threadId, string userId)
        {
            // remove the post from thread
            // make a temp data entry containing the userId
            // redirect to ReloadBlogDashboard action method
            int postIdAsInt = int.Parse(postId);
            int threadIdAsInt = int.Parse(threadId);

            threadRepo.RemoveThreadPost(threadIdAsInt, postIdAsInt);

            TempData["userId"] = userId;
            return RedirectToAction("ReloadBlogDashboard");
        }

        [HttpPost]
        public RedirectToActionResult EditPost(string editedTitle, string editedContent, string postId, string threadId, string userId)
        {
            // Set the post's editedTitle and editedContent properties
            // make a temp data entry containing the userId
            // redirect to ReloadBlogDashboard action method 
            int postIdAsInt = int.Parse(postId);
            int threadIdAsInt = int.Parse(threadId);

            threadRepo.EditThreadPost(threadIdAsInt, postIdAsInt, editedTitle, editedContent);

            TempData["userId"] = userId;
            return RedirectToAction("ReloadBlogDashboard");
        }

        [HttpPost]
        public RedirectToActionResult EditProfile(string editedThreadname, string editedThreadCategory, string editedBio, string threadId, string userId)
        {
            // Get thread by id
            // Update appropriate thread properties
            // make a temp data entry containing the userId
            // redirect to ReloadBlogDashboard action method 
            int threadIdAsInt = int.Parse(threadId);

            threadRepo.EditThreadProfile(editedThreadname, editedThreadCategory, editedBio, threadIdAsInt);
            
            TempData["userId"] = userId;
            return RedirectToAction("ReloadBlogDashboard");
        }


        public IActionResult PostEditor(string postId, string threadId, string userId)
        {
            // NOTE: object IDs are passed in via viewbag because there we don't want to build DB retrieval logic into the view
            // Get thread repo by id
            // Find post by id from thread
            // Pass postObject into view
            // Pass userId into view by Viewbag
            int POST_ID = int.Parse(postId);
            int THREAD_ID = int.Parse(threadId);
            Post postToEdit = threadRepo.GetThreadById(THREAD_ID).GetPostById(POST_ID);

            ViewBag.ThreadId = threadId;
            ViewBag.UserId = userId;
            return View(postToEdit);
        }

        public IActionResult PostDashboard(string userId)
        {
            // get user by id
            // pass user object into view
            int ID = int.Parse(userId);
            User userObject = userRepo.GetUserById(ID);

            return View(userObject);
        }

        public IActionResult EditProfile(string userId)
        {
            // NOTE: object IDs are passed in via viewbag because there we don't want to build DB retrieval logic into the view
            // Utilize userId to get owned thread
            // pass thread object to view
            int USERID = int.Parse(userId);
            Thread thread = userRepo.GetUserById(USERID).OwnedThread;

            ViewBag.UserId = userId;
            return View(thread);
        }
    }
}