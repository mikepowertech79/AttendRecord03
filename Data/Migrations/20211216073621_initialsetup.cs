using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendRecord03.Data.Migrations
{
    public partial class initialsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Record",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonEmail = table.Column<string>(nullable: true),
                    PersonName = table.Column<string>(nullable: true),
                    AbsenceType = table.Column<string>(nullable: true),
                    AbsenceTimeStart = table.Column<DateTime>(nullable: false),
                    AbsenceTimeEnd = table.Column<DateTime>(nullable: false),
                    AbsenceHours = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Record", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Record");
        }
    }
}
