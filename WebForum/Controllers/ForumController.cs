using Microsoft.AspNetCore.Mvc;
using WebForum.Data;
using WebForum.Interfaces;
using WebForum.Models;
using System.Linq;
using WebForum.ViweModels;

namespace WebForum.Controllers
{
    public class ForumController : Controller

    {
        
        private readonly IForumRopository _forumRopository;
        private readonly IPostRepository _postRepository;

        public ForumController(ApplicationDbContext context, IForumRopository forumRopository)
        {
            
            _forumRopository = forumRopository;
        }
        public IActionResult Index()
        {
            var forums =  _forumRopository.GetAll();
            return View(forums);
        }

       public async Task<IActionResult> Topic(int id)
        {
            var forum = _forumRopository.GetById(id);
            var posts = forum.Posts;

            var postListing = posts.Select(post => new PostListingModel
            {
                PostId = post.Id,
                AuthorId = post.UserName.Id,
                AuthorName = post.UserName.UserName,
                PostTitle = post.Title,
                DatePosted = post.Created.ToString(),
                Forum = BuildForumListing(post)
            });

            var model = new ForumTopicVM
            {
                Posts = postListing,
                Forum = BuildForumListing(forum)
            };

            return View(model);  
        }

        private Forum BuildForumListing(Post post)
        {
            var forum = post.Forum;
            return BuildForumListing(forum);
        }

        private Forum BuildForumListing(Forum forum)
        {
            
            return new Forum
            {
                Id = forum.Id,
                Title = forum.Title,
                Description = forum.Description
            };
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Forum forum)
        {
            if (ModelState.IsValid)
            {
                return View(forum);
            }
            forum.Created = DateTime.Now;
            
            _forumRopository.Add(forum);
        
            return RedirectToAction("Index");

        }
      
    }
}
