using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Repositories;
using BloggieWeb.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace BloggieWeb.Controllers
{
    [Route("MstUser")]

    public class MstUserController(IMstUserService userService, UserManager<IdentityUser> userManager) : Controller
    {
        private readonly IMstUserService _userService = userService;
        private readonly UserManager<IdentityUser> _userManager = userManager;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }
    }
}
