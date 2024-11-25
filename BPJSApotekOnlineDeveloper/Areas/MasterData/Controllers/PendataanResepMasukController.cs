using BPJSApotekOnlineDeveloper.Areas.MasterData.Models;
using BPJSApotekOnlineDeveloper.Areas.MasterData.ViewModels;
using BPJSApotekOnlineDeveloper.Models;
using BPJSApotekOnlineDeveloper.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BPJSApotekOnlineDeveloper.Areas.MasterData.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Endpoint ini memerlukan token
    [EnableCors("AllowSpecific")]

    public class PendataanResepMasukController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PendataanResepMasukController(
            ApplicationDbContext applicationDbContext
        )
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult GetPendataanResepMasuks()
        {
            var PendataanResepMasuk = _applicationDbContext.PendataanResepMasuks.ToList();
            if (PendataanResepMasuk == null || !PendataanResepMasuk.Any())
            {
                return NotFound(new { message = "Belum ada data PendataanResepMasuk." });
            }
            return Ok(PendataanResepMasuk);
        }

        [HttpPost]
        public IActionResult AddPendataanResepMasuk([FromBody] PendataanResepMasukViewModel pendataanResepMasuk)
        {
            if (ModelState.IsValid)
            {
                // Mendapatkan tanggal sekarang untuk penentuan format kode unik
                var dateNow = DateTimeOffset.Now;
                var setDateNow = dateNow.ToString("yyMMdd");

                // Mencari entri terakhir berdasarkan hari ini
                var lastCode = _applicationDbContext.PendataanResepMasuks
                    .Where(d => d.CreateDateTime.Date == dateNow.Date)
                    .OrderByDescending(k => k.NoSEP)
                    .FirstOrDefault();

                // Menentukan nilai NoSEP berdasarkan entri terakhir
                if (lastCode == null)
                {
                    pendataanResepMasuk.NoSEP = "SEP" + setDateNow + "0001";
                }
                else
                {
                    var lastCodeNumber = Convert.ToInt32(lastCode.NoSEP.Substring(9));
                    pendataanResepMasuk.NoSEP = "SEP" + setDateNow + (lastCodeNumber + 1).ToString("D4");
                }

                // Membuat instance baru berdasarkan model yang diterima
                var newEntry = new PendataanResepMasuk
                {
                    PendataanResepMasukId = Guid.NewGuid(),
                    NoSEP = pendataanResepMasuk.NoSEP,
                    FaskesAsalResep = pendataanResepMasuk.FaskesAsalResep,
                    NoKartu = pendataanResepMasuk.NoKartu,
                    NmPeserta = pendataanResepMasuk.NmPeserta,
                    JnsKelamin = pendataanResepMasuk.JnsKelamin,
                    TglLahir = pendataanResepMasuk.TglLahir,
                    Pisat = pendataanResepMasuk.Pisat,
                    JnsPst = pendataanResepMasuk.JnsPst,
                    BadanUsaha = pendataanResepMasuk.BadanUsaha,
                    TglSEP = pendataanResepMasuk.TglSEP,
                    TglPulang = pendataanResepMasuk.TglPulang,
                    TkPlyn = pendataanResepMasuk.TkPlyn,
                    Diagnosa_awal = pendataanResepMasuk.Diagnosa_awal,
                    Poli = pendataanResepMasuk.Poli,
                    JnsResep = pendataanResepMasuk.JnsResep,
                    NoResep = pendataanResepMasuk.NoResep,
                    TglResep = pendataanResepMasuk.TglResep,
                    TglPelayanan = pendataanResepMasuk.TglPelayanan,
                    Pokter = pendataanResepMasuk.Pokter,
                    Iterasi = pendataanResepMasuk.Iterasi,
                    CreateDateTime = DateTimeOffset.Now,
                    CreateBy = Guid.NewGuid(),
                    UpdateDateTime = DateTimeOffset.MinValue,
                    UpdateBy = Guid.Empty,
                    DeleteDateTime = DateTimeOffset.MinValue,
                    DeleteBy = Guid.Empty
                };

                // Mengecek duplikasi data
                var isDuplicate = _applicationDbContext.PendataanResepMasuks
                    .Any(c => c.NoSEP == pendataanResepMasuk.NoSEP);

                if (!isDuplicate)
                {
                    _applicationDbContext.PendataanResepMasuks.Add(newEntry);
                    _applicationDbContext.SaveChanges();

                    return CreatedAtAction(nameof(AddPendataanResepMasuk), new { message = "Data berhasil ditambahkan!" }, newEntry);
                }
                else
                {
                    return Conflict(new { message = "Terdapat duplikasi data!" });
                }
            }

            return BadRequest(new { message = "Data tidak valid!" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePendataanResepMasuk(Guid id, [FromBody] PendataanResepMasukViewModel updatePendataanResepMasuk)
        {
            if (updatePendataanResepMasuk == null)
            {
                return BadRequest(new { message = "Data PendataanResepMasuk tidak boleh kosong." });
            }

            // Cari data berdasarkan ID
            var pendataanResepMasuk = _applicationDbContext.PendataanResepMasuks.Find(id);
            if (pendataanResepMasuk == null)
            {
                return NotFound(new { message = $"PendataanResepMasuk dengan ID {id} tidak ditemukan." });
            }

            try
            {
                // Perbarui data berdasarkan ViewModel
                pendataanResepMasuk.NoSEP = updatePendataanResepMasuk.NoSEP;
                pendataanResepMasuk.FaskesAsalResep = updatePendataanResepMasuk.FaskesAsalResep;
                pendataanResepMasuk.NoKartu = updatePendataanResepMasuk.NoKartu;
                pendataanResepMasuk.NmPeserta = updatePendataanResepMasuk.NmPeserta;
                pendataanResepMasuk.JnsKelamin = updatePendataanResepMasuk.JnsKelamin;
                pendataanResepMasuk.TglLahir = updatePendataanResepMasuk.TglLahir;
                pendataanResepMasuk.Pisat = updatePendataanResepMasuk.Pisat;
                pendataanResepMasuk.JnsPst = updatePendataanResepMasuk.JnsPst;
                pendataanResepMasuk.BadanUsaha = updatePendataanResepMasuk.BadanUsaha;
                pendataanResepMasuk.TglSEP = updatePendataanResepMasuk.TglSEP;
                pendataanResepMasuk.TglPulang = updatePendataanResepMasuk.TglPulang;
                pendataanResepMasuk.TkPlyn = updatePendataanResepMasuk.TkPlyn;
                pendataanResepMasuk.Diagnosa_awal = updatePendataanResepMasuk.Diagnosa_awal;
                pendataanResepMasuk.Poli = updatePendataanResepMasuk.Poli;
                pendataanResepMasuk.JnsResep = updatePendataanResepMasuk.JnsResep;
                pendataanResepMasuk.NoResep = updatePendataanResepMasuk.NoResep;
                pendataanResepMasuk.TglResep = updatePendataanResepMasuk.TglResep;
                pendataanResepMasuk.TglPelayanan = updatePendataanResepMasuk.TglPelayanan;
                pendataanResepMasuk.Pokter = updatePendataanResepMasuk.Pokter;
                pendataanResepMasuk.Iterasi = updatePendataanResepMasuk.Iterasi;

                // Tandai sebagai diperbarui
                pendataanResepMasuk.UpdateDateTime = DateTimeOffset.Now;
                pendataanResepMasuk.UpdateBy = Guid.NewGuid();

                // Simpan perubahan ke database
                _applicationDbContext.PendataanResepMasuks.Update(pendataanResepMasuk);
                _applicationDbContext.SaveChanges();

                return Ok(new { message = "Data berhasil diperbarui." }); // 200 OK dengan pesan sukses
            }
            catch (Exception ex)
            {
                // Tangani error jika terjadi masalah
                return StatusCode(500, new { message = $"Terjadi kesalahan saat memperbarui data: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePendataanResepMasuk(Guid id)
        {
            // Cari data berdasarkan ID
            var PendataanResepMasuk = _applicationDbContext.PendataanResepMasuks.Find(id);
            if (PendataanResepMasuk == null)
            {
                return NotFound($"PendataanResepMasuk dengan ID {id} tidak ditemukan.");
            }

            try
            {
                // Hapus entitas dari database
                _applicationDbContext.PendataanResepMasuks.Remove(PendataanResepMasuk);

                // Simpan perubahan
                _applicationDbContext.SaveChanges();

                return NoContent(); // 204 No Content jika berhasil dihapus
            }
            catch (Exception ex)
            {
                // Tangani error jika ada masalah
                return StatusCode(500, $"Terjadi kesalahan saat menghapus data: {ex.Message}");
            }
        }

        [HttpGet("paged")]
        public IActionResult GetPagedPendataanResepMasuks(int page = 1, int perPage = 2)
        {
            if (page <= 0 || perPage <= 0)
            {
                return BadRequest(new { status = "error", message = "Page and perPage must be greater than 0." });
            }

            // Total Rows
            var totalRows = _applicationDbContext.PendataanResepMasuks.Count();

            // Total Pages
            var totalPages = (int)Math.Ceiling(totalRows / (double)perPage);

            // Ambil Data Berdasarkan Pagination
            var rows = _applicationDbContext.PendataanResepMasuks
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToList();

            if (rows.Count == 0 && page > totalPages)
            {
                return NotFound(new { status = "error", message = "Page not found." });
            }

            // Buat Respons
            var response = new ApiResponse<PaginatedData<PendataanResepMasuk>>
            {
                Status = "success",
                Message = "Data retrieved successfully",
                Data = new PaginatedData<PendataanResepMasuk>
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
