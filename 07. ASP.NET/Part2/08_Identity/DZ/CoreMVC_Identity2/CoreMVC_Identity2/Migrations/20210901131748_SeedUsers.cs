using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreMVC_Identity2.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            // добавление ролей
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'8fccd088-fabb-4ca0-9565-995a64bab6cc', N'Admin', N'ADMIN', N'888ab298-5152-45f8-9883-8a6e77f953f6')
            ");

            // добавление пользователей
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName]) VALUES (N'205af406-a113-4fea-8589-42ddb787c9cb', N'admin@ya.ru', N'ADMIN@YA.RU', N'admin@ya.ru', N'ADMIN@YA.RU', 0, N'AQAAAAEAACcQAAAAEB3cfYm/LmNBb8ZxhNIxodtFGJSjQS701qBtC4Q1MjpUjgB0dRJHUpUD0mYPpEUA8Q==', N'4J4RTXIXZDSKUKK6E5BTG4WAA3IGZTJL', N'5f3aeecc-c78d-46de-9d27-fc1d42502593', NULL, 0, 0, NULL, 1, 0, N'Admin', N'Admin')
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName]) VALUES (N'76b73bf9-6a3b-4b88-bce8-b6ad7e3c4cb3', N'alex@ya.ru', N'ALEX@YA.RU', N'alex@ya.ru', N'ALEX@YA.RU', 1, N'AQAAAAEAACcQAAAAEOznuaX+kDjOWEHo8f6ikzaqpISH2RhWKUepFGd5z5OZFlWXaQqBaPWvKijWpbXAyA==', N'HGXUCEKV46ZA6LVLILKRR3C4A5LN66QO', N'f9906c60-7512-44b3-8061-9050cdae4ce1', NULL, 0, 0, NULL, 1, 0, N'Alex', N'Petrov')
            ");

            // добавление пользователей в роли
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'205af406-a113-4fea-8589-42ddb787c9cb', N'8fccd088-fabb-4ca0-9565-995a64bab6cc')
            ");

            // добавление параметров Claims
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] ON
INSERT INTO [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (1, N'205af406-a113-4fea-8589-42ddb787c9cb', N'Position', N'Admin')
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] OFF
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
