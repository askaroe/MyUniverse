using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyUniverse.Migrations
{
    /// <inheritdoc />
    public partial class InitializedTableForQA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionsAnswers",
                columns: table => new
                {
                    Question_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Student_ID = table.Column<int>(type: "integer", nullable: false),
                    Teacher_ID = table.Column<int>(type: "integer", nullable: false),
                    QuestionDescription = table.Column<string>(type: "text", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsAnswers", x => x.Question_ID);
                    table.ForeignKey(
                        name: "FK_QuestionsAnswers_Students_Student_ID",
                        column: x => x.Student_ID,
                        principalTable: "Students",
                        principalColumn: "Student_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionsAnswers_Teachers_Teacher_ID",
                        column: x => x.Teacher_ID,
                        principalTable: "Teachers",
                        principalColumn: "Teacher_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAnswers_Student_ID",
                table: "QuestionsAnswers",
                column: "Student_ID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAnswers_Teacher_ID",
                table: "QuestionsAnswers",
                column: "Teacher_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionsAnswers");
        }
    }
}
