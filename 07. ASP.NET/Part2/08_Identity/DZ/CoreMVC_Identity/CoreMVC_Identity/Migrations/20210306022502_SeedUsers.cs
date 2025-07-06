using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreMVC_Identity.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Users
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'0c183e6e-570a-4592-a020-862dcbef1e3e', NULL, NULL, N'alex@ya.ru', N'ALEX@YA.RU', N'alex@ya.ru', N'ALEX@YA.RU', 0, N'AQAAAAEAACcQAAAAEKyHtW0Z/COH6jA0pYa6unzUKA1ws5KhX0wlNoN92GBBY6E5/uRl5IapUseZNIkRhg==', N'TBCKEU5C3T72W4XKU23NBN7ECP7ABGSA', N'4d45bc7a-4188-4117-885a-93304dac1b29', NULL, 0, 0, NULL, 1, 0)
                INSERT INTO [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'0ded8454-9f60-4810-ac8e-828f56d21058', NULL, NULL, N'admin@ya.ru', N'ADMIN@YA.RU', N'admin@ya.ru', N'ADMIN@YA.RU', 0, N'AQAAAAEAACcQAAAAEBxoPfPy+2KphydO9qXhbkgYlNvZHq25K2ybdKZqz/Sp03h/TTLKYWfXX6eh/Y9Prg==', N'6RZ5PQQMDO7QLA5HHEZCRQWUDLBFBQHD', N'b5b579ae-b200-4ff6-b748-ace6db7311c3', NULL, 0, 0, NULL, 1, 0)
            ");

            // Roles
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'20a0dd37-4265-416a-a9c5-49c1802d9a86', N'Admin', N'ADMIN', N'7ccbde24-fd23-496c-9e3f-8e89cf3427ae')
            ");

            // UserRoles
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0ded8454-9f60-4810-ac8e-828f56d21058', N'20a0dd37-4265-416a-a9c5-49c1802d9a86')
            ");

            // UserClaims
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [dbo].[AspNetUserClaims] ON
                INSERT INTO [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (1, N'0ded8454-9f60-4810-ac8e-828f56d21058', N'Position', N'Admin')
                SET IDENTITY_INSERT [dbo].[AspNetUserClaims] OFF
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
