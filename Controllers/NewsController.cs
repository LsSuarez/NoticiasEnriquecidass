using Microsoft.AspNetCore.Mvc;
using NewsPortal.Models;
using NewsPortal.Services;
using System.Threading.Tasks;

namespace NewsPortal.Controllers
{
    public class NewsController : Controller
    {
        private readonly IExternalApiService _apiService;

        public NewsController(IExternalApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _apiService.GetPostsAsync();
            return View(posts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var posts = await _apiService.GetPostsAsync();
            var post = posts.Find(p => p.id == id);
            if (post == null) return NotFound();

            var author = await _apiService.GetUserAsync(post.userId);
            var comments = await _apiService.GetCommentsAsync(id);

            ViewData["Author"] = author;
            ViewData["Comments"] = comments;

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> SendFeedback(int postId, string sentimiento)
        {
            bool success = await _apiService.SendFeedbackAsync(postId, sentimiento);
            if (success) return Ok(new { message = "Feedback enviado." });
            return BadRequest(new { message = "Error al enviar feedback." });
        }
    }
}
