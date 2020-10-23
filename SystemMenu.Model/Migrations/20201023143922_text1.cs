using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemMenu.Model.Migrations
{
    public partial class text1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "bee_system_menu",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "target",
                table: "bee_system_menu",
                newName: "Target");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "bee_system_menu",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "sort",
                table: "bee_system_menu",
                newName: "Sort");

            migrationBuilder.RenameColumn(
                name: "pid",
                table: "bee_system_menu",
                newName: "Pid");

            migrationBuilder.RenameColumn(
                name: "icon",
                table: "bee_system_menu",
                newName: "Icon");

            migrationBuilder.RenameColumn(
                name: "href",
                table: "bee_system_menu",
                newName: "Href");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "bee_system_menu",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "bee_system_menu",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Target",
                table: "bee_system_menu",
                newName: "target");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "bee_system_menu",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Sort",
                table: "bee_system_menu",
                newName: "sort");

            migrationBuilder.RenameColumn(
                name: "Pid",
                table: "bee_system_menu",
                newName: "pid");

            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "bee_system_menu",
                newName: "icon");

            migrationBuilder.RenameColumn(
                name: "Href",
                table: "bee_system_menu",
                newName: "href");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "bee_system_menu",
                newName: "id");
        }
    }
}
