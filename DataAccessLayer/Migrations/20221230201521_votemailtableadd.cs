using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class votemailtableadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoteMail_Meetings_MeetingId",
                table: "VoteMail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoteMail",
                table: "VoteMail");

            migrationBuilder.RenameTable(
                name: "VoteMail",
                newName: "VoteMails");

            migrationBuilder.RenameIndex(
                name: "IX_VoteMail_MeetingId",
                table: "VoteMails",
                newName: "IX_VoteMails_MeetingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoteMails",
                table: "VoteMails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VoteMails_Meetings_MeetingId",
                table: "VoteMails",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "MeetingID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoteMails_Meetings_MeetingId",
                table: "VoteMails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoteMails",
                table: "VoteMails");

            migrationBuilder.RenameTable(
                name: "VoteMails",
                newName: "VoteMail");

            migrationBuilder.RenameIndex(
                name: "IX_VoteMails_MeetingId",
                table: "VoteMail",
                newName: "IX_VoteMail_MeetingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoteMail",
                table: "VoteMail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VoteMail_Meetings_MeetingId",
                table: "VoteMail",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "MeetingID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
