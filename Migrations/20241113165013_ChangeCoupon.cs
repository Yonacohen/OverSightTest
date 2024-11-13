using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OverSightTest.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCoupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCreatorId",
                table: "Coupons");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Coupons",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Coupons");

            migrationBuilder.AddColumn<Guid>(
                name: "UserCreatorId",
                table: "Coupons",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");
        }
    }
}
