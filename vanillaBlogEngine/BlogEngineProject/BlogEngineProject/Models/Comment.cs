using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngineProject.Models
{
    public class Comment
    {
        // auto setting properties
        public int CommentID { get; set; }
        public String Username { get; set; }
        public String Content { get; set; }
        public DateTime DatePublished { get; set; }
    }
}
