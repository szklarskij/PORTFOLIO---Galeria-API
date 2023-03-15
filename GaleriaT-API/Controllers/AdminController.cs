using System.Collections;
using AutoMapper;
using GaleriaT_API.Entities;
using GaleriaT_API.Models;
using GaleriaT_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GaleriaT_API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [Authorize]
        [HttpPut("update")]
        public ActionResult Update([FromBody]AdminPasswordDto dto)
        {


            _adminService.Update(dto); 
          
            return Ok();    
        }
        [HttpPost("login")]

        public ActionResult Login([FromBody]AdminPasswordDto dto)
        {
            string token = _adminService.GenerateJwt(dto);
            return Ok(token);
        }

    }
}
