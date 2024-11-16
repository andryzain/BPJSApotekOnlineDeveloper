using BPJSApotekOnlineDeveloper.Areas.MasterData.Models;
using BPJSApotekOnlineDeveloper.Areas.MasterData.ViewModels;
using BPJSApotekOnlineDeveloper.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BPJSApotekOnlineDeveloper.Areas.MasterData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Endpoint ini memerlukan token
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
                return NotFound(new { message = "Belum ada data pendaftaran." });
            }
            return Ok(pendaftaran);
        }

        [HttpPost]
        public IActionResult AddPendaftaran([FromBody] PendaftaranViewModel pendaftaran)
        {
            //    _applicationDbContext.Pendaftarans.Add(pendaftaran);
            //    _applicationDbContext.SaveChanges();
            //    return CreatedAtAction(nameof(GetPendaftarans), new { id = pendaftaran.PendaftaranId }, pendaftaran);

            var dateNow = DateTimeOffset.Now;
            var day = dateNow.Day;
            var month = dateNow.Month;
            var year = dateNow.Year;

            var setDateNow = DateTimeOffset.Now.ToString("yyMMdd");

            var lastCode = _applicationDbContext.Pendaftarans.Where(d => d.CreateDateTime.Day == day && d.CreateDateTime.Month == month && d.CreateDateTime.Year == year).OrderByDescending(k => k.NoKartuBpjs).FirstOrDefault();
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
                    pendaftaran.NoKartuBpjs = "CRD" + setDateNow + (Convert.ToInt32(lastCode.NoKartuBpjs.Substring(9, lastCode.NoKartuBpjs.Length - 9)) + 1).ToString("D4");
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
                        return CreatedAtAction(nameof(GetPendaftarans), new { message = "Tambah Data Sukses" }, pendaftaran);
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
        public IActionResult UpdatePendaftaran(int id, [FromBody] Pendaftaran updatePendaftaran)
        {
            var pendaftaran = _applicationDbContext.Pendaftarans.Find(id);
            if (pendaftaran == null) return NotFound();

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

            _applicationDbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePendaftaran(int id)
        {
            var pendaftaran = _applicationDbContext.Pendaftarans.Find(id);
            if (pendaftaran == null) return NotFound();

            _applicationDbContext.Pendaftarans.Remove(pendaftaran);
            _applicationDbContext.SaveChanges();
            return NoContent();
        }
    }
}
