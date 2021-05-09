using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TasteUfes.Migrations
{
    public partial class NotMapped_ServingEnergy_On_NutritionFacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServingEnergy",
                table: "NutritionFacts");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cab6b7ab-636c-4b3f-a549-7e5284a92848"),
                column: "Password",
                value: "AQAAAAEAACcQAAAAEMWXKZqYj6WvHX8oc0M9YUFYjMJItW+1xwSBbrZCZ2EB8d0wuPQIFRaaC3e5lCZStw==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ServingEnergy",
                table: "NutritionFacts",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cab6b7ab-636c-4b3f-a549-7e5284a92848"),
                column: "Password",
                value: "AQAAAAEAACcQAAAAEFPN8KSb/rHZL4sjWvxiO7XJA2vy82PvnYJIp3sfBK6mmWFkcNdbLs24oPQtzKK+Bg==");
        }
    }
}
