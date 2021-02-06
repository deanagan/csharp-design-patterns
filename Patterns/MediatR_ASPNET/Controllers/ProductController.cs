using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

using Api.Interfaces;
using Api.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("v1/api")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            this._productService = productService;
            this._logger = logger;
        }

        [HttpGet("[action]")]
        public IActionResult Products()
        {
            try
            {
                this._logger.LogInformation("Get Products received");
                var productSkuCodes = _productService.GetProducts();
                return Ok(productSkuCodes);
            } catch(Exception ex) {
                this._logger.LogError("Bad request received");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Products(int id)
        {
            try
            {
                var product = _productService.GetProduct(id);
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
