using Microsoft.EntityFrameworkCore.Migrations;

namespace GetirCase.Data.Migrations
{
    public partial class SeedCustomersAndAccountsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("INSERT INTO Customers (Name, Email, PhoneNumber, Password) Values ('Merve', 'm@g.com', '555-5555', 'JcLJr92DuNNCNKoogcw0HAlomqo=')");
            migrationBuilder
                .Sql("INSERT INTO Customers (Name, Email, PhoneNumber, Password) Values ('Emre', 'e@g.com', '455-5555', 'JcLJr92DuNNCNKoogcw0HAlomqo=')");

            migrationBuilder
                .Sql("INSERT INTO Accounts (Balance, Name, CustomerId) Values (1000.5, 'Current', (SELECT Id FROM Customers WHERE Name = 'Merve'))");
            migrationBuilder
                .Sql("INSERT INTO Accounts (Balance, Name, CustomerId) Values (230.8, 'Current', (SELECT Id FROM Customers WHERE Name = 'Emre'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("DELETE FROM Customers");

            migrationBuilder
                .Sql("DELETE FROM Accounts");
        }
    }
}