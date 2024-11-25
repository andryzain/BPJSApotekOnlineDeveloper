using BPJSApotekOnlineDeveloper.Areas.MasterData.Models;
using BPJSApotekOnlineDeveloper.Areas.MasterData.ViewModels;
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

    public class ResepMasukController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ResepMasukController(
            ApplicationDbContext applicationDbContext
        )
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult GetResepMasuks()
        {
            var ResepMasuk = _applicationDbContext.ResepMasuks.ToList();
            if (ResepMasuk == null || !ResepMasuk.Any())
            {
                return NotFound(new { message = "Belum ada data ResepMasuk." });
            }
            return Ok(ResepMasuk);
        }

        [HttpPost]
        public IActionResult AddResepMasuk([FromBody] ResepMasukViewModel ResepMasuk)
        {
            if (ModelState.IsValid)
            {
                // Mendapatkan tanggal sekarang untuk penentuan format kode unik
                var dateNow = DateTimeOffset.Now;
                var setDateNow = dateNow.ToString("yyMMdd");

                // Mencari entri terakhir berdasarkan hari ini
                var lastCode = _applicationDbContext.ResepMasuks
                    .Where(d => d.CreateDateTime.Date == dateNow.Date)
                    .OrderByDescending(k => k.NoSEP)
                    .FirstOrDefault();

                // Menentukan nilai NoSEP berdasarkan entri terakhir
                if (lastCode == null)
                {
                    ResepMasuk.NoSEP = "SEP" + setDateNow + "0001";
                }
                else
                {
                    var lastCodeNumber = Convert.ToInt32(lastCode.NoSEP.Substring(9));
                    ResepMasuk.NoSEP = "SEP" + setDateNow + (lastCodeNumber + 1).ToString("D4");
                }

                // Membuat instance baru berdasarkan model yang diterima
                var newEntry = new ResepMasuk
                {
                    ResepMasukId = Guid.NewGuid(),
                    TglEntry = ResepMasuk.TglEntry,
                    NoSEP = ResepMasuk.NoSEP,
                    FaskesAsalResep = ResepMasuk.FaskesAsalResep,
                    NoKartu = ResepMasuk.NoKartu,
                    NmPeserta = ResepMasuk.NmPeserta,
                    JnsKelamin = ResepMasuk.JnsKelamin,
                    TglLahir = ResepMasuk.TglLahir,
                    Pisat = ResepMasuk.Pisat,
                    JnsPst = ResepMasuk.JnsPst,
                    BadanUsaha = ResepMasuk.BadanUsaha,
                    TglSEP = ResepMasuk.TglSEP,
                    TglPulang = ResepMasuk.TglPulang,
                    TkPlyn = ResepMasuk.TkPlyn,
                    Diagnosa_awal = ResepMasuk.Diagnosa_awal,
                    Poli = ResepMasuk.Poli,
                    JnsResep = ResepMasuk.JnsResep,
                    NoResep = ResepMasuk.NoResep,
                    TglResep = ResepMasuk.TglResep,
                    TglPelayanan = ResepMasuk.TglPelayanan,
                    Pokter = ResepMasuk.Pokter,
                    CreateDateTime = DateTimeOffset.Now,
                    CreateBy = Guid.NewGuid(),
                    UpdateDateTime = DateTimeOffset.MinValue,
                    UpdateBy = Guid.Empty,
                    DeleteDateTime = DateTimeOffset.MinValue,
                    DeleteBy = Guid.Empty
                };

                // Mengecek duplikasi data
                var isDuplicate = _applicationDbContext.ResepMasuks
                    .Any(c => c.NoSEP == ResepMasuk.NoSEP);

                if (!isDuplicate)
                {
                    _applicationDbContext.ResepMasuks.Add(newEntry);
                    _applicationDbContext.SaveChanges();

                    return CreatedAtAction(nameof(AddResepMasuk), new { message = "Data berhasil ditambahkan!" }, newEntry);
                }
                else
                {
                    return Conflict(new { message = "Terdapat duplikasi data!" });
                }
            }

            return BadRequest(new { message = "Data tidak valid!" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateResepMasuk(Guid id, [FromBody] ResepMasukViewModel updateResepMasuk)
        {
            if (updateResepMasuk == null)
            {
                return BadRequest(new { message = "Data ResepMasuk tidak boleh kosong." });
            }

            // Cari data berdasarkan ID
            var ResepMasuk = _applicationDbContext.ResepMasuks.Find(id);
            if (ResepMasuk == null)
            {
                return NotFound(new { message = $"ResepMasuk dengan ID {id} tidak ditemukan." });
            }

            try
            {
                // Perbarui data berdasarkan ViewModel
                ResepMasuk.TglEntry = updateResepMasuk.TglEntry;
                ResepMasuk.NoSEP = updateResepMasuk.NoSEP;
                ResepMasuk.FaskesAsalResep = updateResepMasuk.FaskesAsalResep;
                ResepMasuk.NoKartu = updateResepMasuk.NoKartu;
                ResepMasuk.NmPeserta = updateResepMasuk.NmPeserta;
                ResepMasuk.JnsKelamin = updateResepMasuk.JnsKelamin;
                ResepMasuk.TglLahir = updateResepMasuk.TglLahir;
                ResepMasuk.Pisat = updateResepMasuk.Pisat;
                ResepMasuk.JnsPst = updateResepMasuk.JnsPst;
                ResepMasuk.BadanUsaha = updateResepMasuk.BadanUsaha;
                ResepMasuk.TglSEP = updateResepMasuk.TglSEP;
                ResepMasuk.TglPulang = updateResepMasuk.TglPulang;
                ResepMasuk.TkPlyn = updateResepMasuk.TkPlyn;
                ResepMasuk.Diagnosa_awal = updateResepMasuk.Diagnosa_awal;
                ResepMasuk.Poli = updateResepMasuk.Poli;
                ResepMasuk.JnsResep = updateResepMasuk.JnsResep;
                ResepMasuk.NoResep = updateResepMasuk.NoResep;
                ResepMasuk.TglResep = updateResepMasuk.TglResep;
                ResepMasuk.TglPelayanan = updateResepMasuk.TglPelayanan;
                ResepMasuk.Pokter = updateResepMasuk.Pokter;

                // Tandai sebagai diperbarui
                ResepMasuk.UpdateDateTime = DateTimeOffset.Now;
                ResepMasuk.UpdateBy = Guid.NewGuid();

                // Simpan perubahan ke database
                _applicationDbContext.ResepMasuks.Update(ResepMasuk);
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
        public IActionResult DeleteResepMasuk(Guid id)
        {
            // Cari data berdasarkan ID
            var ResepMasuk = _applicationDbContext.ResepMasuks.Find(id);
            if (ResepMasuk == null)
            {
                return NotFound($"ResepMasuk dengan ID {id} tidak ditemukan.");
            }

            try
            {
                // Hapus entitas dari database
                _applicationDbContext.ResepMasuks.Remove(ResepMasuk);

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
    }
}
