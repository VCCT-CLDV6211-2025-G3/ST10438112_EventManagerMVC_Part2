using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagerMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddImageURLToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4,
                column: "ImageURL",
                value: null);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 5,
                column: "ImageURL",
                value: null);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 6,
                column: "ImageURL",
                value: null);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 7,
                column: "ImageURL",
                value: null);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 8,
                column: "ImageURL",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Events");
        }
    }
}
/*
 * Refrences:
 * Huawei Technologies, 2023. Cloud Computing Technologies. Hangzhou: Posts & Telecom Press.
 * Mrzyglód, K., 2022. Azure for Developers. 2nd ed. Birmingham: Packt Publishing.
 * Microsoft Corporation, 2022. The Developer’s Guide to Azure. Redmond: Microsoft Press.
 * OpenAI, 2025. ChatGPT. [online] Available at: https://openai.com/chatgpt/ [Accessed 03 May 2025].
 * Github Inc., 2025. GitHub Copilot. [online] Available at: https://github.com [Accessed 03 May 2025].
 * Varsity College, 2025. INSY6112 Module Manual. Cape Town: The Independent Institute of Education.
 */ 