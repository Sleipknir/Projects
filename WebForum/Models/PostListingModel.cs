using WebForum.Models;
namespace WebForum.Models
{
    public class PostListingModel
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string AuthorName { get; set; }
        public string AuthorId { get; set; }
        public string DatePosted { get; set; }
        public Forum Forum { get; set; }
    }
}
