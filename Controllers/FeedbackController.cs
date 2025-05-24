using Microsoft.AspNetCore.Mvc;
using NewsPortal.Data;
using NewsPortal.Models;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace NewsPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackContext _context;

        public FeedbackController(FeedbackContext context)
        {
            _context = context;
        }

        // GET: api/feedback
        [HttpGet]
        public IActionResult GetAll()
        {
            var feedbacks = _context.Feedbacks.ToList();
            return Ok(feedbacks);
        }

        // POST: api/feedback
        [HttpPost]
        public async Task<IActionResult> PostFeedback([FromBody] Feedback feedback)
        {
            if (feedback == null)
                return BadRequest("Feedback no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(feedback.Sentimiento))
                return BadRequest("El sentimiento es obligatorio.");

            if (feedback.PostId <= 0)
                return BadRequest("PostId inválido.");

            // Normalizar sentimiento para evitar problemas (por ejemplo, "Like", "like ", etc.)
            var sentimientoNormalized = feedback.Sentimiento.Trim().ToLower();

            if (sentimientoNormalized != "like" && sentimientoNormalized != "dislike")
                return BadRequest("El sentimiento debe ser 'like' o 'dislike'.");

            bool existe = _context.Feedbacks.Any(f => f.PostId == feedback.PostId);
            if (existe)
                return BadRequest("Este post ya tiene feedback.");

            feedback.Sentimiento = sentimientoNormalized;
            feedback.Fecha = DateTime.UtcNow;

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return Ok(feedback);
        }
    }
}
