using Microsoft.EntityFrameworkCore;
using WebForum.Data;
using WebForum.Interfaces;
using WebForum.Models;

namespace WebForum.Repository
{
    public class ForumRepository : IForumRopository
    {
        private readonly ApplicationDbContext _context;

        public ForumRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Forum forum)
        {
            _context.Add(forum);
            return Save();
        }

        public bool Delete(Forum forum)
        {
            _context.Remove(forum);
            return Save();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _context.Forums.Include(f => f.Posts);
        }

        public Forum GetById(int id)
        {
            var forum = _context.Forums.Where(forum => forum.Id == id).
                 Include(forum => forum.Posts).ThenInclude(p => p.UserName).
                Include(forum => forum.Posts).ThenInclude(p => p.Replies).
                ThenInclude(r => r.User).FirstOrDefault(); 
            return forum;
        }

        public async Task<IEnumerable<Forum>> GetForumByTitle(string title)
        {
            return await _context.Forums.Where(x => x.Title == title).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Forum forum)
        {
            _context.Update(forum);
            return Save();
        }
    }
}
