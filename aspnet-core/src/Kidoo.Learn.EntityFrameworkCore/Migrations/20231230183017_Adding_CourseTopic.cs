using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kidoo.Learn.Migrations
{
    /// <inheritdoc />
    public partial class Adding_CourseTopic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTopic_KidooCourseSections_CourseSectionId",
                table: "CourseTopic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseTopic",
                table: "CourseTopic");

            migrationBuilder.RenameTable(
                name: "CourseTopic",
                newName: "KidooCourseTopics");

            migrationBuilder.RenameColumn(
                name: "VideoUrl",
                table: "KidooCourseTopics",
                newName: "VideoFileType");

            migrationBuilder.RenameColumn(
                name: "ThumbnailUrl",
                table: "KidooCourseTopics",
                newName: "VideoFileName");

            migrationBuilder.RenameIndex(
                name: "IX_CourseTopic_CourseSectionId",
                table: "KidooCourseTopics",
                newName: "IX_KidooCourseTopics_CourseSectionId");

            migrationBuilder.AddColumn<byte[]>(
                name: "ThumbnailFileContent",
                table: "KidooCourseTopics",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailFileName",
                table: "KidooCourseTopics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailFileType",
                table: "KidooCourseTopics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "VideoFileContent",
                table: "KidooCourseTopics",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_KidooCourseTopics",
                table: "KidooCourseTopics",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KidooCourseTopics_KidooCourseSections_CourseSectionId",
                table: "KidooCourseTopics",
                column: "CourseSectionId",
                principalTable: "KidooCourseSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KidooCourseTopics_KidooCourseSections_CourseSectionId",
                table: "KidooCourseTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KidooCourseTopics",
                table: "KidooCourseTopics");

            migrationBuilder.DropColumn(
                name: "ThumbnailFileContent",
                table: "KidooCourseTopics");

            migrationBuilder.DropColumn(
                name: "ThumbnailFileName",
                table: "KidooCourseTopics");

            migrationBuilder.DropColumn(
                name: "ThumbnailFileType",
                table: "KidooCourseTopics");

            migrationBuilder.DropColumn(
                name: "VideoFileContent",
                table: "KidooCourseTopics");

            migrationBuilder.RenameTable(
                name: "KidooCourseTopics",
                newName: "CourseTopic");

            migrationBuilder.RenameColumn(
                name: "VideoFileType",
                table: "CourseTopic",
                newName: "VideoUrl");

            migrationBuilder.RenameColumn(
                name: "VideoFileName",
                table: "CourseTopic",
                newName: "ThumbnailUrl");

            migrationBuilder.RenameIndex(
                name: "IX_KidooCourseTopics_CourseSectionId",
                table: "CourseTopic",
                newName: "IX_CourseTopic_CourseSectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseTopic",
                table: "CourseTopic",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTopic_KidooCourseSections_CourseSectionId",
                table: "CourseTopic",
                column: "CourseSectionId",
                principalTable: "KidooCourseSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
