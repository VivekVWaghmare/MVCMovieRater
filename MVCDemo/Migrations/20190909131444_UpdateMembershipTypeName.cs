using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCDemo.Migrations
{
    public partial class UpdateMembershipTypeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE MemberShipType SET Name = 'Pay As You Go' WHERE Id=1");
            migrationBuilder.Sql("UPDATE MemberShipType SET Name = 'Monthly' WHERE Id=2");
            migrationBuilder.Sql("UPDATE MemberShipType SET Name = 'Quarterly' WHERE Id=3");
            migrationBuilder.Sql("UPDATE MemberShipType SET Name = 'Yearly' WHERE Id=4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
