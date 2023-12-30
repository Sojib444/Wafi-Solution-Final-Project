using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kidoo.Learn.Migrations
{
    /// <inheritdoc />
    public partial class File_Adding_13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_KidooCourses_CourseId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTopics_CourseSections_CourseSectionId",
                table: "CourseTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseTopics",
                table: "CourseTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseSections",
                table: "CourseSections");

            migrationBuilder.RenameTable(
                name: "CourseTopics",
                newName: "CourseTopic");

            migrationBuilder.RenameTable(
                name: "CourseSections",
                newName: "CourseSection");

            migrationBuilder.RenameIndex(
                name: "IX_CourseTopics_CourseSectionId",
                table: "CourseTopic",
                newName: "IX_CourseTopic_CourseSectionId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseSections_CourseId",
                table: "CourseSection",
                newName: "IX_CourseSection_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseTopic",
                table: "CourseTopic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseSection",
                table: "CourseSection",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSection_KidooCourses_CourseId",
                table: "CourseSection",
                column: "CourseId",
                principalTable: "KidooCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTopic_CourseSection_CourseSectionId",
                table: "CourseTopic",
                column: "CourseSectionId",
                principalTable: "CourseSection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSection_KidooCourses_CourseId",
                table: "CourseSection");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTopic_CourseSection_CourseSectionId",
                table: "CourseTopic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseTopic",
                table: "CourseTopic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseSection",
                table: "CourseSection");

            migrationBuilder.RenameTable(
                name: "CourseTopic",
                newName: "CourseTopics");

            migrationBuilder.RenameTable(
                name: "CourseSection",
                newName: "CourseSections");

            migrationBuilder.RenameIndex(
                name: "IX_CourseTopic_CourseSectionId",
                table: "CourseTopics",
                newName: "IX_CourseTopics_CourseSectionId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseSection_CourseId",
                table: "CourseSections",
                newName: "IX_CourseSections_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseTopics",
                table: "CourseTopics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseSections",
                table: "CourseSections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_KidooCourses_CourseId",
                table: "CourseSections",
                column: "CourseId",
                principalTable: "KidooCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTopics_CourseSections_CourseSectionId",
                table: "CourseTopics",
                column: "CourseSectionId",
                principalTable: "CourseSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
