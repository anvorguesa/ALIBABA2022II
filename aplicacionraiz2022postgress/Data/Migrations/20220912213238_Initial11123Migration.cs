using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aplicacionraiz2022postgress.Data.Migrations
{
    public partial class Initial11123Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_t_Contacto",
                table: "t_Contacto");

            migrationBuilder.RenameTable(
                name: "t_Contacto",
                newName: "t_contacto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_contacto",
                table: "t_contacto",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_t_contacto",
                table: "t_contacto");

            migrationBuilder.RenameTable(
                name: "t_contacto",
                newName: "t_Contacto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_Contacto",
                table: "t_Contacto",
                column: "id");
        }
    }
}
