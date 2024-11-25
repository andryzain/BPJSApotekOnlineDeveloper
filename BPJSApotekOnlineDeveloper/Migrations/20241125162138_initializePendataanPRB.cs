using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPJSApotekOnlineDeveloper.Migrations
{
    public partial class initializePendataanPRB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MstResepMasuk",
                schema: "dbo",
                table: "MstResepMasuk");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MstPendataanResepMasuk",
                schema: "dbo",
                table: "MstPendataanResepMasuk");

            migrationBuilder.RenameTable(
                name: "MstResepMasuk",
                schema: "dbo",
                newName: "MasterResepMasuk",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "MstPendataanResepMasuk",
                schema: "dbo",
                newName: "MasterPendataanResepMasuk",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MasterResepMasuk",
                schema: "dbo",
                table: "MasterResepMasuk",
                column: "ResepMasukId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MasterPendataanResepMasuk",
                schema: "dbo",
                table: "MasterPendataanResepMasuk",
                column: "PendataanResepMasukId");

            migrationBuilder.CreateTable(
                name: "MasterPendataanPRBMTM",
                schema: "dbo",
                columns: table => new
                {
                    PendataanPrbMtmId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TglInput = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoKunjunganPcare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoHandphone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoResep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoKa_Nama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FktpTerdaftar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FktrlPerujukBalik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JenisKelamin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TglLahir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diagnosa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dpjp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VitalSign = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeratBadan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sistole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diastole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiwayatAlergiObat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiwayatEso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiwayatMerokok = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiwayatPenggunaanObatTambahan_Alternatif = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indikasi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Efektivitas = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_MasterPendataanPRBMTM", x => x.PendataanPrbMtmId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterPendataanPRBMTM",
                schema: "dbo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MasterResepMasuk",
                schema: "dbo",
                table: "MasterResepMasuk");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MasterPendataanResepMasuk",
                schema: "dbo",
                table: "MasterPendataanResepMasuk");

            migrationBuilder.RenameTable(
                name: "MasterResepMasuk",
                schema: "dbo",
                newName: "MstResepMasuk",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "MasterPendataanResepMasuk",
                schema: "dbo",
                newName: "MstPendataanResepMasuk",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MstResepMasuk",
                schema: "dbo",
                table: "MstResepMasuk",
                column: "ResepMasukId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MstPendataanResepMasuk",
                schema: "dbo",
                table: "MstPendataanResepMasuk",
                column: "PendataanResepMasukId");
        }
    }
}
