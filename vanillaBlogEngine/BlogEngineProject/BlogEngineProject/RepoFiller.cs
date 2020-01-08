using BlogEngineProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngineProject.Models;
using BlogEngineProject.Repositories;

namespace BlogEngineProject
{
    public static class RepoFiller
    {
        private static bool hasRepoBeenFilled = false;
        // THIS CLASS FILLS THE USER AND THREAD REPOS
        // NOTE: THESE ARE PARALLEL ARRAYS THAT STORE THREAD AND USER DATA
        private static readonly String[] usernames = new String[] { "Bob", "Jerry", "Tim", "Sally", "Greg" };
        private static readonly String[] threadNames = new String[] { "The Tech Blog", "My Life", "SW Fan Club", "Best Thread", "A Better Wired" };
        private static readonly String[] threadBios = new String[] { "For the techies out there, this one is for you", "oh, it sucks my friend. Do yourself a favor and get a job",
        "The central nerdy sci-fi lover's meeting place", "The title says it all, friends", "Mhmm, we report soooo much more than Wired.com. Take that Wired!" };
        private static readonly String[] threadCategories = new String[] { ThreadCategories.GetCategory(0), ThreadCategories.GetCategory(2), ThreadCategories.GetCategory(0), ThreadCategories.GetCategory(2), ThreadCategories.GetCategory(0) };
        private static readonly Post examplePost = new Post()
        {
            PostID = ObjectIDBuilder.GetPostID(),
            Title = "Test Post",
            Content = "Hello my friends, this is a test post create by the GOD himself, Blake",
            TimeStamp = DateTime.Now
        };

        public static bool HasRepoBeenFilled()
        {
            return hasRepoBeenFilled;
        }

        public static void FillRepos()
        {
            // BUILDS 5 USERS, EACH WITH THREADS

            // create user objects and fill repo of users
            // then create thread objects, since they are dependent on users existing
            BuildAndSendObjectsToRepos();
            hasRepoBeenFilled = true;
        }

        private static void BuildAndSendObjectsToRepos()
        {
            /*
             * Steps:
             *      loop through the array of usernames
             *      build user objects and a thread for each of them
             *      add the thread to the threadRepo 
             *      add the user object to the user repo
             */
            // loop to generate user objects
            for(int i =0; i< usernames.Length; i++)
            {
                // build user object
                User user = new User()
                {
                    UserID = ObjectIDBuilder.GetUserID(),
                    Username = usernames[i],
                    Password = "password",
                    DateJoined = DateTime.Now
                };

                // call thread builder
                // pass creator username to thread
                // set owned thread property
                Thread thread = BuildThread(i);
                thread.CreatorName = user.Username;
                user.OwnedThread = thread;

                // set objects to object repos
                new FakeThreadRepo().AddThreadtoRepo(thread);
                new FakeUserRepo().AddUsertoRepo(user);
            }
        }

        private static Thread BuildThread(int currentArrayIndex)
        {
            // build thread
            Thread thread = new Thread()
            {
                ThreadID = ObjectIDBuilder.GetThreadID(),
                Name = threadNames[currentArrayIndex],
                Bio = threadBios[currentArrayIndex],
                Category = threadCategories[currentArrayIndex]
            };
            thread.AddPostToThread(examplePost);

            return thread;
        }
    }
}
