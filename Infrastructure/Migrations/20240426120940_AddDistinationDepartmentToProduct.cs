using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDistinationDepartmentToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DestiniationDepartmentId",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_DestiniationDepartmentId",
                table: "OrderProducts",
                column: "DestiniationDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OfficeId",
                table: "OrderProducts",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Departments_DestiniationDepartmentId",
                table: "OrderProducts",
                column: "DestiniationDepartmentId",
                principalTable: "Departments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Departments_OfficeId",
                table: "OrderProducts",
                column: "OfficeId",
                principalTable: "Departments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Departments_DestiniationDepartmentId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Departments_OfficeId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_DestiniationDepartmentId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_OfficeId",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "DestiniationDepartmentId",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "OrderProducts");
        }
    }
}
