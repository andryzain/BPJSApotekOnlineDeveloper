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

        [HttpGet]
        public IActionResult GetUserActives()
        {
            var user = _applicationDbContext.UserActives.ToList();
            if (user == null || !user.Any())
            {
                return NotFound(new { message = "Belum ada data user." });
            }
            return Ok(user);
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


        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserActiveViewModel userActive)
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
            if (ModelState.IsValid)
            {
                var userLogin = new ApplicationUser
                {
                    KodeUser = userActive.UserActiveCode,
                    NamaUser = userActive.FullName,
                    Email = userActive.Email,
                    UserName = userActive.Email,
                    PhoneNumber = userActive.Handphone,
                    IsActive = true
                };

                var user = new UserActive
                {
                    CreateDateTime = DateTimeOffset.Now,
                    CreateBy = Guid.NewGuid(),
                    UpdateDateTime = DateTimeOffset.MinValue,
                    UpdateBy = new Guid("00000000-0000-0000-0000-000000000000"),
                    DeleteDateTime = DateTimeOffset.MinValue,
                    DeleteBy = new Guid("00000000-0000-0000-0000-000000000000"),
                    UserActiveId = Guid.NewGuid(),
                    UserActiveCode = userActive.UserActiveCode,
                    FullName = userActive.FullName,
                    IdentityNumber = userActive.IdentityNumber,                   
                    PlaceOfBirth = userActive.PlaceOfBirth,
                    DateOfBirth = userActive.DateOfBirth,
                    Gender = userActive.Gender,
                    Address = userActive.Address,
                    Handphone = userActive.Handphone,
                    Email = userActive.Email,
                    IsActive = true
                };

                var passTglLahir = userActive.DateOfBirth.ToString("ddMMMyyyy");                

                var checkDuplicate = _applicationDbContext.UserActives.Where(c => c.UserActiveCode == userActive.UserActiveCode).ToList();

                if (checkDuplicate.Count == 0)
                {
                    var result = _applicationDbContext.UserActives.Where(c => c.UserActiveCode == userActive.UserActiveCode).FirstOrDefault();
                    if (result == null)
                    {
                        var resultLogin = await _userManager.CreateAsync(userLogin, passTglLahir);

                        if (resultLogin.Succeeded)
                        {
                            _applicationDbContext.UserActives.Add(user);
                            _applicationDbContext.SaveChanges();
                            return CreatedAtAction(nameof(GetUserActives), new { message = "Tambah Data Sukses" }, userActive);
                        }
                        else
                        {
                            return NotFound(new { message = "Data tidak valid !!!" });
                        }                        
                    }
                    else
                    {
                        return NotFound(new { message = "Data tidak dapat di input !!!" });
                    }
                }
                else
                {
                    return NotFound(new { message = "Terdapat duplikasi data !!!" });
                }
            }
            else
            {
                return NotFound(new { message = "Data tidak valid !!!" });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserActive update)
        {
            var user = _applicationDbContext.UserActives.Find(id);
            if (user == null) return NotFound();

            user.FullName = update.FullName;
            user.IdentityNumber = update.IdentityNumber;
            user.PlaceOfBirth = update.PlaceOfBirth;
            user.DateOfBirth = update.DateOfBirth;
            user.Gender = update.Gender;
            user.Address = update.Address;
            user.Handphone = update.Handphone;
            user.Email = update.Email;
            user.IsActive = update.IsActive;            

            _applicationDbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _applicationDbContext.UserActives.Find(id);
            if (user == null) return NotFound();

            _applicationDbContext.UserActives.Remove(user);
            _applicationDbContext.SaveChanges();
            return NoContent();
        }
    }
}
