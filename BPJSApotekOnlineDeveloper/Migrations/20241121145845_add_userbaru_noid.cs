using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPJSApotekOnlineDeveloper.Migrations
{
    public partial class add_userbaru_noid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "dbo",
                table: "MstUserActive");

            migrationBuilder.DropColumn(
                name: "PositionId",
                schema: "dbo",
                table: "MstUserActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                schema: "dbo",
                table: "MstUserActive",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PositionId",
                schema: "dbo",
                table: "MstUserActive",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
