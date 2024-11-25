using BPJSApotekOnlineDeveloper.Areas.MasterData.Models;
using BPJSApotekOnlineDeveloper.Areas.MasterData.ViewModels;
using BPJSApotekOnlineDeveloper.Models;
using BPJSApotekOnlineDeveloper.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BPJSApotekOnlineDeveloper.Areas.MasterData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Endpoint ini memerlukan token
    [EnableCors("AllowSpecific")]

    public class PendaftaranController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;


        public PendaftaranController(
            ApplicationDbContext applicationDbContext
        )
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult GetPendaftarans()
        {
            var pendaftaran = _applicationDbContext.Pendaftarans.ToList();
            if (pendaftaran == null || !pendaftaran.Any())
            {
                return NotFound(new { message = "Belum ada data pendaftaran. || 404 Not Found" });
            }
            return Ok(new { message = "Berhasil || 200 OK" });
        }

        [HttpPost]
        public IActionResult AddPendaftaran([FromBody] PendaftaranViewModel pendaftaran)
        {
            var dateNow = DateTimeOffset.Now;
            var day = dateNow.Day;
            var month = dateNow.Month;
            var year = dateNow.Year;

            var setDateNow = DateTimeOffset.Now.ToString("yyMMdd");

            var lastCode = _applicationDbContext.Pendaftarans
                .Where(d => d.CreateDateTime.Day == day && d.CreateDateTime.Month == month && d.CreateDateTime.Year == year)
                .OrderByDescending(k => k.NoKartuBpjs)
                .FirstOrDefault();

            if (lastCode == null)
            {
                pendaftaran.NoKartuBpjs = "CRD" + setDateNow + "0001";
            }
            else
            {
                var lastCodeTrim = lastCode.NoKartuBpjs.Substring(3, 6);

                if (lastCodeTrim != setDateNow)
                {
                    pendaftaran.NoKartuBpjs = "CRD" + setDateNow + "0001";
                }
                else
                {
                    pendaftaran.NoKartuBpjs = "CRD" + setDateNow +
                        (Convert.ToInt32(lastCode.NoKartuBpjs.Substring(9, lastCode.NoKartuBpjs.Length - 9)) + 1).ToString("D4");
                }
            }

            if (ModelState.IsValid)
            {
                var daftar = new Pendaftaran
                {
                    CreateDateTime = DateTimeOffset.Now,
                    CreateBy = Guid.NewGuid(),
                    UpdateDateTime = DateTimeOffset.MinValue,
                    UpdateBy = new Guid("00000000-0000-0000-0000-000000000000"),
                    DeleteDateTime = DateTimeOffset.MinValue,
                    DeleteBy = new Guid("00000000-0000-0000-0000-000000000000"),
                    PendaftaranId = Guid.NewGuid(),
                    NoKartuBpjs = pendaftaran.NoKartuBpjs,
                    NamaLengkapPeserta = pendaftaran.NamaLengkapPeserta,
                    JenisKelamin = pendaftaran.JenisKelamin,
                    TanggalLahir = pendaftaran.TanggalLahir,
                    AlamatTinggal = pendaftaran.AlamatTinggal,
                    NoTelepon = pendaftaran.NoTelepon,
                    Email = pendaftaran.Email,
                    NoIdentitasKTPSIM = pendaftaran.NoIdentitasKTPSIM,
                    StatusKepesertaanBpjs = pendaftaran.StatusKepesertaanBpjs,
                    TipeKepesertaanBpjs = pendaftaran.TipeKepesertaanBpjs,
                    TanggalPendaftaranBpjs = pendaftaran.TanggalPendaftaranBpjs,
                    NamaKeluarga = pendaftaran.NamaKeluarga,
                    NamaApotek = pendaftaran.NamaApotek,
                    StatusVerifikasi = pendaftaran.StatusVerifikasi,
                    InformasiObat = pendaftaran.InformasiObat
                };

                var checkDuplicate = _applicationDbContext.Pendaftarans.Where(c => c.NoKartuBpjs == pendaftaran.NoKartuBpjs).ToList();

                if (checkDuplicate.Count == 0)
                {
                    var result = _applicationDbContext.Pendaftarans.Where(c => c.NoKartuBpjs == pendaftaran.NoKartuBpjs).FirstOrDefault();
                    if (result == null)
                    {
                        _applicationDbContext.Pendaftarans.Add(daftar);
                        _applicationDbContext.SaveChanges();
                        return CreatedAtAction(nameof(GetPendaftarans), new { message = "Tambah Data Sukses || 201 Created" }, pendaftaran);
                    }
                    else
                    {
                        return BadRequest(new { message = "Data tidak valid !!! || 400 Bad Request" });
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
        public IActionResult UpdatePendaftaran(Guid id, [FromBody] PendaftaranViewModel updatePendaftaran)
        {
            if (updatePendaftaran == null)
            {
                return BadRequest("Data pendaftaran tidak boleh kosong. || 400 Bad Request");
            }

            // Cari data berdasarkan ID
            var pendaftaran = _applicationDbContext.Pendaftarans.Find(id);
            if (pendaftaran == null)
            {
                return NotFound($"Pendaftaran dengan ID {id} tidak ditemukan. || 404 Not Found");
            }

            try
            {
                // Perbarui data pendaftaran
                pendaftaran.NamaLengkapPeserta = updatePendaftaran.NamaLengkapPeserta;
                pendaftaran.JenisKelamin = updatePendaftaran.JenisKelamin;
                pendaftaran.TanggalLahir = updatePendaftaran.TanggalLahir;
                pendaftaran.AlamatTinggal = updatePendaftaran.AlamatTinggal;
                pendaftaran.NoTelepon = updatePendaftaran.NoTelepon;
                pendaftaran.Email = updatePendaftaran.Email;
                pendaftaran.NoIdentitasKTPSIM = updatePendaftaran.NoIdentitasKTPSIM;
                pendaftaran.StatusKepesertaanBpjs = updatePendaftaran.StatusKepesertaanBpjs;
                pendaftaran.TipeKepesertaanBpjs = updatePendaftaran.TipeKepesertaanBpjs;
                pendaftaran.TanggalPendaftaranBpjs = updatePendaftaran.TanggalPendaftaranBpjs;
                pendaftaran.NamaKeluarga = updatePendaftaran.NamaKeluarga;
                pendaftaran.NamaApotek = updatePendaftaran.NamaApotek;
                pendaftaran.StatusVerifikasi = updatePendaftaran.StatusVerifikasi;
                pendaftaran.InformasiObat = updatePendaftaran.InformasiObat;

                // Tandai data sebagai telah diubah
                _applicationDbContext.Pendaftarans.Update(pendaftaran);

                // Simpan perubahan ke database
                _applicationDbContext.SaveChanges();

                return Ok(new { message = "Berhasil Update || 200 OK" });
            }
            catch (Exception ex)
            {
                // Tangani error jika terjadi masalah
                return StatusCode(500, $"Terjadi kesalahan saat memperbarui data: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePendaftaran(Guid id)
        {
            // Cari data berdasarkan ID
            var pendaftaran = _applicationDbContext.Pendaftarans.Find(id);
            if (pendaftaran == null)
            {
                return NotFound($"Pendaftaran dengan ID {id} tidak ditemukan. || 404 Not Found");
            }

            try
            {
                // Hapus entitas dari database
                _applicationDbContext.Pendaftarans.Remove(pendaftaran);

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
        public IActionResult GetPagedPendaftarans(int page = 1, int perPage = 2)
        {
            if (page <= 0 || perPage <= 0)
            {
                return BadRequest(new { status = "error", message = "Page and perPage must be greater than 0." });
            }

            // Total Rows
            var totalRows = _applicationDbContext.Pendaftarans.Count();

            // Total Pages
            var totalPages = (int)Math.Ceiling(totalRows / (double)perPage);

            // Ambil Data Berdasarkan Pagination
            var rows = _applicationDbContext.Pendaftarans
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToList();

            if (rows.Count == 0 && page > totalPages)
            {
                return NotFound(new { status = "error", message = "Page not found." });
            }

            // Buat Respons
            var response = new ApiResponse<PaginatedData<Pendaftaran>>
            {
                Status = "success",
                Message = "Data retrieved successfully",
                Data = new PaginatedData<Pendaftaran>
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
