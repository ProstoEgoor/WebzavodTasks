using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Task_2._1._1.Migrations
{
    public partial class InitialCreat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "personal_card",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    discount = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personal_card", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "purchase",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    card_id = table.Column<long>(type: "bigint", nullable: true),
                    purchase_sum = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase", x => x.id);
                    table.ForeignKey(
                        name: "FK_purchase_personal_card_card_id",
                        column: x => x.card_id,
                        principalTable: "personal_card",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_profile",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    email = table.Column<string>(type: "varchar(50)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    birthdate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_profile", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_user_profile_personal_card_user_id",
                        column: x => x.user_id,
                        principalTable: "personal_card",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_purchase_card_id",
                table: "purchase",
                column: "card_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "purchase");

            migrationBuilder.DropTable(
                name: "user_profile");

            migrationBuilder.DropTable(
                name: "personal_card");
        }
    }
}
