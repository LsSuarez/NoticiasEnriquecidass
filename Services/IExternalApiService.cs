using System.Collections.Generic;
using System.Threading.Tasks;
using NewsPortal.Models;

namespace NewsPortal.Services
{
    public interface IExternalApiService
    {
        Task<List<Post>> GetPostsAsync();
        Task<User> GetUserAsync(int userId);
        Task<List<Comment>> GetCommentsAsync(int postId);
        Task<bool> SendFeedbackAsync(int postId, string sentimiento);
    }
}
