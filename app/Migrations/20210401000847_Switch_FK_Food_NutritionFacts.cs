using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TasteUfes.Migrations
{
    public partial class Switch_FK_Food_NutritionFacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_NutritionFacts_NutritionFactsId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_NutritionFactsId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "NutritionFactsId",
                table: "Foods");

            migrationBuilder.AddColumn<Guid>(
                name: "FoodId",
                table: "NutritionFacts",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cab6b7ab-636c-4b3f-a549-7e5284a92848"),
                column: "Password",
                value: "AQAAAAEAACcQAAAAEM4y+xpWBmcmKPIBsBOrcTJiJ5I8NyphxIhDWYNlEQRsoTwJDTWUwiDoDqecgXCKxA==");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionFacts_FoodId",
                table: "NutritionFacts",
                column: "FoodId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionFacts_Foods_FoodId",
                table: "NutritionFacts",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NutritionFacts_Foods_FoodId",
                table: "NutritionFacts");

            migrationBuilder.DropIndex(
                name: "IX_NutritionFacts_FoodId",
                table: "NutritionFacts");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "NutritionFacts");

            migrationBuilder.AddColumn<Guid>(
                name: "NutritionFactsId",
                table: "Foods",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cab6b7ab-636c-4b3f-a549-7e5284a92848"),
                column: "Password",
                value: "AQAAAAEAACcQAAAAEMWXKZqYj6WvHX8oc0M9YUFYjMJItW+1xwSBbrZCZ2EB8d0wuPQIFRaaC3e5lCZStw==");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_NutritionFactsId",
                table: "Foods",
                column: "NutritionFactsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_NutritionFacts_NutritionFactsId",
                table: "Foods",
                column: "NutritionFactsId",
                principalTable: "NutritionFacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
