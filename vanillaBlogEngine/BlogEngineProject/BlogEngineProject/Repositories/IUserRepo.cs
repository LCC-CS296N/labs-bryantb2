using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngineProject.Models;

namespace BlogEngineProject.Repositories
{
    public interface IUserRepo
    {
        List<AppUser> GetUsers();
        AppUser GetUserById(int userId);
        AppUser GetUserByUsername(string username);
        bool GetUsernameEligibility(string username);
        bool CheckUserCredentials(string username, string password);
        List<Thread> SearchForUsersAndThreads(String searchString);
        void AddUsertoRepo(AppUser user);
        AppUser RemoveUserfromRepo(int userID);
    }
}
