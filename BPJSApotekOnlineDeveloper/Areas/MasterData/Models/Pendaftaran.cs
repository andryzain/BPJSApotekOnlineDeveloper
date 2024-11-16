using BPJSApotekOnlineDeveloper.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPJSApotekOnlineDeveloper.Areas.MasterData.Models
{
    [Table("MasterPendaftaran", Schema = "dbo")]
    public class Pendaftaran : UserActivity
    {
        [Key]
        public Guid PendaftaranId { get; set; } = Guid.NewGuid(); // Memberikan nilai unik otomatis
        public string NoKartuBpjs { get; set; } = "Generate Otomatis (Jgn Diisi)";
        public string NamaLengkapPeserta { get; set; } = "";
        public string JenisKelamin { get; set; } = "";
        public string TanggalLahir { get; set; } = "";
        public string AlamatTinggal { get; set; } = "";
        public string NoTelepon { get; set; } = "";
        public string Email { get; set; } = "";
        public string NoIdentitasKTPSIM { get; set; } = "";
        public string StatusKepesertaanBpjs { get; set; } = "";
        public string TipeKepesertaanBpjs { get; set; } = "";
        public string TanggalPendaftaranBpjs { get; set; } = "";
        public string NamaKeluarga { get; set; } = "";
        public string NamaApotek { get; set; } = "";
        public string StatusVerifikasi { get; set; } = "";
        public string InformasiObat { get; set; } = "";
    }
}
