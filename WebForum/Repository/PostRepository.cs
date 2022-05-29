using Microsoft.EntityFrameworkCore;
using WebForum.Data;
using WebForum.Interfaces;
using WebForum.Models;

namespace WebForum.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public bool Delete(Post post)
        {
            _context.Remove(post);
            return Save();
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetByIdAsync(int id)
        {
            return await _context.Posts.Where(x => x.Id == id).ToListAsync();//Include(x =>x.Title).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Post>> GetFilteredPosts(int id)
        {
            return _context.Forums.Where(f => f.Id == id).First().Posts;
        }

        public async Task<IEnumerable<Post>> GetForumId(int id)
        {
            return (IEnumerable<Post>)_context.Posts.Where(x => x.Forum.Id == id).First().Forum;
        }

        public IEnumerable<Post> GetPostsByForumId(int id)
        {
            return _context.Forums.Where(_f => _f.Id == id).First().Posts;
        }

        public async Task<IEnumerable<Post>> GetPostsByTitle(string title)
        {
            return await _context.Posts.Where(x => x.Title == title).ToListAsync();
        }

       
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Post post)
        {
            _context.Update(post);
            return Save();
        }
    }
}
