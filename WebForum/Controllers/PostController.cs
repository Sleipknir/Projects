using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebForum.Data;
using WebForum.Interfaces;
using WebForum.Models;
using WebForum.ViweModels;

namespace WebForum.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IForumRopository _forumRopository;

        private static UserManager<ApplicationUser> _userManager;

        public PostController(IPostRepository postRepository, IForumRopository forumRopository, UserManager<ApplicationUser> userManager)
        {
            _postRepository = postRepository;
            _forumRopository = forumRopository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int id)
        {
            //var posts = _postRepository.GetPostsByForumId(id);

           // ViewData["forumTitle"] = posts.ToList();
            return View();
        }
        public async Task<IActionResult> Create(int id)
        {
            var forum = _forumRopository.GetById(id);
            var model = new NewPostVM
                {
                    ForumTitle = forum.Title,
                    ForumId = forum.Id,
                    AuthorName = User.Identity.Name
                };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(NewPostVM model)
        {
            var userId = _userManager.GetUserId(User);
            var user =  _userManager.FindByIdAsync(userId).Result;
            var post = BuildPost(model, user);

            await _postRepository.Add(post);

            return RedirectToAction("Detail", "Post",new {id = post.Id });
        }

        private Post BuildPost(NewPostVM model, ApplicationUser user)
        {
            var forum = _forumRopository.GetById(model.ForumId); 
            return new Post
            {
                Title = model.Title,
                Content = model.Content,
                Created = DateTime.Now,
                UserName = user,
                Forum = forum
            };
        }

        public async Task<IActionResult> Detail(int id)
        {
            IEnumerable<Post> post = await _postRepository.GetByIdAsync(id);
            return View(post);
        }
    }
}
