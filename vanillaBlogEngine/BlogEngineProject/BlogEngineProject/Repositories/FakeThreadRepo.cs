using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngineProject.Models;

namespace BlogEngineProject.Repositories
{
    public class FakeThreadRepo : IThreadRepo
    {
        // CLASS FIELDS
        private static List<Thread> activeThreads = new List<Thread>();

        // METHODS
        public List<Thread> GetThreads() => activeThreads;
        public Thread GetThreadById(int threadId) => FindThreadById(threadId);
        public Thread GetThreadByName(string threadname) => FindThreadByName(threadname);
        public bool GetThreadnameEligibility(string username) => !(IsThreadnameTaken(username));

        public List<Thread> GetCategoryOfThreads(int categoryIndex)
        {
            string CATEGORY = ThreadCategories.GetCategory(categoryIndex);
            List<Thread> categorySpecificThreads = new List<Thread>();
            // iterate through activeThreads list
            // add a reference to the thread in categorySEpcificThread list
            foreach (Thread t in activeThreads)
            {
                if (t.Category == CATEGORY)
                    categorySpecificThreads.Add(t);
            }
            return categorySpecificThreads;
        }

        public void AddThreadtoRepo(Thread thread)
        {
            if (IsThreadnameTaken(thread.Name) == false)
            {
                activeThreads.Add(thread);
            }
            else
            {
                throw new ArgumentException("Please make sure that the thread name is unique!");
            }
        }

        public Thread RemoveThreadfromRepo(int threadID)
        {
            // find thread
            // then remove it
            Thread removedThread = null;
            foreach (Thread t in FakeThreadRepo.activeThreads)
            {
                if (t.ThreadID == threadID)
                {
                    removedThread = t;
                    activeThreads.Remove(t);
                    return removedThread;
                }
            }
            return removedThread;
        }

        public void AddThreadPost(int threadId, Post newPost)
        {
            // TODO: code this for memory repo
        }

        public void RemoveThreadPost(int threadId, int postId)
        {
            // TODO: code this for memory repo
        }

        public void EditThreadPost(int threadId, int postId, string editedTitle, string editedContent)
        {
            // TODO: code this for memory repo
        }

        public void EditThreadProfile(string editedThreadname, string editedThreadCategory, string editedBio, int threadId)
        {
            // TODO: code this for memory repo
        }

        private bool IsThreadnameTaken(String threadName)
        {
            // looks through the thread list for an identical threadname string
            // if the threadname is taken, return true
            foreach (Thread t in activeThreads)
            {
                if (t.Name == threadName)
                    return true;
            }
            return false;
        }

        private Thread FindThreadById(int threadId)
        {
            // run foreach loop on userlist
            // return true if current thread's ID matches the parameter
            foreach (Thread t in activeThreads)
            {
                if (t.ThreadID == threadId)
                    return t;
            }
            return null;
        }

        private Thread FindThreadByName(string threadname)
        {
            // determine if threadName exists
            // run foreach loop on userlist
            // return true if current user's ID matches the parameter
            bool doesThreadExist = IsThreadnameTaken(threadname);
            if (doesThreadExist == true)
            {
                foreach (Thread t in activeThreads)
                {
                    if (t.Name == threadname)
                        return t;
                }
            }
            return null;
        }
    }
}
