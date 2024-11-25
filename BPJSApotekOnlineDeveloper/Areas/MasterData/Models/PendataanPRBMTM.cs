using BPJSApotekOnlineDeveloper.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPJSApotekOnlineDeveloper.Areas.MasterData.Models
{
    [Table("MasterPendataanPRBMTM", Schema = "dbo")]
    public class PendataanPRBMTM : UserActivity
    {
        [Key]
        public Guid PendataanPrbMtmId { get; set; } = Guid.NewGuid();
        public string TglInput { get; set; } = "";
        public string NoKunjunganPcare { get; set; } = "";
        public string NoHandphone { get; set; } = "";
        public string NoResep { get; set; } = "";
        public string NoKa_Nama { get; set; } = "";
        public string FktpTerdaftar { get; set; } = "";
        public string FktrlPerujukBalik { get; set; } = "";
        public string JenisKelamin { get; set; } = "";
        public string TglLahir { get; set; } = "";
        public string Diagnosa { get; set; } = "";
        public string Dpjp { get; set; } = "";
        public string VitalSign { get; set; } = "";
        public string BeratBadan { get; set; } = "";
        public string Sistole { get; set; } = "";
        public string Diastole { get; set; } = "";
        public string RiwayatAlergiObat { get; set; } = "";
        public string RiwayatEso { get; set; } = "";
        public string RiwayatMerokok { get; set; } = "";
        public string RiwayatPenggunaanObatTambahan_Alternatif { get; set; } = "";
        public string Indikasi { get; set; } = "";
        public string Efektivitas { get; set; } = "";
    }
}
