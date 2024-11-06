using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DenemeTakipAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dersler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DersAdi = table.Column<string>(type: "text", nullable: false),
                    IsTyt = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dersler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: true),
                    ProviderKey = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AytDenemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MatematikDogru = table.Column<int>(type: "integer", nullable: false),
                    MatematikYanlis = table.Column<int>(type: "integer", nullable: false),
                    FizikDogru = table.Column<int>(type: "integer", nullable: false),
                    FizikYanlis = table.Column<int>(type: "integer", nullable: false),
                    KimyaDogru = table.Column<int>(type: "integer", nullable: false),
                    KimyaYanlis = table.Column<int>(type: "integer", nullable: false),
                    BiyolojiDogru = table.Column<int>(type: "integer", nullable: false),
                    BiyolojiYanlis = table.Column<int>(type: "integer", nullable: false),
                    EdebiyatDogru = table.Column<int>(type: "integer", nullable: false),
                    EdebiyatYanlis = table.Column<int>(type: "integer", nullable: false),
                    Cografya1Dogru = table.Column<int>(type: "integer", nullable: false),
                    Cografya1Yanlis = table.Column<int>(type: "integer", nullable: false),
                    Tarih1Dogru = table.Column<int>(type: "integer", nullable: false),
                    Tarih1Yanlis = table.Column<int>(type: "integer", nullable: false),
                    Cografya2Dogru = table.Column<int>(type: "integer", nullable: false),
                    Cografya2Yanlis = table.Column<int>(type: "integer", nullable: false),
                    Tarih2Dogru = table.Column<int>(type: "integer", nullable: false),
                    Tarih2Yanlis = table.Column<int>(type: "integer", nullable: false),
                    DinDogru = table.Column<int>(type: "integer", nullable: false),
                    DinYanlis = table.Column<int>(type: "integer", nullable: false),
                    FelsefeDogru = table.Column<int>(type: "integer", nullable: false),
                    FelsefeYanlis = table.Column<int>(type: "integer", nullable: false),
                    DilDogru = table.Column<int>(type: "integer", nullable: false),
                    DilYanlis = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AytDenemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AytDenemes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TytDenemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TurkceDogru = table.Column<int>(type: "integer", nullable: false),
                    TurkceYanlis = table.Column<int>(type: "integer", nullable: false),
                    MatematikDogru = table.Column<int>(type: "integer", nullable: false),
                    MatematikYanlis = table.Column<int>(type: "integer", nullable: false),
                    FenDogru = table.Column<int>(type: "integer", nullable: false),
                    FenYanlis = table.Column<int>(type: "integer", nullable: false),
                    SosyalDogru = table.Column<int>(type: "integer", nullable: false),
                    SosyalYanlis = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TytDenemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TytDenemes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Konular",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    KonuAdi = table.Column<string>(type: "text", nullable: false),
                    DersId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsTyt = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konular", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Konular_Dersler_DersId",
                        column: x => x.DersId,
                        principalTable: "Dersler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AytDenemeKonu",
                columns: table => new
                {
                    AytDenemesYanlisId = table.Column<Guid>(type: "uuid", nullable: false),
                    YanlisKonularId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AytDenemeKonu", x => new { x.AytDenemesYanlisId, x.YanlisKonularId });
                    table.ForeignKey(
                        name: "FK_AytDenemeKonu_AytDenemes_AytDenemesYanlisId",
                        column: x => x.AytDenemesYanlisId,
                        principalTable: "AytDenemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AytDenemeKonu_Konular_YanlisKonularId",
                        column: x => x.YanlisKonularId,
                        principalTable: "Konular",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AytDenemeKonu1",
                columns: table => new
                {
                    AytDenemesBosId = table.Column<Guid>(type: "uuid", nullable: false),
                    BosKonularId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AytDenemeKonu1", x => new { x.AytDenemesBosId, x.BosKonularId });
                    table.ForeignKey(
                        name: "FK_AytDenemeKonu1_AytDenemes_AytDenemesBosId",
                        column: x => x.AytDenemesBosId,
                        principalTable: "AytDenemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AytDenemeKonu1_Konular_BosKonularId",
                        column: x => x.BosKonularId,
                        principalTable: "Konular",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KonuTytDeneme",
                columns: table => new
                {
                    BosKonularId = table.Column<Guid>(type: "uuid", nullable: false),
                    TytDenemesBosId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KonuTytDeneme", x => new { x.BosKonularId, x.TytDenemesBosId });
                    table.ForeignKey(
                        name: "FK_KonuTytDeneme_Konular_BosKonularId",
                        column: x => x.BosKonularId,
                        principalTable: "Konular",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KonuTytDeneme_TytDenemes_TytDenemesBosId",
                        column: x => x.TytDenemesBosId,
                        principalTable: "TytDenemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KonuTytDeneme1",
                columns: table => new
                {
                    TytDenemesYanlisId = table.Column<Guid>(type: "uuid", nullable: false),
                    YanlisKonularId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KonuTytDeneme1", x => new { x.TytDenemesYanlisId, x.YanlisKonularId });
                    table.ForeignKey(
                        name: "FK_KonuTytDeneme1_Konular_YanlisKonularId",
                        column: x => x.YanlisKonularId,
                        principalTable: "Konular",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KonuTytDeneme1_TytDenemes_TytDenemesYanlisId",
                        column: x => x.TytDenemesYanlisId,
                        principalTable: "TytDenemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserKonular",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    KonuId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserKonular", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserKonular_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserKonular_Konular_KonuId",
                        column: x => x.KonuId,
                        principalTable: "Konular",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "128f0e53-f259-411a-b4be-e050e48c199e", "90916599-5e1c-4029-95b6-2795de639a1e", "user", "USER" },
                    { "a55c5f9f-4f8c-4848-882f-0bcb3ec62171", "7531cfe8-d4bf-46f2-a2f7-9ab0237dec0b", "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenEndDate", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c5bc8bb5-0f4f-452a-911c-9844f7e2aac7", 0, "f7053488-c18f-415f-89f7-35df3524784b", "admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEIl8Wza3qjQKShoK5z1mJA3HHB+LtoGiYifABBupUgEByQCT9uxCoDfXHY63D/8N8g==", null, false, null, null, "013c44e9-7da5-4853-be15-64b1918ab684", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[] { "a55c5f9f-4f8c-4848-882f-0bcb3ec62171", "c5bc8bb5-0f4f-452a-911c-9844f7e2aac7", "AppUserRole" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AytDenemeKonu_YanlisKonularId",
                table: "AytDenemeKonu",
                column: "YanlisKonularId");

            migrationBuilder.CreateIndex(
                name: "IX_AytDenemeKonu1_BosKonularId",
                table: "AytDenemeKonu1",
                column: "BosKonularId");

            migrationBuilder.CreateIndex(
                name: "IX_AytDenemes_UserId",
                table: "AytDenemes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Konular_DersId",
                table: "Konular",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_KonuTytDeneme_TytDenemesBosId",
                table: "KonuTytDeneme",
                column: "TytDenemesBosId");

            migrationBuilder.CreateIndex(
                name: "IX_KonuTytDeneme1_YanlisKonularId",
                table: "KonuTytDeneme1",
                column: "YanlisKonularId");

            migrationBuilder.CreateIndex(
                name: "IX_TytDenemes_UserId",
                table: "TytDenemes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserKonular_KonuId",
                table: "UserKonular",
                column: "KonuId");

            migrationBuilder.CreateIndex(
                name: "IX_UserKonular_UserId_KonuId",
                table: "UserKonular",
                columns: new[] { "UserId", "KonuId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AytDenemeKonu");

            migrationBuilder.DropTable(
                name: "AytDenemeKonu1");

            migrationBuilder.DropTable(
                name: "IdentityUserLogin");

            migrationBuilder.DropTable(
                name: "KonuTytDeneme");

            migrationBuilder.DropTable(
                name: "KonuTytDeneme1");

            migrationBuilder.DropTable(
                name: "UserKonular");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AytDenemes");

            migrationBuilder.DropTable(
                name: "TytDenemes");

            migrationBuilder.DropTable(
                name: "Konular");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Dersler");
        }
    }
}
