using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOfficeBoyIdToProudctorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Departments_OfficeId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_OfficeId",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "OrderProducts");

            migrationBuilder.AddColumn<string>(
                name: "OfficeBoyId",
                table: "OrderProducts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OfficeBoyId",
                table: "OrderProducts",
                column: "OfficeBoyId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_AspNetUsers_OfficeBoyId",
                table: "OrderProducts",
                column: "OfficeBoyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_AspNetUsers_OfficeBoyId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_OfficeBoyId",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "OfficeBoyId",
                table: "OrderProducts");

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OfficeId",
                table: "OrderProducts",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Departments_OfficeId",
                table: "OrderProducts",
                column: "OfficeId",
                principalTable: "Departments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
