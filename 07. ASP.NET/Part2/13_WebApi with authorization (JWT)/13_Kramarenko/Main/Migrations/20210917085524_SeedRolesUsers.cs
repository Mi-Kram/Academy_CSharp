using Microsoft.EntityFrameworkCore.Migrations;

namespace Main.Migrations
{
    public partial class SeedRolesUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'ebf3edc0-30b5-4ad1-b4de-5fae4354b1dd', N'Admin', N'ADMIN', N'376afc6e-6d57-4600-91bc-6acdba3b8d2f')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'fd7d9b74-c899-4fb3-a87b-b28eaebb3817', N'User', N'USER', N'793e976a-0954-4c97-8cd3-4bb1c5be57a3')");

            // Admin, qwertyz
            // User, qwerty
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'51b438ed-67cb-432f-a4f4-d6cee32a924f', N'Ivan', N'IVAN', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEP+swM2WWR56GJ3Qcdo5rdUHUZZtCZQ6KtKCYlGbXgVwfunSX/miWONkJyVkjzfgZg==', N'A7GJQNV5KYIXEDJEAEI24NICLOBNJAKV', N'c731b6cb-228d-4850-b3d6-1418e9a4f564', NULL, 0, 0, NULL, 1, 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'72e4c4f7-149a-4f45-b1fe-166a0a331054', N'Admin', N'ADMIN', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEPE8eyM8CR0u0zRkv7VzKEmGon2OWFDvGfCTZkvHi/7/peD8NplSj+Sb9xqKjZ3Nhw==', N'ETJW7EPVM5P6NQMFTVQ3KZAI25XWY4AP', N'b95af3c1-f1ca-4b3d-adc3-e7eef6e1d588', NULL, 0, 0, NULL, 1, 0)");

            migrationBuilder.Sql(@"
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'72e4c4f7-149a-4f45-b1fe-166a0a331054', N'ebf3edc0-30b5-4ad1-b4de-5fae4354b1dd')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'51b438ed-67cb-432f-a4f4-d6cee32a924f', N'fd7d9b74-c899-4fb3-a87b-b28eaebb3817')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
