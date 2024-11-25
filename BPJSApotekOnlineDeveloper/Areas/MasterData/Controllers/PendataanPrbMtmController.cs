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
    public class PendataanPrbMtmController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;


        public PendataanPrbMtmController(
            ApplicationDbContext applicationDbContext
        )
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult GetPendataanPrbMtms()
        {
            var pendataanPrb = _applicationDbContext.PendataanPRBMTMs.ToList();
            if (pendataanPrb == null || !pendataanPrb.Any())
            {
                return NotFound(new { message = "Belum ada data pendataan PRB. || 404 Not Found" });
            }
            return Ok(new { message = "Berhasil || 200 OK" });
        }

        [HttpPost]
        public IActionResult AddPendataanPrbMtms([FromBody] PendataanPRBMTMViewModel pendataanPRBMTM)
        {           
            if (ModelState.IsValid)
            {
                var pendataan = new PendataanPRBMTM
                {
                    CreateDateTime = DateTimeOffset.Now,
                    CreateBy = Guid.NewGuid(),
                    UpdateDateTime = DateTimeOffset.MinValue,
                    UpdateBy = new Guid("00000000-0000-0000-0000-000000000000"),
                    DeleteDateTime = DateTimeOffset.MinValue,
                    DeleteBy = new Guid("00000000-0000-0000-0000-000000000000"),
                    PendataanPrbMtmId = Guid.NewGuid(),
                    TglInput = pendataanPRBMTM.TglInput,
                    NoKunjunganPcare = pendataanPRBMTM.NoKunjunganPcare,
                    NoHandphone = pendataanPRBMTM.NoHandphone,
                    NoResep = pendataanPRBMTM.NoResep,
                    NoKa_Nama = pendataanPRBMTM.NoKa_Nama,
                    FktpTerdaftar = pendataanPRBMTM.FktpTerdaftar,
                    FktrlPerujukBalik = pendataanPRBMTM.FktrlPerujukBalik,
                    JenisKelamin = pendataanPRBMTM.JenisKelamin,
                    TglLahir = pendataanPRBMTM.TglLahir,
                    Diagnosa = pendataanPRBMTM.Diagnosa,
                    Dpjp = pendataanPRBMTM.Dpjp,
                    VitalSign = pendataanPRBMTM.VitalSign,
                    BeratBadan = pendataanPRBMTM.BeratBadan,
                    Sistole = pendataanPRBMTM.Sistole,
                    Diastole = pendataanPRBMTM.Diastole,
                    RiwayatAlergiObat = pendataanPRBMTM.RiwayatAlergiObat,
                    RiwayatEso = pendataanPRBMTM.RiwayatEso,
                    RiwayatMerokok = pendataanPRBMTM.RiwayatMerokok,
                    RiwayatPenggunaanObatTambahan_Alternatif = pendataanPRBMTM.RiwayatPenggunaanObatTambahan_Alternatif,
                    Indikasi = pendataanPRBMTM.Indikasi,
                    Efektivitas = pendataanPRBMTM.Efektivitas,
                };

                var checkDuplicate = _applicationDbContext.PendataanPRBMTMs.Where(c => c.NoKunjunganPcare == pendataanPRBMTM.NoKunjunganPcare).ToList();

                if (checkDuplicate.Count == 0)
                {
                    var result = _applicationDbContext.PendataanPRBMTMs.Where(c => c.NoKunjunganPcare == pendataanPRBMTM.NoKunjunganPcare).FirstOrDefault();
                    if (result == null)
                    {
                        _applicationDbContext.PendataanPRBMTMs.Add(pendataan);
                        _applicationDbContext.SaveChanges();
                        return CreatedAtAction(nameof(GetPendataanPrbMtms), new { message = "Tambah Data Sukses || 201 Created" }, pendataan);
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
        public IActionResult UpdatePendataanPrbMtm(Guid id, [FromBody] PendataanPRBMTM updatePendataanPRBMTM)
        {
            if (updatePendataanPRBMTM == null)
            {
                return BadRequest("Data pendataan tidak boleh kosong. || 400 Bad Request");
            }

            // Cari data berdasarkan ID
            var pendataan = _applicationDbContext.PendataanPRBMTMs.Find(id);
            if (pendataan == null)
            {
                return NotFound($"Pendataan dengan ID {id} tidak ditemukan. || 404 Not Found");
            }

            try
            {
                // Perbarui data Pendataan PRB
                pendataan.TglInput = updatePendataanPRBMTM.TglInput;
                pendataan.NoKunjunganPcare = updatePendataanPRBMTM.NoKunjunganPcare;
                pendataan.NoHandphone = updatePendataanPRBMTM.NoHandphone;
                pendataan.NoResep = updatePendataanPRBMTM.NoResep;
                pendataan.NoKa_Nama = updatePendataanPRBMTM.NoKa_Nama;
                pendataan.FktpTerdaftar = updatePendataanPRBMTM.FktpTerdaftar;
                pendataan.FktrlPerujukBalik = updatePendataanPRBMTM.FktrlPerujukBalik;
                pendataan.JenisKelamin = updatePendataanPRBMTM.JenisKelamin;
                pendataan.TglLahir = updatePendataanPRBMTM.TglLahir;
                pendataan.Diagnosa = updatePendataanPRBMTM.Diagnosa;
                pendataan.Dpjp = updatePendataanPRBMTM.Dpjp;
                pendataan.VitalSign = updatePendataanPRBMTM.VitalSign;
                pendataan.BeratBadan = updatePendataanPRBMTM.BeratBadan;
                pendataan.Sistole = updatePendataanPRBMTM.Sistole;
                pendataan.Diastole = updatePendataanPRBMTM.Diastole;
                pendataan.RiwayatAlergiObat = updatePendataanPRBMTM.RiwayatAlergiObat;
                pendataan.RiwayatEso = updatePendataanPRBMTM.RiwayatEso;
                pendataan.RiwayatMerokok = updatePendataanPRBMTM.RiwayatMerokok;
                pendataan.RiwayatPenggunaanObatTambahan_Alternatif = updatePendataanPRBMTM.RiwayatPenggunaanObatTambahan_Alternatif;
                pendataan.Indikasi = updatePendataanPRBMTM.Indikasi;
                pendataan.Efektivitas = updatePendataanPRBMTM.Efektivitas;

                // Tandai data sebagai telah diubah
                _applicationDbContext.PendataanPRBMTMs.Update(pendataan);

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
        public IActionResult DeletePendataanPrbMtm(Guid id)
        {
            // Cari data berdasarkan ID
            var pendataan = _applicationDbContext.PendataanPRBMTMs.Find(id);
            if (pendataan == null)
            {
                return NotFound($"Pendataan dengan ID {id} tidak ditemukan. || 404 Not Found");
            }

            try
            {
                // Hapus entitas dari database
                _applicationDbContext.PendataanPRBMTMs.Remove(pendataan);

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
        public IActionResult GetPagedPendaftaranPendataanPrbMtms(int page = 1, int perPage = 2)
        {
            if (page <= 0 || perPage <= 0)
            {
                return BadRequest(new { status = "error", message = "Page and perPage must be greater than 0." });
            }

            // Total Rows
            var totalRows = _applicationDbContext.PendataanPRBMTMs.Count();

            // Total Pages
            var totalPages = (int)Math.Ceiling(totalRows / (double)perPage);

            // Ambil Data Berdasarkan Pagination
            var rows = _applicationDbContext.PendataanPRBMTMs
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToList();

            if (rows.Count == 0 && page > totalPages)
            {
                return NotFound(new { status = "error", message = "Page not found." });
            }

            // Buat Respons
            var response = new ApiResponse<PaginatedData<PendataanPRBMTM>>
            {
                Status = "success",
                Message = "Data retrieved successfully",
                Data = new PaginatedData<PendataanPRBMTM>
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
