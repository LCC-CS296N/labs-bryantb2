using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngineProject.Models;

namespace BlogEngineProject.Repositories
{
    public interface IUserRepo
    {
        List<StandardUser> GetUsers();
        StandardUser GetUserById(int userId);
        StandardUser GetUserByUsername(string username);
        bool GetUsernameEligibility(string username);
        bool CheckUserCredentials(string username, string password);
        List<Thread> SearchForUsersAndThreads(String searchString);
        void AddUsertoRepo(StandardUser user);
        StandardUser RemoveUserfromRepo(int userID);
    }
}
