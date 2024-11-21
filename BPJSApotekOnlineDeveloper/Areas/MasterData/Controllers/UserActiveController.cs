using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using BPJSApotekOnlineDeveloper.Areas.MasterData.Models;
using BPJSApotekOnlineDeveloper.Areas.MasterData.ViewModels;
using System.Data;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using PurchasingSystemDeveloper.Models;
using BPJSApotekOnlineDeveloper.Areas.MasterData.Repositories;
using BPJSApotekOnlineDeveloper.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace BPJSApotekOnlineDeveloper.Areas.MasterData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Endpoint ini memerlukan token
    public class UserActiveController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<UserActiveController> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UserActiveController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserActiveRepository userActiveRepository,

            ApplicationDbContext applicationDbContext,
            ILogger<UserActiveController> logger,
            IHostingEnvironment hostingEnvironment
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
                
            _applicationDbContext = applicationDbContext;
            _logger = logger;   
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("GetAspNetUsers")]
        public IActionResult GetAspNetUsers()
        {
            var users = _applicationDbContext.Users.ToList(); // _context.Users adalah akses ke tabel AspNetUsers

            if (users == null || !users.Any())
            {
                return NotFound(new { message = "Belum ada data pengguna." });
            }

            return Ok(users);
        }


        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserActive userActive)
        {
            var dateNow = DateTimeOffset.Now;
            var day = dateNow.Day;
            var month = dateNow.Month;
            var year = dateNow.Year;

            var setDateNow = DateTimeOffset.Now.ToString("yyMMdd");

            // Generate UserActiveCode
            var lastCode = _applicationDbContext.UserActives
                .Where(d => d.DateOfBirth.Day == day && d.DateOfBirth.Month == month && d.DateOfBirth.Year == year)
                .OrderByDescending(k => k.UserActiveCode)
                .FirstOrDefault();

            if (lastCode == null)
            {
                userActive.UserActiveCode = "USR" + setDateNow + "0001";
            }
            else
            {
                var lastCodeTrim = lastCode.UserActiveCode.Substring(3, 6);

                if (lastCodeTrim != setDateNow)
                {
                    userActive.UserActiveCode = "USR" + setDateNow + "0001";
                }
                else
                {
                    userActive.UserActiveCode = "USR" + setDateNow +
                                                (Convert.ToInt32(lastCode.UserActiveCode.Substring(9)) + 1).ToString("D4");
                }
            }

            // Validate ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Data tidak valid!" });
            }

            // Check for duplicate UserActiveCode
            var checkDuplicate = _applicationDbContext.UserActives
                .FirstOrDefault(c => c.UserActiveCode == userActive.UserActiveCode);

            if (checkDuplicate != null)
            {
                return Conflict(new { message = "Terdapat duplikasi data!" });
            }

            // Add User to AspNetUsers
            var identityUser = new IdentityUser
            {
                UserName = userActive.Email,
                Email = userActive.Email,
                PhoneNumber = userActive.Handphone,
            };


            // Add User to UserActive
            userActive.UserActiveId = Guid.NewGuid();
            _applicationDbContext.UserActives.Add(userActive);
            await _applicationDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(AddUser), new { message = "Tambah Data Sukses" }, userActive);
        }

    }
}
