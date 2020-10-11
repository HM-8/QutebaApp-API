using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;
using System;

namespace QutebaApp_Data.Migrations
{
    public partial class main : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(unicode: false, maxLength: 45, nullable: false),
                    creation_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    role = table.Column<string>(unicode: false, maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    UID = table.Column<string>(unicode: false, maxLength: 45, nullable: false),
                    role_ID = table.Column<int>(nullable: false),
                    display_name = table.Column<string>(unicode: false, maxLength: 45, nullable: false),
                    email = table.Column<string>(unicode: false, maxLength: 45, nullable: false),
                    email_verified = table.Column<byte>(type: "tinyint(1)", nullable: true, defaultValueSql: "'0'"),
                    created_account_with = table.Column<string>(unicode: false, maxLength: 45, nullable: false),
                    creation_time = table.Column<DateTime>(nullable: false),
                    last_signin_time = table.Column<DateTime>(nullable: true),
                    last_refresh_time = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.UID);
                    table.ForeignKey(
                        name: "role_ID",
                        column: x => x.role_ID,
                        principalTable: "roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "profiles",
                columns: table => new
                {
                    user_UID = table.Column<string>(unicode: false, maxLength: 45, nullable: false),
                    photo_url = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    salary = table.Column<double>(nullable: true),
                    salary_creation_time = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.user_UID);
                    table.ForeignKey(
                        name: "user_UID",
                        column: x => x.user_UID,
                        principalTable: "accounts",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "spendings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_UID = table.Column<string>(unicode: false, maxLength: 45, nullable: false),
                    category_ID = table.Column<int>(nullable: false),
                    amount = table.Column<double>(nullable: false),
                    reason = table.Column<string>(nullable: true),
                    creation_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spendings", x => x.ID);
                    table.ForeignKey(
                        name: "category_ID",
                        column: x => x.category_ID,
                        principalTable: "categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "user_ID",
                        column: x => x.user_UID,
                        principalTable: "profiles",
                        principalColumn: "user_UID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "role_ID_idx",
                table: "accounts",
                column: "role_ID");

            migrationBuilder.CreateIndex(
                name: "category_ID_idx",
                table: "spendings",
                column: "category_ID");

            migrationBuilder.CreateIndex(
                name: "user_ID_idx",
                table: "spendings",
                column: "user_UID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "spendings");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "profiles");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
