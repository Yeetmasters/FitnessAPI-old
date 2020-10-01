using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    password = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    firstname = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    lastname = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    height = table.Column<float>(nullable: false),
                    weight = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Workout",
                columns: table => new
                {
                    workout_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    workoutname = table.Column<string>(maxLength: 30, nullable: false),
                    goal = table.Column<int>(nullable: false),
                    difficulty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout", x => x.workout_id);
                });

            migrationBuilder.CreateTable(
                name: "User_Workout",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false),
                    workout_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Workout", x => new { x.user_id, x.workout_id });
                    table.ForeignKey(
                        name: "FK__User_Workout__user",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__User_Workout__workout",
                        column: x => x.workout_id,
                        principalTable: "Workout",
                        principalColumn: "workout_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Workout_workout_id",
                table: "User_Workout",
                column: "workout_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_Workout");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Workout");
        }
    }
}
