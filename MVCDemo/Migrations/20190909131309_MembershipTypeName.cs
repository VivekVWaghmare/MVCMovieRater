using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCDemo.Migrations
{
    public partial class MembershipTypeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MemberShipType",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "MemberShipType");
        }
    }
}
