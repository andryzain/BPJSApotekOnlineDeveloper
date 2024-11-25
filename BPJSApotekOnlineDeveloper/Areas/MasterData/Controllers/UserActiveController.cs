using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BPJSApotekOnlineDeveloper.Areas.MasterData.Models;
using BPJSApotekOnlineDeveloper.Areas.MasterData.ViewModels;
using System.Data;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using PurchasingSystemDeveloper.Models;
using BPJSApotekOnlineDeveloper.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using BPJSApotekOnlineDeveloper.Models;

namespace BPJSApotekOnlineDeveloper.Areas.MasterData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Endpoint ini memerlukan token
    [EnableCors("AllowSpecific")]

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
                return NotFound(new { message = "Belum ada data user. || 404 Not Found" });
            }
            return Ok(new {message = "Berhasil || 200 OK"});
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
                            return CreatedAtAction(nameof(GetUserActives), new { message = "Tambah Data Berhasil || 201 Created" }, userActive);
                        }
                        else
                        {
                            return BadRequest(new { message = "Data tidak valid !!! || 400 Bad Request" });
                        }
                    }
                    else
                    {
                        return BadRequest(new { message = "Data tidak dapat di input !!! || 400 Bad Request" });
                    }
                }
                else
                {
                    return Conflict(new { message = "Terdapat duplikasi data !!! || 409 Conflict Data" });
                }
            }
            else
            {
                return BadRequest(new { message = "Data tidak valid !!! || 400 Bad Request" });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody] UserActiveViewModel update)
        {
            if (update == null)
            {
                return BadRequest("Data user tidak boleh kosong. || 400 Bad Request");
            }

            // Cari data berdasarkan ID
            var user = _applicationDbContext.UserActives.Find(id);
            if (user == null)
            {
                return NotFound($"User dengan ID {id} tidak ditemukan. || 404 Not Found");
            }

            try
            {
                // Perbarui data user
                user.FullName = update.FullName;
                user.IdentityNumber = update.IdentityNumber;
                user.PlaceOfBirth = update.PlaceOfBirth;
                user.DateOfBirth = update.DateOfBirth;
                user.Gender = update.Gender;
                user.Address = update.Address;
                user.Handphone = update.Handphone;
                user.Email = update.Email;
                user.IsActive = update.IsActive;

                // Tandai data sebagai telah diubah
                _applicationDbContext.UserActives.Update(user);

                // Simpan perubahan ke database
                _applicationDbContext.SaveChanges();

                return Ok(new { message = "Berhasil Update || 200 OK" } );
            }
            catch (Exception ex)
            {
                // Tangani error jika terjadi masalah
                return StatusCode(500, $"Terjadi kesalahan saat memperbarui data: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            // Cari data berdasarkan ID
            var user = _applicationDbContext.UserActives.Find(id);
            if (user == null)
            {
                return NotFound($"User dengan ID {id} tidak ditemukan. || 404 Not Found");
            }

            try
            {
                // Hapus Akun Login
                var userLogin = _signInManager.UserManager.Users.FirstOrDefault(s => s.KodeUser == user.UserActiveCode);
                _applicationDbContext.Attach(userLogin);
                _applicationDbContext.Entry(userLogin).State = EntityState.Deleted;
                _applicationDbContext.SaveChanges();

                // Hapus entitas dari database
                _applicationDbContext.UserActives.Remove(user);

                // Simpan perubahan
                _applicationDbContext.SaveChanges();

                return Ok(new { message = "Berhasil Hapus || 200 OK" });
            }
            catch (Exception ex)
            {
                // Tangani error jika ada masalah
                return StatusCode(500, $"Terjadi kesalahan saat menghapus data: {ex.Message}");
            }
        }

        [HttpGet("paged")]
        public IActionResult GetPagedUsers(int page = 1, int perPage = 2)
        {
            if (page <= 0 || perPage <= 0)
            {
                return BadRequest(new { status = "error", message = "Page and perPage must be greater than 0." });
            }

            // Total Rows
            var totalRows = _applicationDbContext.UserActives.Count();

            // Total Pages
            var totalPages = (int)Math.Ceiling(totalRows / (double)perPage);

            // Ambil Data Berdasarkan Pagination
            var rows = _applicationDbContext.UserActives
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToList();

            if (rows.Count == 0 && page > totalPages)
            {
                return NotFound(new { status = "error", message = "Page not found." });
            }

            // Buat Respons
            var response = new ApiResponse<PaginatedData<UserActive>>
            {
                Status = "success",
                Message = "Data retrieved successfully",
                Data = new PaginatedData<UserActive>
                {
                    Rows = rows,
                    TotalRows = totalRows,
                    CurrentPage = page,
                    PerPage = perPage,
                    TotalPages = totalPages
                }
            };

            return Ok(response);
        }
    }
}
