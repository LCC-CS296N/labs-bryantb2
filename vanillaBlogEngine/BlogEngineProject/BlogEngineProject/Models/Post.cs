using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlogEngineProject.Models
{
    public class Post
    {
        // auto setting properties
        public int PostID { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public String Title { get; set; }

        [StringLength(100, MinimumLength = 15)]
        public String Content { get; set; }
        public String ImageURL { get; set; }
        public List<Comment> Comments { get; set; }
        public DateTime TimeStamp { get; set; }
        
        // METHODS
        public void AddCommentToHistory(Comment comment) => Comments.Add(comment);

        public Comment RemoveCommentFromHistory(int commentID)
        {
            // find comment
            // then remove it
            Comment removedComment = null;
            foreach(Comment c in Comments)
            {
                if (c.CommentID == commentID)
                {
                    removedComment = c;
                    Comments.Remove(c);
                }   
            }
            return removedComment;
        }
    }
}
