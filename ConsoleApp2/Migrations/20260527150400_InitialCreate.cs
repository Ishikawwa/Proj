using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Institutions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Longitude = table.Column<long>(type: "bigint", nullable: false),
                    Latitude = table.Column<long>(type: "bigint", nullable: false),
                    AvatarUrl = table.Column<string>(type: "text", nullable: true),
                    PhotoUrls = table.Column<List<string>>(type: "text[]", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    AveragePrice = table.Column<int>(type: "integer", nullable: true),
                    WebUrl = table.Column<string>(type: "text", nullable: true),
                    Labels = table.Column<int[]>(type: "integer[]", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nickname = table.Column<string>(type: "text", nullable: false),
                    AvatarUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsBanned = table.Column<bool>(type: "boolean", nullable: false),
                    IsMuted = table.Column<bool>(type: "boolean", nullable: false),
                    VkId = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstitutionLabels",
                columns: table => new
                {
                    InstitutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitutionLabels", x => new { x.InstitutionId, x.Label });
                    table.ForeignKey(
                        name: "FK_InstitutionLabels_Institutions_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    End = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    InstitutionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingHours_Institutions_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteInstitutions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteInstitutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavouriteInstitutions_Institutions_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteInstitutions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OwnerRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    IsProcessed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnerRequests_Institutions_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnerRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsBanned = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Institutions_InstitutionEntityId",
                        column: x => x.InstitutionEntityId,
                        principalTable: "Institutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UpdateInstitutionRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdateInstitutionRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpdateInstitutionRequests_Institutions_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UpdateInstitutionRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Viewings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viewings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Viewings_Institutions_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Viewings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visitings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visitings_Institutions_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visitings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReviewId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsLiked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewScores_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewScores_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpamReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReviewId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReviewEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    IsProcessed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpamReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpamReports_Reviews_ReviewEntityId",
                        column: x => x.ReviewEntityId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpamReports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteInstitutions_InstitutionId",
                table: "FavouriteInstitutions",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteInstitutions_UserId",
                table: "FavouriteInstitutions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerRequests_InstitutionId",
                table: "OwnerRequests",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerRequests_UserId",
                table: "OwnerRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_InstitutionEntityId",
                table: "Reviews",
                column: "InstitutionEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewScores_ReviewId",
                table: "ReviewScores",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewScores_UserId",
                table: "ReviewScores",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SpamReports_ReviewEntityId",
                table: "SpamReports",
                column: "ReviewEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SpamReports_UserId",
                table: "SpamReports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UpdateInstitutionRequests_InstitutionId",
                table: "UpdateInstitutionRequests",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_UpdateInstitutionRequests_UserId",
                table: "UpdateInstitutionRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Viewings_InstitutionId",
                table: "Viewings",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Viewings_UserId",
                table: "Viewings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitings_InstitutionId",
                table: "Visitings",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitings_UserId",
                table: "Visitings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_InstitutionId",
                table: "WorkingHours",
                column: "InstitutionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouriteInstitutions");

            migrationBuilder.DropTable(
                name: "InstitutionLabels");

            migrationBuilder.DropTable(
                name: "OwnerRequests");

            migrationBuilder.DropTable(
                name: "ReviewScores");

            migrationBuilder.DropTable(
                name: "SpamReports");

            migrationBuilder.DropTable(
                name: "UpdateInstitutionRequests");

            migrationBuilder.DropTable(
                name: "Viewings");

            migrationBuilder.DropTable(
                name: "Visitings");

            migrationBuilder.DropTable(
                name: "WorkingHours");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Institutions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
