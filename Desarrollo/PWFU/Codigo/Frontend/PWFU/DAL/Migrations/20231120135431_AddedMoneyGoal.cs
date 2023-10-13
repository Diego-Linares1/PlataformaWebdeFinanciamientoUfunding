using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PWFU.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoneyGoal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Goal",
                schema: "dbo",
                table: "projects",
                newName: "ProjectGoal");

            migrationBuilder.AddColumn<float>(
                name: "MoneyGoal",
                schema: "dbo",
                table: "projects",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoneyGoal",
                schema: "dbo",
                table: "projects");

            migrationBuilder.RenameColumn(
                name: "ProjectGoal",
                schema: "dbo",
                table: "projects",
                newName: "Goal");
        }
    }
}
