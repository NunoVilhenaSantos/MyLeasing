﻿#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace MyLeasing.Web.Migrations.MySQL;

/// <inheritdoc />
public partial class UserDBUpdate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "AspNetRoles",
                table => new
                {
                    Id = table.Column<string>("varchar(255)", nullable: false),
                    Name = table.Column<string>("varchar(256)", maxLength: 256,
                        nullable: true),
                    NormalizedName = table.Column<string>("varchar(256)",
                        maxLength: 256, nullable: true),
                    ConcurrencyStamp =
                        table.Column<string>("longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "AspNetUsers",
                table => new
                {
                    Id = table.Column<string>("varchar(255)", nullable: false),
                    Document = table.Column<string>("varchar(20)",
                        maxLength: 20, nullable: false),
                    FirstName = table.Column<string>("varchar(50)",
                        maxLength: 50, nullable: false),
                    LastName = table.Column<string>("varchar(50)",
                        maxLength: 50, nullable: false),
                    Address = table.Column<string>("varchar(100)",
                        maxLength: 100, nullable: true),
                    FixedPhone =
                        table.Column<string>("longtext", nullable: true),
                    CellPhone =
                        table.Column<string>("longtext", nullable: true),
                    ProfilePhotoId =
                        table.Column<Guid>("char(36)", nullable: false),
                    UserName = table.Column<string>("varchar(256)",
                        maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>("varchar(256)",
                        maxLength: 256, nullable: true),
                    Email = table.Column<string>("varchar(256)", maxLength: 256,
                        nullable: true),
                    NormalizedEmail = table.Column<string>("varchar(256)",
                        maxLength: 256, nullable: true),
                    EmailConfirmed =
                        table.Column<bool>("tinyint(1)", nullable: false),
                    PasswordHash =
                        table.Column<string>("longtext", nullable: true),
                    SecurityStamp =
                        table.Column<string>("longtext", nullable: true),
                    ConcurrencyStamp =
                        table.Column<string>("longtext", nullable: true),
                    PhoneNumber =
                        table.Column<string>("longtext", nullable: true),
                    PhoneNumberConfirmed =
                        table.Column<bool>("tinyint(1)", nullable: false),
                    TwoFactorEnabled =
                        table.Column<bool>("tinyint(1)", nullable: false),
                    LockoutEnd =
                        table.Column<DateTimeOffset>("datetime(6)",
                            nullable: true),
                    LockoutEnabled =
                        table.Column<bool>("tinyint(1)", nullable: false),
                    AccessFailedCount =
                        table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "AspNetRoleClaims",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy",
                            MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId =
                        table.Column<string>("varchar(255)", nullable: false),
                    ClaimType =
                        table.Column<string>("longtext", nullable: true),
                    ClaimValue =
                        table.Column<string>("longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        x => x.RoleId,
                        "AspNetRoles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "AspNetUserClaims",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy",
                            MySQLValueGenerationStrategy.IdentityColumn),
                    UserId =
                        table.Column<string>("varchar(255)", nullable: false),
                    ClaimType =
                        table.Column<string>("longtext", nullable: true),
                    ClaimValue =
                        table.Column<string>("longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        "FK_AspNetUserClaims_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "AspNetUserLogins",
                table => new
                {
                    LoginProvider =
                        table.Column<string>("varchar(255)", nullable: false),
                    ProviderKey =
                        table.Column<string>("varchar(255)", nullable: false),
                    ProviderDisplayName =
                        table.Column<string>("longtext", nullable: true),
                    UserId =
                        table.Column<string>("varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins",
                        x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        "FK_AspNetUserLogins_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "AspNetUserRoles",
                table => new
                {
                    UserId =
                        table.Column<string>("varchar(255)", nullable: false),
                    RoleId =
                        table.Column<string>("varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles",
                        x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        x => x.RoleId,
                        "AspNetRoles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_AspNetUserRoles_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "AspNetUserTokens",
                table => new
                {
                    UserId =
                        table.Column<string>("varchar(255)", nullable: false),
                    LoginProvider =
                        table.Column<string>("varchar(255)", nullable: false),
                    Name =
                        table.Column<string>("varchar(255)", nullable: false),
                    Value = table.Column<string>("longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens",
                        x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        "FK_AspNetUserTokens_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "Lessees",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy",
                            MySQLValueGenerationStrategy.IdentityColumn),
                    WasDeleted =
                        table.Column<bool>("tinyint(1)", nullable: false),
                    Document = table.Column<string>("varchar(20)",
                        maxLength: 20, nullable: false),
                    FirstName = table.Column<string>("varchar(50)",
                        maxLength: 50, nullable: false),
                    LastName = table.Column<string>("varchar(50)",
                        maxLength: 50, nullable: false),
                    ProfilePhotoUrl =
                        table.Column<string>("longtext", nullable: true),
                    ProfilePhotoId =
                        table.Column<Guid>("char(36)", nullable: false),
                    FixedPhone =
                        table.Column<string>("longtext", nullable: true),
                    CellPhone =
                        table.Column<string>("longtext", nullable: true),
                    Address = table.Column<string>("varchar(100)",
                        maxLength: 100, nullable: true),
                    UserId =
                        table.Column<string>("varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessees", x => x.Id);
                    table.ForeignKey(
                        "FK_Lessees_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "Owners",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy",
                            MySQLValueGenerationStrategy.IdentityColumn),
                    WasDeleted =
                        table.Column<bool>("tinyint(1)", nullable: false),
                    Document =
                        table.Column<string>("longtext", nullable: false),
                    FirstName =
                        table.Column<string>("longtext", nullable: false),
                    LastName =
                        table.Column<string>("longtext", nullable: false),
                    ProfilePhotoUrl =
                        table.Column<string>("longtext", nullable: true),
                    ProfilePhotoId =
                        table.Column<Guid>("char(36)", nullable: false),
                    FixedPhone =
                        table.Column<string>("longtext", nullable: true),
                    CellPhone =
                        table.Column<string>("longtext", nullable: true),
                    Address = table.Column<string>("longtext", nullable: true),
                    UserId =
                        table.Column<string>("varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                    table.ForeignKey(
                        "FK_Owners_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateIndex(
            "IX_AspNetRoleClaims_RoleId",
            "AspNetRoleClaims",
            "RoleId");

        migrationBuilder.CreateIndex(
            "RoleNameIndex",
            "AspNetRoles",
            "NormalizedName",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_AspNetUserClaims_UserId",
            "AspNetUserClaims",
            "UserId");

        migrationBuilder.CreateIndex(
            "IX_AspNetUserLogins_UserId",
            "AspNetUserLogins",
            "UserId");

        migrationBuilder.CreateIndex(
            "IX_AspNetUserRoles_RoleId",
            "AspNetUserRoles",
            "RoleId");

        migrationBuilder.CreateIndex(
            "EmailIndex",
            "AspNetUsers",
            "NormalizedEmail");

        migrationBuilder.CreateIndex(
            "UserNameIndex",
            "AspNetUsers",
            "NormalizedUserName",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Lessees_UserId",
            "Lessees",
            "UserId");

        migrationBuilder.CreateIndex(
            "IX_Owners_UserId",
            "Owners",
            "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "AspNetRoleClaims");

        migrationBuilder.DropTable(
            "AspNetUserClaims");

        migrationBuilder.DropTable(
            "AspNetUserLogins");

        migrationBuilder.DropTable(
            "AspNetUserRoles");

        migrationBuilder.DropTable(
            "AspNetUserTokens");

        migrationBuilder.DropTable(
            "Lessees");

        migrationBuilder.DropTable(
            "Owners");

        migrationBuilder.DropTable(
            "AspNetRoles");

        migrationBuilder.DropTable(
            "AspNetUsers");
    }
}