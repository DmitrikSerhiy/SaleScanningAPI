using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleScanningAPI.Services;

namespace SaleScanningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminalController : ControllerBase
    {
        private ITerminalService terminal;
        public TerminalController(ITerminalService Terminal)
        {
            terminal = Terminal;
            terminal.SetPricing();
        }

        [HttpGet("products", Name = "GetRegisteredProducts")]
        public IActionResult GetProducts()
        {
            return Ok(terminal.GetRegisteredProducts());
        }

        [HttpPost("products", Name = "Purchace")]
        public IActionResult Purchace([FromBody] string[] productNames)
        {
            foreach (var productName in productNames)
                if (!terminal.IsExist(productName))
                    ModelState.AddModelError("", $"Item '{productName}' is not found; ");

            if (!ModelState.IsValid)
                return NotFound(GetErrorMessages());

            terminal.Scan(productNames);


            return Ok(terminal.GetScannedProducts().ToList());
        }

        private string GetErrorMessages()
        {
            StringBuilder errorMessages = new StringBuilder();
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    errorMessages.AppendJoin(" ", error.ErrorMessage);
                }
            }
            return errorMessages.ToString();
        }
    }
}