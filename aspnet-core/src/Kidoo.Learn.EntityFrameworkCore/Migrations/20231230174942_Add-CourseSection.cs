using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kidoo.Learn.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSection_KidooCourses_CourseId",
                table: "CourseSection");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTopic_CourseSection_CourseSectionId",
                table: "CourseTopic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseSection",
                table: "CourseSection");

            migrationBuilder.RenameTable(
                name: "CourseSection",
                newName: "KidooCourseSections");

            migrationBuilder.RenameColumn(
                name: "ThumbnailUrl",
                table: "KidooCourseSections",
                newName: "ThumbnailFileType");

            migrationBuilder.RenameIndex(
                name: "IX_CourseSection_CourseId",
                table: "KidooCourseSections",
                newName: "IX_KidooCourseSections_CourseId");

            migrationBuilder.AddColumn<byte[]>(
                name: "ThumbnailFileContent",
                table: "KidooCourseSections",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailFileName",
                table: "KidooCourseSections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_KidooCourseSections",
                table: "KidooCourseSections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTopic_KidooCourseSections_CourseSectionId",
                table: "CourseTopic",
                column: "CourseSectionId",
                principalTable: "KidooCourseSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KidooCourseSections_KidooCourses_CourseId",
                table: "KidooCourseSections",
                column: "CourseId",
                principalTable: "KidooCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTopic_KidooCourseSections_CourseSectionId",
                table: "CourseTopic");

            migrationBuilder.DropForeignKey(
                name: "FK_KidooCourseSections_KidooCourses_CourseId",
                table: "KidooCourseSections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KidooCourseSections",
                table: "KidooCourseSections");

            migrationBuilder.DropColumn(
                name: "ThumbnailFileContent",
                table: "KidooCourseSections");

            migrationBuilder.DropColumn(
                name: "ThumbnailFileName",
                table: "KidooCourseSections");

            migrationBuilder.RenameTable(
                name: "KidooCourseSections",
                newName: "CourseSection");

            migrationBuilder.RenameColumn(
                name: "ThumbnailFileType",
                table: "CourseSection",
                newName: "ThumbnailUrl");

            migrationBuilder.RenameIndex(
                name: "IX_KidooCourseSections_CourseId",
                table: "CourseSection",
                newName: "IX_CourseSection_CourseId");

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
    }
}
