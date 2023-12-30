using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kidoo.Learn.Migrations
{
    /// <inheritdoc />
    public partial class File_Adding_12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KidooCourseSections_KidooCourses_CourseId",
                table: "KidooCourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_KidooCourseTopics_KidooCourseSections_CourseSectionId",
                table: "KidooCourseTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KidooCourseTopics",
                table: "KidooCourseTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KidooCourseSections",
                table: "KidooCourseSections");

            migrationBuilder.RenameTable(
                name: "KidooCourseTopics",
                newName: "CourseTopics");

            migrationBuilder.RenameTable(
                name: "KidooCourseSections",
                newName: "CourseSections");

            migrationBuilder.RenameIndex(
                name: "IX_KidooCourseTopics_CourseSectionId",
                table: "CourseTopics",
                newName: "IX_CourseTopics_CourseSectionId");

            migrationBuilder.RenameIndex(
                name: "IX_KidooCourseSections_CourseId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "KidooCourseTopics");

            migrationBuilder.RenameTable(
                name: "CourseSections",
                newName: "KidooCourseSections");

            migrationBuilder.RenameIndex(
                name: "IX_CourseTopics_CourseSectionId",
                table: "KidooCourseTopics",
                newName: "IX_KidooCourseTopics_CourseSectionId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseSections_CourseId",
                table: "KidooCourseSections",
                newName: "IX_KidooCourseSections_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KidooCourseTopics",
                table: "KidooCourseTopics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KidooCourseSections",
                table: "KidooCourseSections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KidooCourseSections_KidooCourses_CourseId",
                table: "KidooCourseSections",
                column: "CourseId",
                principalTable: "KidooCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KidooCourseTopics_KidooCourseSections_CourseSectionId",
                table: "KidooCourseTopics",
                column: "CourseSectionId",
                principalTable: "KidooCourseSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
