using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Models;
using OnlineBookStore.Services;

namespace OnlineBookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IServiceClass _service;
        public BookController(IServiceClass service)
        {
            _service = service;
        }

        [HttpGet("GetTopSellingAuthors")]
        public async Task<ActionResult<IList<Author>>> GetTopSellingAuthors()
        {
            var topAuthorsResult = _service.GetTopSellingAuthors();

            if (!string.IsNullOrEmpty(topAuthorsResult.Item2)) 
                return StatusCode(500, topAuthorsResult.Item2);

            return Ok(topAuthorsResult.Item1);
        }
    
        [HttpDelete("DeleteBooks")]
        public async Task<ActionResult<bool>> DeleteBooks(List<int> bookIds)
        {
            var deleteedBook = _service.DeleteBooks(bookIds);
            if (!string.IsNullOrEmpty(deleteedBook.Item2))
                return StatusCode(500, deleteedBook.Item2);
            return Ok(deleteedBook.Item1);
        }

        [HttpGet("CalculateRevenue")]
        public async Task<ActionResult<Book>> CalculateRevenue(DateTime startDate, DateTime endDate)
        {
            var totalRevenue = _service.CalculateRevenue(startDate, endDate);

            if (!string.IsNullOrEmpty(totalRevenue.Item2))
                return StatusCode(500, totalRevenue.Item2);

            return Ok(totalRevenue.Item1);
        }
    }
}
