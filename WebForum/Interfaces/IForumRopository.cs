using WebForum.Models;

namespace WebForum.Interfaces
{
    public interface IForumRopository
    {
        IEnumerable<Forum> GetAll();
        Forum GetById(int id);
        Task<IEnumerable<Forum>> GetForumByTitle(string title);
        bool Add(Forum forum);
        bool Update(Forum forum);
        bool Delete(Forum forum);
        bool Save();
    }
}
