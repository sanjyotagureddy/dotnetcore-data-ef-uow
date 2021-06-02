using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DotNet.Core.Data.UoW.Repositories.Interfaces;

namespace DotNet.Core.Data.UoW.TestApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IUnitOfWork context, ILogger<ProductController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _context.Products.GetAllAsync();
            return Ok(result);
        }
    }
}
