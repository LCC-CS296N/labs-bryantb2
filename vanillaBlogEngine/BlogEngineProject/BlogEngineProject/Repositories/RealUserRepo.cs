using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngineProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BlogEngineProject.Repositories
{
    public class RealUserRepo : IUserRepo
    {
        // CLASS FIELDS
        private AppDbContext context;

        // CONSTRUCTOR
        public RealUserRepo(AppDbContext appContext)
        {
            this.context = appContext;
        }

        // METHODS
        public List<StandardUser> GetUsers()
        {
            var userList = context.StandardUsers
                .Include(userObj => userObj.OwnedThread)
                    .ThenInclude(thread => thread.Posts)
                .ToList();
            return userList;
        }
        public StandardUser GetUserById(int userId) => FindUserById(userId);
        public StandardUser GetUserByUsername(string username) => FindUserByUsername(username);
        public bool GetUsernameEligibility(string username) => !(IsUsernameTaken(username));
        public bool CheckUserCredentials(string username, string password) => AreUserCredentialsValid(username, password);

        public List<Thread> SearchForUsersAndThreads(String searchString)
        {
            // ASSUMPTION: search string could be a username OR a threadname
            // therefore, the search will be conducted here since the User domain model has a Thread

            // search user list
            // add the user's thread if the username matches the searchString
            List<Thread> threadSearchResult = new List<Thread>();

            List<StandardUser> userList = GetUsers();
            foreach (StandardUser u in userList)
            {
                if (u.Name == searchString)
                    threadSearchResult.Add(u.OwnedThread);
            }

            // then

            // search thread list
            // add the thread to search results if the thread matches the searchString
            List<Thread> threads = new RealThreadRepo(context).GetThreads();
            foreach (Thread t in threads)
            {
                if (t.Name == searchString)
                    threadSearchResult.Add(t);
            }

            return threadSearchResult;
        }

        public void AddUsertoRepo(StandardUser user)
        {
            if (IsUsernameTaken(user.Name) == false)
            {
                context.StandardUsers.Add(user);
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Please make sure that the username is unique!");
            }
        }

        public StandardUser RemoveUserfromRepo(int userID)
        {
            // find user
            // then remove it
            StandardUser removedUser = null;
            List<StandardUser> userList = GetUsers();
            foreach (StandardUser u in userList)
            {
                if (u.StandardUserID == userID)
                {
                    removedUser = u;
                    context.StandardUsers.Remove(u);
                    context.SaveChanges();
                }
            }
            return removedUser;
        }

        private bool AreUserCredentialsValid(string username, string password)
        {
            // run a foreach loop on the user list
            // return true if username and password match an existing user
            List<StandardUser> userList = GetUsers();
            foreach (StandardUser u in userList)
            {
                if (u.Name == username && u.Password == password)
                    return true;
            }
            return false;
        }

        private bool IsUsernameTaken(String username)
        {
            // looks through the user list for an identical username string
            // if the username is taken, return true
            List<StandardUser> userList = GetUsers();
            foreach (StandardUser u in userList)
            {
                if (u.Name == username)
                    return true;
            }
            return false;
        }

        private StandardUser FindUserById(int userId)
        {
            // run foreach loop on userlist
            // return true if current user's ID matches the parameter
            List<StandardUser> userList = GetUsers();
            foreach (StandardUser u in userList)
            {
                if (u.StandardUserID == userId)
                    return u;
            }
            return null;
        }

        private StandardUser FindUserByUsername(string username)
        {
            // determine if username exists
            // run foreach loop on userlist
            // return true if current user's ID matches the parameter
            bool doesUsernameExist = IsUsernameTaken(username);
            if (doesUsernameExist == true)
            {
                List<StandardUser> userList = GetUsers();
                foreach (StandardUser u in userList)
                {
                    if (u.Name == username)
                        return u;
                }
            }
            return null;
        }
    }
}
