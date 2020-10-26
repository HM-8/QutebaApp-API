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
                    role_uID = table.Column<int>(nullable: false),
                    created_account_with = table.Column<string>(maxLength: 45, nullable: false),
                    user_creation_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.ID);
                    table.ForeignKey(
                        name: "role_uid",
                        column: x => x.role_uID,
                        principalTable: "roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "profiles",
                columns: table => new
                {
                    user_pID = table.Column<int>(nullable: false),
                    username = table.Column<string>(maxLength: 45, nullable: false),
                    photo_url = table.Column<string>(maxLength: 255, nullable: true),
                    profile_creation_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.user_pID);
                    table.ForeignKey(
                        name: "user_pid",
                        column: x => x.user_pID,
                        principalTable: "users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_cID = table.Column<int>(nullable: false),
                    category_name = table.Column<string>(maxLength: 45, nullable: false),
                    category_type = table.Column<string>(maxLength: 45, nullable: false),
                    category_creation_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.ID);
                    table.ForeignKey(
                        name: "user_id",
                        column: x => x.user_cID,
                        principalTable: "profiles",
                        principalColumn: "user_pID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "incomes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_iID = table.Column<int>(nullable: false),
                    income_category_ID = table.Column<int>(nullable: false),
                    income_amount = table.Column<double>(nullable: false),
                    income_creation_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_incomes", x => x.ID);
                    table.ForeignKey(
                        name: "category_iid",
                        column: x => x.income_category_ID,
                        principalTable: "categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "user_iid",
                        column: x => x.user_iID,
                        principalTable: "profiles",
                        principalColumn: "user_pID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "spendings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_sID = table.Column<int>(nullable: false),
                    spending_category_ID = table.Column<int>(nullable: false),
                    spending_amount = table.Column<double>(nullable: false),
                    reason = table.Column<string>(nullable: true),
                    spending_creation_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spendings", x => x.ID);
                    table.ForeignKey(
                        name: "category_sid",
                        column: x => x.spending_category_ID,
                        principalTable: "categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "user_sid",
                        column: x => x.user_sID,
                        principalTable: "profiles",
                        principalColumn: "user_pID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "user_ID_idx",
                table: "categories",
                column: "user_cID");

            migrationBuilder.CreateIndex(
                name: "category_iid_idx",
                table: "incomes",
                column: "income_category_ID");

            migrationBuilder.CreateIndex(
                name: "user_iid_idx",
                table: "incomes",
                column: "user_iID");

            migrationBuilder.CreateIndex(
                name: "user_ID_UNIQUE",
                table: "profiles",
                column: "user_pID",
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
                column: "spending_category_ID");

            migrationBuilder.CreateIndex(
                name: "user_id_idx",
                table: "spendings",
                column: "user_sID");

            migrationBuilder.CreateIndex(
                name: "email_UNIQUE",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "role_ID_idx",
                table: "users",
                column: "role_uID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "incomes");

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
