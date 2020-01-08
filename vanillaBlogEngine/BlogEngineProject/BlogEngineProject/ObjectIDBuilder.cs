using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngineProject
{
    public static class ObjectIDBuilder
    {
        // SUMMARY: this class will be used to grab a unique ID that can then be assigned to comments, posts, and threads,

        // CLASS FIELDS
        private static int CommentID = 0;
        private static int PostID = 0;
        private static int UserID = 0;
        private static int ThreadID = 0;

        public static int GetPostID()
        {
            int ID = PostID;
            PostID += 1;
            return ID;
        }

        public static int GetCommentID()
        {
            int ID = CommentID;
            CommentID += 1;
            return ID;
        }

        public static int GetUserID()
        {
            int ID = UserID;
            UserID += 1;
            return ID;
        }

        public static int GetThreadID()
        {
            int ID = ThreadID;
            ThreadID += 1;
            return ID;
        }
    }
}
