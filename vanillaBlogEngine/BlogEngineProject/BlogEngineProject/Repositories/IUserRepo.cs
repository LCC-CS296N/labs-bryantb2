using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngineProject.Models;

namespace BlogEngineProject.Repositories
{
    public interface IUserRepo
    {
        List<User> GetUsers();
        User GetUserById(int userId);
        User GetUserByUsername(string username);
        bool GetUsernameEligibility(string username);
        bool CheckUserCredentials(string username, string password);
        List<Thread> SearchForUsersAndThreads(String searchString);
        void AddUsertoRepo(User user);
        User RemoveUserfromRepo(int userID);
    }
}
