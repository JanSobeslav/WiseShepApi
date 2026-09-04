using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JesonApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FrameResource_Lyrics_LyricResourceId",
                table: "FrameResource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FrameResource",
                table: "FrameResource");

            migrationBuilder.RenameTable(
                name: "FrameResource",
                newName: "Frames");

            migrationBuilder.RenameColumn(
                name: "Translations",
                table: "Frames",
                newName: "TranslationsJson");

            migrationBuilder.RenameIndex(
                name: "IX_FrameResource_LyricResourceId",
                table: "Frames",
                newName: "IX_Frames_LyricResourceId");

            migrationBuilder.AlterColumn<int>(
                name: "LyricResourceId",
                table: "Frames",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Frames",
                table: "Frames",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Frames_Lyrics_LyricResourceId",
                table: "Frames",
                column: "LyricResourceId",
                principalTable: "Lyrics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Frames_Lyrics_LyricResourceId",
                table: "Frames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Frames",
                table: "Frames");

            migrationBuilder.RenameTable(
                name: "Frames",
                newName: "FrameResource");

            migrationBuilder.RenameColumn(
                name: "TranslationsJson",
                table: "FrameResource",
                newName: "Translations");

            migrationBuilder.RenameIndex(
                name: "IX_Frames_LyricResourceId",
                table: "FrameResource",
                newName: "IX_FrameResource_LyricResourceId");

            migrationBuilder.AlterColumn<int>(
                name: "LyricResourceId",
                table: "FrameResource",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FrameResource",
                table: "FrameResource",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FrameResource_Lyrics_LyricResourceId",
                table: "FrameResource",
                column: "LyricResourceId",
                principalTable: "Lyrics",
                principalColumn: "Id");
        }
    }
}
