using Microsoft.EntityFrameworkCore.Migrations;

namespace CashMachine.Migrations
{
    public partial class sampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "Amount" },
                values: new object[] { 1, 80000 });

            migrationBuilder.InsertData(
                table: "Money",
                columns: new[] { "Id", "Quantity", "Value" },
                values: new object[,]
                {
                    { 1, 50, 500 },
                    { 2, 50, 1000 },
                    { 3, 100, 100 },
                    { 4, 25, 5000 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Money",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Money",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Money",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Money",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
