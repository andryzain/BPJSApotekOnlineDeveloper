using System.ComponentModel.DataAnnotations;

namespace BPJSApotekOnlineDeveloper.Areas.MasterData.ViewModels
{
    public class PendataanResepMasukViewModel
    {
        public string NoSEP { get; set; } = ""; // Input Pencarian
        //[Required(ErrorMessage = "Fullname is required !")]
        public string FaskesAsalResep { get; set; } = "";
        public string NoKartu { get; set; } = "";
        public string NmPeserta { get; set; } = "";
        public string JnsKelamin { get; set; } = "";
        public string TglLahir { get; set; } = "";
        public string Pisat { get; set; } = "";
        public string JnsPst { get; set; } = "";
        public string BadanUsaha { get; set; } = "";
        public string TglSEP { get; set; } = "";
        public string TglPulang { get; set; } = "";
        public string TkPlyn { get; set; } = "";
        public string Diagnosa_awal { get; set; } = "";
        public string Poli { get; set; } = "";
        public string JnsResep { get; set; } = "";
        public string NoResep { get; set; } = "";
        public string TglResep { get; set; } = "";
        public string TglPelayanan { get; set; } = "";
        public string Pokter { get; set; } = "";
        public string Iterasi { get; set; } = "";
    }
}
