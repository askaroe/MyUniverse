using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyUniverse.Migrations
{
    /// <inheritdoc />
    public partial class InitializedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Course_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Course_ID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Student_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Student_ID);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Teacher_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Teacher_ID);
                });

            migrationBuilder.CreateTable(
                name: "Student_Courses",
                columns: table => new
                {
                    Student_course_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Student_ID = table.Column<int>(type: "integer", nullable: false),
                    Course_ID = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Courses", x => x.Student_course_ID);
                    table.ForeignKey(
                        name: "FK_Student_Courses_Courses_Course_ID",
                        column: x => x.Course_ID,
                        principalTable: "Courses",
                        principalColumn: "Course_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Courses_Students_Student_ID",
                        column: x => x.Student_ID,
                        principalTable: "Students",
                        principalColumn: "Student_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teacher_Courses",
                columns: table => new
                {
                    Teacher_course_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Teacher_ID = table.Column<int>(type: "integer", nullable: false),
                    Course_ID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher_Courses", x => x.Teacher_course_ID);
                    table.ForeignKey(
                        name: "FK_Teacher_Courses_Courses_Course_ID",
                        column: x => x.Course_ID,
                        principalTable: "Courses",
                        principalColumn: "Course_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teacher_Courses_Teachers_Teacher_ID",
                        column: x => x.Teacher_ID,
                        principalTable: "Teachers",
                        principalColumn: "Teacher_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_Courses_Course_ID",
                table: "Student_Courses",
                column: "Course_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Courses_Student_ID",
                table: "Student_Courses",
                column: "Student_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_Courses_Course_ID",
                table: "Teacher_Courses",
                column: "Course_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_Courses_Teacher_ID",
                table: "Teacher_Courses",
                column: "Teacher_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student_Courses");

            migrationBuilder.DropTable(
                name: "Teacher_Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
