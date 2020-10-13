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
    public class DrzaveApiController : ControllerBase
    {
        private readonly IDrzavaService _drzavaService;

        public DrzaveApiController(IDrzavaService drzavaService)
        {
            _drzavaService = drzavaService;
        }

        [HttpGet]
        public IActionResult Drzave([FromQuery] string search)
        {
            return Ok(new
            {
                drzave = _drzavaService.GetDrzave(search)
            });
        }

    }
}