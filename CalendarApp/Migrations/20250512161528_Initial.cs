using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CalendarApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupMeetingParticipants",
                columns: table => new
                {
                    GroupMeetingId = table.Column<int>(nullable: false),
                    ParticipantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMeetingParticipants", x => new { x.GroupMeetingId, x.ParticipantId });
                    table.ForeignKey(
                        name: "FK_GroupMeetingParticipants_Appointments_GroupMeetingId",
                        column: x => x.GroupMeetingId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupMeetingParticipants_Users_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TriggerTime = table.Column<DateTime>(nullable: false),
                    RelatedAppointmentId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reminders_Appointments_RelatedAppointmentId",
                        column: x => x.RelatedAppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reminders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { 1, "password1", "user1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { 2, "password2", "user2" });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "Discriminator", "EndTime", "Location", "Name", "OwnerId", "StartTime" },
                values: new object[] { 1, "Appointment", new DateTime(2025, 5, 13, 10, 0, 0, 0, DateTimeKind.Local), "Office A", "Meeting with Boss", 1, new DateTime(2025, 5, 13, 9, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "Discriminator", "EndTime", "Location", "Name", "OwnerId", "StartTime" },
                values: new object[] { 3, "GroupMeeting", new DateTime(2025, 5, 13, 11, 30, 0, 0, DateTimeKind.Local), "Meeting Room 1", "Team Sync", 1, new DateTime(2025, 5, 13, 11, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "Discriminator", "EndTime", "Location", "Name", "OwnerId", "StartTime" },
                values: new object[] { 2, "Appointment", new DateTime(2025, 5, 14, 16, 0, 0, 0, DateTimeKind.Local), "Home Office", "Project Deadline Prep", 2, new DateTime(2025, 5, 14, 14, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "GroupMeetingParticipants",
                columns: new[] { "GroupMeetingId", "ParticipantId" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Reminders",
                columns: new[] { "Id", "RelatedAppointmentId", "TriggerTime", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 5, 13, 8, 45, 0, 0, DateTimeKind.Local), 1 },
                    { 2, 2, new DateTime(2025, 5, 14, 13, 30, 0, 0, DateTimeKind.Local), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_OwnerId",
                table: "Appointments",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMeetingParticipants_ParticipantId",
                table: "GroupMeetingParticipants",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_RelatedAppointmentId",
                table: "Reminders",
                column: "RelatedAppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_UserId",
                table: "Reminders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupMeetingParticipants");

            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
