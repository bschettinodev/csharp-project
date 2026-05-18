using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(
                        type: "character varying(100)",
                        maxLength: 100,
                        nullable: false
                    ),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    InitialBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(
                        type: "character varying(100)",
                        maxLength: 100,
                        nullable: false
                    ),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Color = table.Column<string>(
                        type: "character varying(7)",
                        maxLength: 7,
                        nullable: false
                    ),
                    Icon = table.Column<string>(
                        type: "character varying(50)",
                        maxLength: 50,
                        nullable: true
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(
                        type: "character varying(150)",
                        maxLength: 150,
                        nullable: false
                    ),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Date = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Notes = table.Column<string>(
                        type: "character varying(500)",
                        maxLength: 500,
                        nullable: true
                    ),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    UpdatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                }
            );

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "Icon", "Name", "Type" },
                values: new object[,]
                {
                    {
                        new Guid("11111111-1111-4111-8111-111111111111"),
                        "#55E27A",
                        "salary",
                        "Salary",
                        1,
                    },
                    {
                        new Guid("11111111-1111-4111-8111-111111111112"),
                        "#6EE7B7",
                        "freelance",
                        "Freelance",
                        1,
                    },
                    {
                        new Guid("11111111-1111-4111-8111-111111111113"),
                        "#A3E635",
                        "investment",
                        "Investments",
                        1,
                    },
                    {
                        new Guid("11111111-1111-4111-8111-111111111114"),
                        "#22C55E",
                        "refund",
                        "Refund",
                        1,
                    },
                    {
                        new Guid("22222222-2222-4222-8222-222222222221"),
                        "#F97316",
                        "food",
                        "Food",
                        2,
                    },
                    {
                        new Guid("22222222-2222-4222-8222-222222222222"),
                        "#38BDF8",
                        "transport",
                        "Transport",
                        2,
                    },
                    {
                        new Guid("22222222-2222-4222-8222-222222222223"),
                        "#F472B6",
                        "shopping",
                        "Shopping",
                        2,
                    },
                    {
                        new Guid("22222222-2222-4222-8222-222222222224"),
                        "#A78BFA",
                        "housing",
                        "Housing",
                        2,
                    },
                    {
                        new Guid("22222222-2222-4222-8222-222222222225"),
                        "#FBBF24",
                        "bills",
                        "Bills",
                        2,
                    },
                    {
                        new Guid("22222222-2222-4222-8222-222222222226"),
                        "#F87171",
                        "health",
                        "Health",
                        2,
                    },
                    {
                        new Guid("22222222-2222-4222-8222-222222222227"),
                        "#60A5FA",
                        "education",
                        "Education",
                        2,
                    },
                    {
                        new Guid("22222222-2222-4222-8222-222222222228"),
                        "#FACC15",
                        "entertainment",
                        "Entertainment",
                        2,
                    },
                    {
                        new Guid("22222222-2222-4222-8222-222222222229"),
                        "#818CF8",
                        "subscriptions",
                        "Subscriptions",
                        2,
                    },
                    {
                        new Guid("22222222-2222-4222-8222-22222222223a"),
                        "#34D399",
                        "groceries",
                        "Groceries",
                        2,
                    },
                    {
                        new Guid("22222222-2222-4222-8222-22222222223b"),
                        "#2DD4BF",
                        "travel",
                        "Travel",
                        2,
                    },
                    {
                        new Guid("22222222-2222-4222-8222-22222222223c"),
                        "#FB7185",
                        "personal-care",
                        "Personal Care",
                        2,
                    },
                    {
                        new Guid("22222222-2222-4222-8222-22222222223d"),
                        "#C084FC",
                        "pets",
                        "Pets",
                        2,
                    },
                    {
                        new Guid("22222222-2222-4222-8222-22222222223e"),
                        "#94A3B8",
                        "taxes",
                        "Taxes",
                        2,
                    },
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Name",
                table: "Accounts",
                column: "Name"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Date",
                table: "Transactions",
                column: "Date"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Transactions");

            migrationBuilder.DropTable(name: "Accounts");

            migrationBuilder.DropTable(name: "Categories");
        }
    }
}
