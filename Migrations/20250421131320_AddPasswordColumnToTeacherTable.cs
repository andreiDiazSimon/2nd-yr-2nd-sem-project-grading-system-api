using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grading_system_api.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordColumnToTeacherTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Teachers");
        }
    }
}
