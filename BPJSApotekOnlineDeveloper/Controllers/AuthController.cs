using BPJSApotekOnlineDeveloper.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PurchasingSystemDeveloper.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BPJSApotekOnlineDeveloper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(
            IConfiguration configuration,

            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _configuration = configuration;

            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            // Simulasi data user (bisa diganti dengan database)
            //var users = new List<LoginModel>
            //{
            //    new LoginModel { Username = "admin", Password = "admin@admin123" },
            //    new LoginModel { Username = "user", Password = "user@user123" }
            //};

            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    return BadRequest(new { message = "User sedang online" });
                }
                else
                {
                    if (model.Email == "superadmin@admin.com" && model.Password == "Admin@123")
                    {
                        // Membuat token JWT
                        var jwtSettings = _configuration.GetSection("Jwt");
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var claims = new[]
                        {
                                new Claim(JwtRegisteredClaimNames.Sub, model.Email),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                            };

                        var token = new JwtSecurityToken(
                            issuer: jwtSettings["Issuer"],
                            audience: jwtSettings["Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpirationInMinutes"])),
                            signingCredentials: credentials
                        );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                    else
                    {
                        var user = await _signInManager.UserManager.FindByNameAsync(model.Email);

                        // Cek apakah user ada
                        if (user == null)
                        {
                            return BadRequest(new { message = "User belum terdaftar" });
                        }
                        else if (user.IsActive == true && user.IsOnline == false)
                        {
                            // Cek password
                            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);
                            if (result.Succeeded)
                            {
                                // Membuat token JWT
                                var jwtSettings = _configuration.GetSection("Jwt");
                                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
                                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                                var claims = new[]
                                {
                                new Claim(JwtRegisteredClaimNames.Sub, model.Email),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                            };

                                var token = new JwtSecurityToken(
                                    issuer: jwtSettings["Issuer"],
                                    audience: jwtSettings["Audience"],
                                    claims: claims,
                                    expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpirationInMinutes"])),
                                    signingCredentials: credentials
                                );

                                return Ok(new
                                {
                                    token = new JwtSecurityTokenHandler().WriteToken(token),
                                    expiration = token.ValidTo
                                });
                            }
                            //else
                            //{
                            //    return Unauthorized(new { message = "Password salah" });
                            //}

                            if (result.IsLockedOut)
                            {
                                // HttpContext.session.Clear untuk menghapus session data pengguna tidak lagi tersimpan
                                //HttpContext.Session.Clear();

                                // Hitung waktu yang tersisa
                                var lockTime = await _userManager.GetLockoutEndDateAsync(user);
                                var timeRemaining = lockTime.Value - DateTimeOffset.UtcNow;

                                //TempData["UserLockOut"] = "Sorry, your account is locked in " + timeRemaining.Minutes + " minutes " + timeRemaining.Seconds + " seconds";
                                //return View(model);
                            }
                        }
                        else if (user.IsActive == true && user.IsOnline == true)
                        {
                            //TempData["UserOnlineMessage"] = "Sorry, your account is online, has been logged out, please sign back in !";

                            user.IsOnline = false;
                            await _userManager.UpdateAsync(user);

                            return Ok(user);
                        }
                        else
                        {
                            return NotFound(new { message = "Maaf, akun anda belum aktif..." });
                        }
                    }
                }
            }

            return Ok();
        }
    }
}
