using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using NewsPortal.Models;

namespace NewsPortal.Services
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;

        public ExternalApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Post>>("https://jsonplaceholder.typicode.com/posts");
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await _httpClient.GetFromJsonAsync<User>($"https://jsonplaceholder.typicode.com/users/{userId}");
        }

        public async Task<List<Comment>> GetCommentsAsync(int postId)
        {
            return await _httpClient.GetFromJsonAsync<List<Comment>>($"https://jsonplaceholder.typicode.com/comments?postId={postId}");
        }

        public async Task<bool> SendFeedbackAsync(int postId, string sentimiento)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:5261/api/feedback", new { PostId = postId, Sentimiento = sentimiento });
            return response.IsSuccessStatusCode;
        }
    }
}
