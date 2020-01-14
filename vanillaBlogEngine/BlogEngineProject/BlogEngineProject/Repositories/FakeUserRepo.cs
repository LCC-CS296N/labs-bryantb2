using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngineProject.Models;

namespace BlogEngineProject.Repositories
{
    public class FakeUserRepo : IUserRepo
    {
        // CLASS FIELDS
        private static List<AppUser> userList = new List<AppUser>();
        
        // METHODS
        public  List<AppUser> GetUsers() => userList;
        public  AppUser GetUserById(int userId) => FindUserById(userId);
        public  AppUser GetUserByUsername(string username) => FindUserByUsername(username);
        public  bool GetUsernameEligibility(string username) => !(IsUsernameTaken(username));
        public  bool CheckUserCredentials(string username, string password) => AreUserCredentialsValid(username, password);

        public  List<Thread> SearchForUsersAndThreads(string searchString)
        {
            // ASSUMPTION: search string could be a username OR a threadname
            // therefore, the search will be conducted here since the User domain model has a Thread

            // search user list
            // add the user's thread if the username matches the searchString
            List<Thread> threadSearchResult = new List<Thread>();
            foreach(AppUser u in userList)
            {
                if (u.Name == searchString)
                    threadSearchResult.Add(u.OwnedThread);
            }

            // then

            // search thread list
            // add the thread to search results if the thread matches the searchString
            List<Thread> threads = new FakeThreadRepo().GetThreads();
            foreach (Thread t in threads)
            {
                if (t.Name == searchString)
                    threadSearchResult.Add(t);
            }

            return threadSearchResult;
        }

        public  void AddUsertoRepo(AppUser user)
        {
            if (IsUsernameTaken(user.Name) == false)
            {
                userList.Add(user);
            }
            else
            {
                throw new ArgumentException("Please make sure that the username is unique!");
            }
        }

        public  AppUser RemoveUserfromRepo(int userID)
        {
            // find user
            // then remove it
            AppUser removedUser = null;
            foreach (AppUser u in userList)
            {
                if (u.UserID == userID)
                {
                    removedUser = u;
                    userList.Remove(u);
                    return removedUser;
                }
            }
            return removedUser;
        }

        private  bool AreUserCredentialsValid(string username, string password)
        {
            // run a foreach loop on the user list
            // return true if username and password match an existing user
            foreach (AppUser u in userList)
            {
                if (u.Name == username && u.Password == password)
                    return true;
            }
            return false;
        }

        private  bool IsUsernameTaken(String username)
        {
            // looks through the user list for an identical username string
            // if the username is taken, return true
            foreach(AppUser u in userList)
            {
                if (u.Name == username)
                    return true;
            }
            return false;
        }

        private  AppUser FindUserById(int userId)
        {
            // run foreach loop on userlist
            // return true if current user's ID matches the parameter
            foreach (AppUser u in userList)
            {
                if (u.UserID == userId)
                    return u;
            }
            return null;
        }

        private  AppUser FindUserByUsername(string username)
        {
            // determine if username exists
            // run foreach loop on userlist
            // return true if current user's ID matches the parameter
            bool doesUsernameExist = IsUsernameTaken(username);
            if (doesUsernameExist == true)
            {
                foreach(AppUser u in userList)
                {
                    if (u.Name == username)
                        return u;
                }
            }
            return null;
        }
    }
}
