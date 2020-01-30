using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BlogEngineProject.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(100, MinimumLength = 2)]
        [Required]
        public String Name { get; set; }

        public String Gender { get; set; }
        public DateTime DateJoined { get; set; }
        public Thread OwnedThread { get; set; }
        public List<Thread> FavoriteThreads { get; set; } // will store a list of thread DB ID numbers
        public List<Comment> CommentHistory { get; set; }

        // METHODS
        public void AddToFavorites(Thread thread) => FavoriteThreads.Add(thread);

        public Thread RemoveFromFavorites(int threadID)
        {
            // find threadID
            // then remove it
            Thread removedThread = null;
            List<Thread> threadList = FavoriteThreads;
            foreach (Thread thread in FavoriteThreads)
            {
                if (thread.ThreadID == threadID)
                {
                    removedThread = thread;
                    FavoriteThreads.Remove(thread);
                    return removedThread;
                }
            }
            return removedThread;
        }

        public void AddCommentToHistory(Comment comment) => CommentHistory.Add(comment);

        public Comment RemoveCommentFromHistory(int commentID)
        {
            // find comment
            // then remove it
            Comment removedComment = null;
            foreach (Comment c in CommentHistory)
            {
                if (c.CommentID == commentID)
                {
                    removedComment = c;
                    CommentHistory.Remove(c);
                    return removedComment;
                }
            }
            return removedComment;
        }
    }
}
