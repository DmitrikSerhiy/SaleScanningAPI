using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SaleScanningAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        public SaleController()
        {

        }

        [HttpGet("products", Name = "GetAllProducts")]
        public IActionResult GetProducts()
        {
            return Ok("Initial");
        }
    }
}