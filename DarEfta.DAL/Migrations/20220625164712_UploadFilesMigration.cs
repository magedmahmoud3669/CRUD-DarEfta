using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DarEfta.DAL.Migrations
{
    public partial class UploadFilesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CvName",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CvName",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Employee");
        }
    }
}
