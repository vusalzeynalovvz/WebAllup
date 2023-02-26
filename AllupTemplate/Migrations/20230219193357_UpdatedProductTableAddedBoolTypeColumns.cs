using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllupTemplate.Migrations
{
    public partial class UpdatedProductTableAddedBoolTypeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MainTite",
                table: "Sliders",
                newName: "MainTitle");

            migrationBuilder.AddColumn<bool>(
                name: "IsArrival",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBestSeller",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArrival",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsBestSeller",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "MainTitle",
                table: "Sliders",
                newName: "MainTite");
        }
    }
}
