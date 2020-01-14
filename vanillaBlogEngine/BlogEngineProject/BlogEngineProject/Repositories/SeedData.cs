using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngineProject.Models;

namespace BlogEngineProject.Repositories
{
    public class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();

            if(!context.Users.Any())
            {
                RealUserRepo userRepo = new RealUserRepo(context);
                RealThreadRepo threadRepo = new RealThreadRepo(context);

                // Add users
                const string username = "Sally";
                const string password = "1234";
                const string gender = "female";
                DateTime dateJoinedD = DateTime.Now;
                AppUser newUser = new AppUser() // user1
                {
                    Name = username,
                    Password = password,
                    Gender = gender,
                    DateJoined = dateJoinedD
                };

                const string bio = "hello there friends";
                const string name = "testThread1";
                string creatorName = newUser.Name;
                string category = ThreadCategories.GetCategory(0);
                Thread thread1 = new Thread()
                {
                    Name = name,
                    CreatorName = creatorName,
                    Bio = bio,
                    Category = category
                };
                threadRepo.AddThreadtoRepo(thread1);
                newUser.OwnedThread = thread1;
                userRepo.AddUsertoRepo(newUser);
                

                // ---------------------------------------->

                const string username2 = "Robert";
                const string password2 = "5678";
                const string gender2 = "male";
                DateTime dateJoinedD2 = DateTime.Now;
                AppUser newUser2 = new AppUser() // user2
                {
                    Name = username2,
                    Password = password2,
                    Gender = gender2,
                    DateJoined = dateJoinedD2
                };

                const string bio2 = "hello there friends";
                const string name2 = "testThread2";
                string creatorName2 = newUser2.Name;
                string category2 = ThreadCategories.GetCategory(1);
                Thread thread2 = new Thread()
                {
                    Name = name2,
                    CreatorName = creatorName2,
                    Bio = bio2,
                    Category = category2
                };
                threadRepo.AddThreadtoRepo(thread2);
                newUser2.OwnedThread = thread2;
                userRepo.AddUsertoRepo(newUser2);

                // ---------------------------------------->

                const string username3 = "Jim";
                const string password3 = "1011";
                const string gender3 = "male";
                DateTime dateJoinedD3 = DateTime.Now;
                AppUser newUser3 = new AppUser() // user2
                {
                    Name = username3,
                    Password = password3,
                    Gender = gender3,
                    DateJoined = dateJoinedD3
                };

                const string bio3 = "hello there friends";
                const string name3 = "testThread3";
                string creatorName3 = newUser3.Name;
                string category3 = ThreadCategories.GetCategory(2);
                Thread thread3 = new Thread()
                {
                    Name = name3,
                    CreatorName = creatorName3,
                    Bio = bio3,
                    Category = category3
                };
                threadRepo.AddThreadtoRepo(thread3);
                newUser3.OwnedThread = thread3;
                userRepo.AddUsertoRepo(newUser3);

                // ---------------------------------------->




            }
        }
    }
}
