using WebForum.Models;

namespace WebForum.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAll();
        Task<IEnumerable<Post>> GetByIdAsync(int id);
        Task<IEnumerable<Post>> GetPostsByTitle(string title);
        IEnumerable<Post> GetPostsByForumId(int id);
        Task<IEnumerable<Post>> GetFilteredPosts(int id);

        Task<IEnumerable<Post>> GetForumId(int id);

        Task Add(Post post);
        bool Update(Post post);
        bool Delete(Post post);
        bool Save();
      
    }
}
