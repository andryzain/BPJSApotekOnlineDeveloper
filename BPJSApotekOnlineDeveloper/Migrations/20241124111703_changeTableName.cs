using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPJSApotekOnlineDeveloper.Migrations
{
    public partial class changeTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MstUserActive",
                schema: "dbo",
                table: "MstUserActive");

            migrationBuilder.DropColumn(
                name: "Foto",
                schema: "dbo",
                table: "MstUserActive");

            migrationBuilder.RenameTable(
                name: "MstUserActive",
                schema: "dbo",
                newName: "MasterUserActive",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MasterUserActive",
                schema: "dbo",
                table: "MasterUserActive",
                column: "UserActiveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MasterUserActive",
                schema: "dbo",
                table: "MasterUserActive");

            migrationBuilder.RenameTable(
                name: "MasterUserActive",
                schema: "dbo",
                newName: "MstUserActive",
                newSchema: "dbo");

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                schema: "dbo",
                table: "MstUserActive",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MstUserActive",
                schema: "dbo",
                table: "MstUserActive",
                column: "UserActiveId");
        }
    }
}
