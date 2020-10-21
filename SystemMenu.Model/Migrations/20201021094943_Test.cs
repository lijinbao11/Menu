using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemMenu.Model.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bee_manager",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bee_manager", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bee_system_menu",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pid = table.Column<long>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    icon = table.Column<string>(nullable: true),
                    href = table.Column<string>(nullable: true),
                    target = table.Column<string>(nullable: true),
                    sort = table.Column<int>(nullable: false),
                    status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bee_system_menu", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bee_login_record",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IPconfig = table.Column<string>(nullable: true),
                    COMport = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Mid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bee_login_record", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bee_login_record_bee_manager_Mid",
                        column: x => x.Mid,
                        principalTable: "bee_manager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bee_login_record_Mid",
                table: "bee_login_record",
                column: "Mid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bee_login_record");

            migrationBuilder.DropTable(
                name: "bee_system_menu");

            migrationBuilder.DropTable(
                name: "bee_manager");
        }
    }
}
