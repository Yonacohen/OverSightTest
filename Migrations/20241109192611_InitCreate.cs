using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OverSightTest.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserCreatorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreationDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Discount = table.Column<float>(type: "float", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DoublePromotions = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsLimited = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LimitedUseNum = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");
        }
    }
}
