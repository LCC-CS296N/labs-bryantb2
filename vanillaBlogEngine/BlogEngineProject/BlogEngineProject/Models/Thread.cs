using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngineProject.Models
{
    public class Thread
    {
        // private fields
        private List<Post> posts = new List<Post>();
        // auto implemented properties
        public int ThreadID { get; set; }
        public String Name { get; set; }
        public String CreatorName { get; set; }
        public String Bio { get; set; }
        public String Category { get; set; }
        public String ProfilePicURL { get; set; }
        public List<Post> Posts { get { return posts; } }

        // METHODS
        public Post GetPostById(int postId)
        {
            // loop through posts
            // return ref to post if IDs match
            foreach(Post p in Posts)
            {
                if (p.PostID == postId)
                    return p;
            }
            return null;
        }

        public void AddPostToThread(Post post) => Posts.Add(post);

        public Post RemovePostFromHistory(int postID)
        {
            // find post
            // then remove it
            foreach (Post p in Posts)
            {
                if (p.PostID == postID)
                {
                    Posts.Remove(p);
                    return p;
                }
            }
            return null;
        }
    }
}
