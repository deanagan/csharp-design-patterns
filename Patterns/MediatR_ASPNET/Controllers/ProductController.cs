using System;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

using Api.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("v1/api")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ILogger _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            this.productService = productService;
            this._logger = logger;
        }

        [HttpGet("[action]/{sku}")]
        public IActionResult GetProduct(string sku)
        {
            try
            {
                var product = productService.GetProduct(sku);
                this._logger.LogTrace("Status Code {0}", product != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound);
                return StatusCode(
                    product != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound,
                    product);
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
