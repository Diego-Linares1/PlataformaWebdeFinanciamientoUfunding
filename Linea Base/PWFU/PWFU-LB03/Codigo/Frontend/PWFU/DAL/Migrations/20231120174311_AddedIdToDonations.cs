using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PWFU.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedIdToDonations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_donations",
                schema: "dbo",
                table: "donations");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "dbo",
                table: "donations",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_donations",
                schema: "dbo",
                table: "donations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_donations_UserId",
                schema: "dbo",
                table: "donations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_donations",
                schema: "dbo",
                table: "donations");

            migrationBuilder.DropIndex(
                name: "IX_donations_UserId",
                schema: "dbo",
                table: "donations");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "donations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_donations",
                schema: "dbo",
                table: "donations",
                columns: new[] { "UserId", "ProjectId" });
        }
    }
}
