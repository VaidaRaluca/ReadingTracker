using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadingTracker.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserAbilitiesToReader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Readers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Readers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Readers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Readers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Readers");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Readers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Readers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Readers");
        }
    }
}
