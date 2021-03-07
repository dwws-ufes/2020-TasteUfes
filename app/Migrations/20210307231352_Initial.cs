using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TasteUfes.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nutrients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(255)", maxLength: 64, nullable: false),
                    EnergyPerGram = table.Column<double>(type: "REAL", nullable: false),
                    DailyRecommendation = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutrients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NutritionFacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ServingSize = table.Column<double>(type: "REAL", nullable: false),
                    ServingSizeUnit = table.Column<int>(type: "INTEGER", nullable: false),
                    ServingEnergy = table.Column<double>(type: "REAL", nullable: false),
                    DailyValue = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionFacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(255)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "VARCHAR(255)", maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(255)", maxLength: 128, nullable: false),
                    Username = table.Column<string>(type: "VARCHAR(255)", maxLength: 16, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(255)", maxLength: 256, nullable: false),
                    NutritionFactsId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foods_NutritionFacts_NutritionFactsId",
                        column: x => x.NutritionFactsId,
                        principalTable: "NutritionFacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NutritionFactsNutrients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AmountPerServing = table.Column<double>(type: "REAL", nullable: false),
                    AmountPerServingUnit = table.Column<int>(type: "INTEGER", nullable: false),
                    DailyValue = table.Column<double>(type: "REAL", nullable: false),
                    NutritionFactsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NutrientId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionFactsNutrients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionFactsNutrients_Nutrients_NutrientId",
                        column: x => x.NutrientId,
                        principalTable: "Nutrients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NutritionFactsNutrients_NutritionFacts_NutritionFactsId",
                        column: x => x.NutritionFactsId,
                        principalTable: "NutritionFacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(255)", maxLength: 256, nullable: false),
                    Servings = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityUnit = table.Column<int>(type: "INTEGER", nullable: false),
                    FoodId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RecipeId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Preparations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PreparationTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    RecipeId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preparations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Preparations_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PreparationSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Step = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(255)", maxLength: 2048, nullable: false),
                    PreparationId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreparationSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreparationSteps_Preparations_PreparationId",
                        column: x => x.PreparationId,
                        principalTable: "Preparations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Nutrients",
                columns: new[] { "Id", "DailyRecommendation", "EnergyPerGram", "Name" },
                values: new object[] { new Guid("f1602bf1-47f8-4310-b42b-ae19f8b14d42"), 375.0, 4.0, "Carbohydrate" });

            migrationBuilder.InsertData(
                table: "Nutrients",
                columns: new[] { "Id", "DailyRecommendation", "EnergyPerGram", "Name" },
                values: new object[] { new Guid("df414894-5d99-41a9-a82f-8db54526d580"), 50.0, 4.0, "Protein" });

            migrationBuilder.InsertData(
                table: "Nutrients",
                columns: new[] { "Id", "DailyRecommendation", "EnergyPerGram", "Name" },
                values: new object[] { new Guid("d5ed4949-2d1b-4a9a-bea6-b2b03c54b255"), 80.0, 9.0, "Total Fat" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("48a7328b-4f1e-4b88-b62f-ac9ac6ef03c8"), "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Foods_Name",
                table: "Foods",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foods_NutritionFactsId",
                table: "Foods",
                column: "NutritionFactsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_FoodId",
                table: "Ingredients",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Nutrients_Name",
                table: "Nutrients",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NutritionFactsNutrients_NutrientId",
                table: "NutritionFactsNutrients",
                column: "NutrientId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionFactsNutrients_NutritionFactsId",
                table: "NutritionFactsNutrients",
                column: "NutritionFactsId");

            migrationBuilder.CreateIndex(
                name: "IX_Preparations_RecipeId",
                table: "Preparations",
                column: "RecipeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreparationSteps_PreparationId",
                table: "PreparationSteps",
                column: "PreparationId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UsersId",
                table: "Recipes",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "NutritionFactsNutrients");

            migrationBuilder.DropTable(
                name: "PreparationSteps");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Nutrients");

            migrationBuilder.DropTable(
                name: "Preparations");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "NutritionFacts");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
