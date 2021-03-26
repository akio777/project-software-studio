using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LabReservation.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Labinfo",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    tool_type = table.Column<string>(type: "TEXT", nullable: true),
                    update_by = table.Column<int>(type: "INTEGER", nullable: false),
                    created_by = table.Column<int>(type: "INTEGER", nullable: false),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    update_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labinfo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Labstore",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    lad_id = table.Column<int>(type: "INTEGER", nullable: false),
                    tool_id = table.Column<int>(type: "INTEGER", nullable: false),
                    update_by = table.Column<int>(type: "INTEGER", nullable: false),
                    created_by = table.Column<int>(type: "INTEGER", nullable: false),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    update_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labstore", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Reserve",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    store_id = table.Column<int>(type: "INTEGER", nullable: false),
                    created_by = table.Column<int>(type: "INTEGER", nullable: false),
                    start_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    end_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserve", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Toolinfo",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    type = table.Column<string>(type: "TEXT", nullable: true),
                    update_by = table.Column<int>(type: "INTEGER", nullable: false),
                    created_by = table.Column<int>(type: "INTEGER", nullable: false),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    update_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toolinfo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Userinfo",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    user = table.Column<string>(type: "TEXT", nullable: true),
                    password = table.Column<string>(type: "TEXT", nullable: true),
                    role = table.Column<string>(type: "TEXT", nullable: true),
                    update_by = table.Column<int>(type: "INTEGER", nullable: false),
                    created_by = table.Column<int>(type: "INTEGER", nullable: false),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    update_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Userinfo", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Labinfo");

            migrationBuilder.DropTable(
                name: "Labstore");

            migrationBuilder.DropTable(
                name: "Reserve");

            migrationBuilder.DropTable(
                name: "Toolinfo");

            migrationBuilder.DropTable(
                name: "Userinfo");
        }
    }
}
