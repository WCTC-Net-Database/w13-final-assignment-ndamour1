using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleRpgEntities.Migrations
{
    public partial class MassUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MonsterType",
                table: "Monsters",
                newName: "Race");

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Modifier",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Race",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Monsters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "Monsters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Monsters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Waaagh",
                table: "Monsters",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Damage",
                table: "Abilities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Defense",
                table: "Abilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Dodge",
                table: "Abilities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InUse",
                table: "Abilities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MonsterId",
                table: "Abilities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_EquipmentId",
                table: "Monsters",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_RoomId",
                table: "Monsters",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Abilities_MonsterId",
                table: "Abilities",
                column: "MonsterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abilities_Monsters_MonsterId",
                table: "Abilities",
                column: "MonsterId",
                principalTable: "Monsters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Monsters_Equipments_EquipmentId",
                table: "Monsters",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Monsters_Rooms_RoomId",
                table: "Monsters",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abilities_Monsters_MonsterId",
                table: "Abilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Monsters_Equipments_EquipmentId",
                table: "Monsters");

            migrationBuilder.DropForeignKey(
                name: "FK_Monsters_Rooms_RoomId",
                table: "Monsters");

            migrationBuilder.DropIndex(
                name: "IX_Monsters_EquipmentId",
                table: "Monsters");

            migrationBuilder.DropIndex(
                name: "IX_Monsters_RoomId",
                table: "Monsters");

            migrationBuilder.DropIndex(
                name: "IX_Abilities_MonsterId",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Modifier",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Race",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "Waaagh",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "Defense",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "Dodge",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "InUse",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "MonsterId",
                table: "Abilities");

            migrationBuilder.RenameColumn(
                name: "Race",
                table: "Monsters",
                newName: "MonsterType");

            migrationBuilder.AlterColumn<int>(
                name: "Damage",
                table: "Abilities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
