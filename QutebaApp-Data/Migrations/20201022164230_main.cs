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
                    category_name = table.Column<string>(maxLength: 45, nullable: false),
                    category_creation_time = table.Column<DateTime>(nullable: false)
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
                    role_name = table.Column<string>(maxLength: 45, nullable: false),
                    role_creation_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(maxLength: 45, nullable: false),
                    password = table.Column<string>(maxLength: 45, nullable: false),
                    fullname = table.Column<string>(maxLength: 45, nullable: false),
                    role_ID = table.Column<int>(nullable: false),
                    created_account_with = table.Column<string>(maxLength: 45, nullable: false),
                    user_creation_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.ID);
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
                    user_ID = table.Column<int>(nullable: false),
                    username = table.Column<string>(maxLength: 45, nullable: false),
                    photo_url = table.Column<string>(maxLength: 255, nullable: true),
                    income = table.Column<double>(nullable: true),
                    income_creation_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.user_ID);
                    table.ForeignKey(
                        name: "user_id",
                        column: x => x.user_ID,
                        principalTable: "users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "spendings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_ID = table.Column<int>(nullable: false),
                    category_ID = table.Column<int>(nullable: false),
                    amount = table.Column<double>(nullable: false),
                    reason = table.Column<string>(nullable: true),
                    spending_creation_time = table.Column<DateTime>(nullable: false)
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
                        name: "user_s_id",
                        column: x => x.user_ID,
                        principalTable: "profiles",
                        principalColumn: "user_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "name_UNIQUE",
                table: "categories",
                column: "category_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "user_ID_UNIQUE",
                table: "profiles",
                column: "user_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "username_UNIQUE",
                table: "profiles",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "role_name_UNIQUE",
                table: "roles",
                column: "role_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "category_ID_idx",
                table: "spendings",
                column: "category_ID");

            migrationBuilder.CreateIndex(
                name: "user_id_idx",
                table: "spendings",
                column: "user_ID");

            migrationBuilder.CreateIndex(
                name: "email_UNIQUE",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "role_ID_idx",
                table: "users",
                column: "role_ID");
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
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
