using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPJSApotekOnlineDeveloper.Migrations
{
    public partial class initializePendaftaran : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "MasterPendaftaran",
                schema: "dbo",
                columns: table => new
                {
                    PendaftaranId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoKartuBpjs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NamaLengkapPeserta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JenisKelamin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TanggalLahir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlamatTinggal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoTelepon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoIdentitasKTPSIM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusKepesertaanBpjs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipeKepesertaanBpjs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TanggalPendaftaranBpjs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NamaKeluarga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NamaApotek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusVerifikasi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InformasiObat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCancel = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterPendaftaran", x => x.PendaftaranId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterPendaftaran",
                schema: "dbo");
        }
    }
}
