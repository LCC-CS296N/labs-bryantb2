using System;
using Xunit;
using BlogEngineProject.Models;
using BlogEngineProject.Repositories;
using BlogEngineProject;
using System.Collections.Generic;
using BlogEngineProject.Models;
using BlogEngineProject.Repositories;

namespace UnitTests
{
    public class RepositoryTests
    {
        // THREAD REPO TESTS
        [Fact]
        public void AddThreads()
        {
            // arrange
            FakeThreadRepo threadRepo = new FakeThreadRepo();

            const string bio = "hello there friends";
            const string name = "testThread1";
            const string creatorName = "bob";
            const string category = "games and music";
            int id = ObjectIDBuilder.GetThreadID();
            Thread thread1 = new Thread()
            {
                ThreadID = id,
                Name = name,
                CreatorName = creatorName,
                Bio = bio,
                Category = category
            };

            // act
            threadRepo.AddThreadtoRepo(thread1); // add thread
            List<Thread> threads = threadRepo.GetThreads(); // get thread
            Thread foundThread = null; // found thread
            foreach(Thread t in threads)
            {
                if (t == thread1)
                    foundThread = t;
            }

            // Assert
            Assert.Equal(thread1.Name, foundThread.Name);
            Assert.Equal(thread1.Category, foundThread.Category);
            Assert.Equal(thread1.CreatorName, foundThread.CreatorName);
        }

        [Fact]
        public void RemoveThreads()
        {
            // arrange
            FakeThreadRepo threadRepo = new FakeThreadRepo();

            const string bio = "hello there friends";
            const string name = "testThread";
            const string creatorName = "bob";
            const string category = "games and music";
            int id = ObjectIDBuilder.GetThreadID();
            Thread thread1 = new Thread()
            {
                ThreadID = id,
                Name = name,
                CreatorName = creatorName,
                Bio = bio,
                Category = category
            };
            threadRepo.AddThreadtoRepo(thread1); // add thread

            // act 
            Thread removedThread = threadRepo.RemoveThreadfromRepo(id); // remove thread

            // assert
            Assert.Equal(thread1.Name, removedThread.Name);
            Assert.Equal(thread1.Category, removedThread.Category);
            Assert.Equal(thread1.CreatorName, removedThread.CreatorName);
        }

        [Fact]
        public void GetThreads()
        {
            // arrange
            FakeThreadRepo threadRepo = new FakeThreadRepo();

            const string bio = "hello there friends";
            const string name = "testThread2";
            const string creatorName = "bob";
            const string category = "games and music";
            int id = ObjectIDBuilder.GetThreadID();
            Thread thread1 = new Thread()
            {
                ThreadID = id,
                Name = name,
                CreatorName = creatorName,
                Bio = bio,
                Category = category
            };
            threadRepo.AddThreadtoRepo(thread1); // add thread

            // act 
            Thread foundThread = threadRepo.GetThreadById(id); // found thread with id
            Thread foundThread2 = threadRepo.GetThreadByName(name); // found thread with name

            // assert (test if the thread was found with id)
            Assert.Equal(thread1.Name, foundThread.Name);
            Assert.Equal(thread1.Category, foundThread.Category);
            Assert.Equal(thread1.CreatorName, foundThread.CreatorName);

            // assert (test if the thread was found with name)
            Assert.Equal(thread1.Name, foundThread2.Name);
            Assert.Equal(thread1.Category, foundThread2.Category);
            Assert.Equal(thread1.CreatorName, foundThread2.CreatorName);
        }

        // USER REPO TESTS
        [Fact]
        public void AddRemoveUser()
        {
            // arrange 
            FakeUserRepo userRepo = new FakeUserRepo();

            int id = ObjectIDBuilder.GetUserID();
            const string username = "robert";
            const string password = "asdf";
            const string gender = "male";
            DateTime dateJoinedD = DateTime.Now;
            Thread ownedThread = null;
            User newUser = new User()
            {
                UserID = id,
                Username = username,
                Password = password,
                Gender = gender,
                DateJoined = dateJoinedD,
                OwnedThread = ownedThread
            };


            // act
            userRepo.AddUsertoRepo(newUser);
            User removedUser = userRepo.RemoveUserfromRepo(id);

            // assert
            Assert.Equal(newUser.Username, removedUser.Username);
            Assert.Equal(newUser.DateJoined, removedUser.DateJoined);
            Assert.Equal(newUser.UserID, removedUser.UserID);
        }

        [Fact]
        public void SearchUsersAndThreads()
        {
            // arrange
            FakeThreadRepo threadRepo = new FakeThreadRepo();
            FakeUserRepo userRepo = new FakeUserRepo();

            int id = ObjectIDBuilder.GetUserID();
            const string username = "Sally";
            const string password = "1234";
            const string gender = "female";
            DateTime dateJoinedD = DateTime.Now;
            Thread ownedThread = null;
            User newUser = new User() // user1
            {
                UserID = id,
                Username = username,
                Password = password,
                Gender = gender,
                DateJoined = dateJoinedD,
                OwnedThread = ownedThread
            };

            const string bio = "hello there friends";
            const string name = "testThread3";
            string creatorName = newUser.Username;
            const string category = "games and music";
            int threadId = ObjectIDBuilder.GetThreadID();
            Thread thread1 = new Thread()
            {
                ThreadID = id,
                Name = name,
                CreatorName = creatorName,
                Bio = bio,
                Category = category
            };
            newUser.OwnedThread = thread1;


            int id2 = ObjectIDBuilder.GetUserID();
            const string username2 = "robert";
            const string password2 = "asdf";
            const string gender2 = "male";
            DateTime dateJoinedD2 = DateTime.Now;
            Thread ownedThread2 = null;
            User newUser2 = new User() // user2
            {
                UserID = id2,
                Username = username2,
                Password = password2,
                Gender = gender2,
                DateJoined = dateJoinedD2,
                OwnedThread = ownedThread2
            };

            const string bio2 = "hello there friends";
            const string name2 = "testThread4";
            string creatorName2 = newUser2.Username;
            const string category2 = "games and music";
            int threadId2 = ObjectIDBuilder.GetThreadID();
            Thread thread2 = new Thread()
            {
                ThreadID = id2,
                Name = name2,
                CreatorName = creatorName2,
                Bio = bio2,
                Category = category2
            };
            newUser2.OwnedThread = thread2;

            threadRepo.AddThreadtoRepo(thread1);
            threadRepo.AddThreadtoRepo(thread2);

            userRepo.AddUsertoRepo(newUser);
            userRepo.AddUsertoRepo(newUser2);

            // act
            List<Thread> searchResult = userRepo.SearchForUsersAndThreads(name2); // search for thread by the name of the second thread object

            Thread parsedThread = null;
            foreach(Thread t in searchResult)
            {
                // get the search result out of the list structure and into a Thread variable
                if (t.Name == name2)
                    parsedThread = t;
            }

            // assert
            Assert.Equal(thread2, parsedThread);
            Assert.Equal(name2, parsedThread.Name);
        }
    }
}
