using System;
using System.Collections.Generic;
using System.Text;
using BlogEngineProject.Controllers;
using Xunit;
using BlogEngineProject.Repositories;
using BlogEngineProject;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests
{
    public class BlogControllerTests
    {
        [Fact]
        public void SignUpTest()
        {
            // arrange
            FakeUserRepo userRepo = new FakeUserRepo();
            FakeThreadRepo threadRepo = new FakeThreadRepo();
            MyBlogController blogController = new MyBlogController(userRepo,threadRepo);
            
            const string username = "Jack";
            const string password = "7890";
            const string confirmedPassword = password;

            // act 
           // blogController.SignUpRedirect(username, password, confirmedPassword);
                // NULL ERROR WHEN A METHOD HAS TEMPDATA : NEEDS A MOCK SESSION OBJECT?!?

            // assert
            int repoCount = userRepo.GetUsers().Count;
            string usernameFromRepo = userRepo.GetUsers()[repoCount - 1].Name;
            Assert.Equal(username, usernameFromRepo); // gets most recently added user
        }
    }
}
