using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventAPI.Migrations
{
    /// <inheritdoc />
    public partial class Addcolumnswithdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxPeople = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Speakers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventParticipant",
                columns: table => new
                {
                    Event_Id = table.Column<int>(type: "int", nullable: false),
                    Participant_Id = table.Column<int>(type: "int", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CancelDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventParticipant", x => new { x.Event_Id, x.Participant_Id });
                    table.ForeignKey(
                        name: "FK_EventParticipant_Events_Event_Id",
                        column: x => x.Event_Id,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventParticipant_Participants_Participant_Id",
                        column: x => x.Participant_Id,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventSpeaker",
                columns: table => new
                {
                    Event_Id = table.Column<int>(type: "int", nullable: false),
                    Speaker_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSpeaker", x => new { x.Event_Id, x.Speaker_Id });
                    table.ForeignKey(
                        name: "FK_EventSpeaker_Events_Event_Id",
                        column: x => x.Event_Id,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventSpeaker_Speakers_Speaker_Id",
                        column: x => x.Speaker_Id,
                        principalTable: "Speakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "Description", "MaxPeople", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), "Sztuczna inteligencja w praktyce", 7, "AI Konferencja" },
                    { 2, new DateTime(2025, 7, 7, 0, 0, 0, 0, DateTimeKind.Local), "Chmura i bezpieczeństwo", 80, "Chmura" },
                    { 3, new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Local), "Automatyzacja i CI/CD", 60, "DevOps" }
                });

            migrationBuilder.InsertData(
                table: "Participants",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "adam1@mail.com", "Adam", "Kowal" },
                    { 2, "beata2@mail.com", "Beata", "Nowak" },
                    { 3, "cezary3@mail.com", "Cezary", "Lis" },
                    { 4, "dorota4@mail.com", "Dorota", "Mazur" },
                    { 5, "edward5@mail.com", "Edward", "Wójcik" },
                    { 6, "filip6@mail.com", "Filip", "Kaczmarek" },
                    { 7, "grazyna7@mail.com", "Grażyna", "Baran" },
                    { 8, "henryk8@mail.com", "Henryk", "Sikora" },
                    { 9, "iwona9@mail.com", "Iwona", "Król" },
                    { 10, "jacek10@mail.com", "Jacek", "Wieczorek" },
                    { 11, "katarzyna11@mail.com", "Katarzyna", "Jankowska" },
                    { 12, "leszek12@mail.com", "Leszek", "Zając" },
                    { 13, "monika13@mail.com", "Monika", "Pawlak" },
                    { 14, "norbert14@mail.com", "Norbert", "Michalski" },
                    { 15, "olga15@mail.com", "Olga", "Kubiak" }
                });

            migrationBuilder.InsertData(
                table: "Speakers",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Anna", "Kowalska" },
                    { 2, "Jan", "Nowak" },
                    { 3, "Maria", "Wiśniewska" },
                    { 4, "Piotr", "Zieliński" },
                    { 5, "Ewa", "Dąbrowska" }
                });

            migrationBuilder.InsertData(
                table: "EventParticipant",
                columns: new[] { "Event_Id", "Participant_Id", "CancelDate", "RegisterDate", "Status" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), "Registered" },
                    { 1, 2, null, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), "Registered" },
                    { 1, 3, null, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), "Registered" },
                    { 1, 4, null, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), "Registered" },
                    { 1, 5, null, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), "Registered" },
                    { 2, 6, null, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), "Registered" },
                    { 2, 7, null, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), "Registered" },
                    { 2, 8, null, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), "Registered" },
                    { 2, 9, null, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), "Registered" },
                    { 2, 10, null, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), "Registered" },
                    { 3, 11, null, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), "Registered" },
                    { 3, 12, null, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), "Registered" },
                    { 3, 13, null, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), "Registered" },
                    { 3, 14, null, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), "Registered" },
                    { 3, 15, null, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), "Registered" }
                });

            migrationBuilder.InsertData(
                table: "EventSpeaker",
                columns: new[] { "Event_Id", "Speaker_Id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipant_Participant_Id",
                table: "EventParticipant",
                column: "Participant_Id");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpeaker_Speaker_Id",
                table: "EventSpeaker",
                column: "Speaker_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventParticipant");

            migrationBuilder.DropTable(
                name: "EventSpeaker");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Speakers");
        }
    }
}
