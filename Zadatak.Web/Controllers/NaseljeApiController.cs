using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zadatak.Web.Interfaces;
using Zadatak.Web.ViewModels;

namespace Zadatak.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NaseljeApiController : ControllerBase
    {
        private readonly INaseljaService _naseljaService;

        public NaseljeApiController(INaseljaService naseljaService)
        {
            _naseljaService = naseljaService;
        }

        [HttpGet]
        public IActionResult Naselja([FromQuery] PaginationFilterViewModel filters)
        {
            return Ok(new
            {
                naselja = _naseljaService.GetFilteredNaselja(filters),
                count = _naseljaService.CountNaselja()
            });
        }

        [HttpPost]
        public IActionResult Naselje([FromBody] NaseljeViewModel model)
        {
            StatusCodeViewModel status = _naseljaService.NewNaselje(model);
            return StatusCode(status.StatusCode, new {
                code = status.StatusCode,
                status = status.StatusMessage
            });
        }

        [HttpPut]
        public IActionResult UpdateNaselje([FromBody] NaseljeViewModel model)
        {
            StatusCodeViewModel status = _naseljaService.UpdateNaselje(model);
            return StatusCode(status.StatusCode, new
            {
                code = status.StatusCode,
                status = status.StatusMessage
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNaselje(int id)
        {
            StatusCodeViewModel status = _naseljaService.DeleteNaselje(id);
            return StatusCode(status.StatusCode, new
            {
                code = status.StatusCode,
                status = status.StatusMessage
            });
        }

    }
}