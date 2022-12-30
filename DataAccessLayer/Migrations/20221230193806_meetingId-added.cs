using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class meetingIdadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoteMail_Meetings_MeetingID",
                table: "VoteMail");

            migrationBuilder.RenameColumn(
                name: "MeetingID",
                table: "VoteMail",
                newName: "MeetingId");

            migrationBuilder.RenameIndex(
                name: "IX_VoteMail_MeetingID",
                table: "VoteMail",
                newName: "IX_VoteMail_MeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoteMail_Meetings_MeetingId",
                table: "VoteMail",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "MeetingID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoteMail_Meetings_MeetingId",
                table: "VoteMail");

            migrationBuilder.RenameColumn(
                name: "MeetingId",
                table: "VoteMail",
                newName: "MeetingID");

            migrationBuilder.RenameIndex(
                name: "IX_VoteMail_MeetingId",
                table: "VoteMail",
                newName: "IX_VoteMail_MeetingID");

            migrationBuilder.AddForeignKey(
                name: "FK_VoteMail_Meetings_MeetingID",
                table: "VoteMail",
                column: "MeetingID",
                principalTable: "Meetings",
                principalColumn: "MeetingID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
