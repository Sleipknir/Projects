using WebForum.Models;
using System.Collections.Generic;

namespace WebForum.ViweModels
{
    public class ForumTopicVM
    {
        public Forum Forum{ get; set; }
        public IEnumerable<PostListingModel>Posts { get; set; }
    }
}
