using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCDemo.Migrations
{
    public partial class Movies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_MemberShipType_MembershipTypeId",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberShipType",
                table: "MemberShipType");

            migrationBuilder.RenameTable(
                name: "MemberShipType",
                newName: "MemberShipTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberShipTypes",
                table: "MemberShipTypes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_MemberShipTypes_MembershipTypeId",
                table: "Customers",
                column: "MembershipTypeId",
                principalTable: "MemberShipTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_MemberShipTypes_MembershipTypeId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberShipTypes",
                table: "MemberShipTypes");

            migrationBuilder.RenameTable(
                name: "MemberShipTypes",
                newName: "MemberShipType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberShipType",
                table: "MemberShipType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_MemberShipType_MembershipTypeId",
                table: "Customers",
                column: "MembershipTypeId",
                principalTable: "MemberShipType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
