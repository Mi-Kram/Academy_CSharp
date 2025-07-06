using Microsoft.EntityFrameworkCore.Migrations;

namespace Main.Migrations
{
    public partial class SeedRolesUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'43acb328-0a20-4022-ae15-d580f5d7f419', N'Master', N'MASTER', N'd2434727-3fde-41af-8c69-b75812ccd49b')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'aaafe402-35a9-47d6-b8bb-3981160a36d6', N'Admin', N'ADMIN', N'c9f52fdb-4518-4f78-9d4b-e5727a613935')");

            // Masters: qwerty
            // Admins: qwertyz
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [PhotoPath], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'7aba73cc-f1fd-42fa-9332-f4914b2f0cbf', N'/Images/UserPhotos/7aba73cc-f1fd-42fa-9332-f4914b2f0cbf.jpg', N'Anna', N'ANNA', NULL, NULL, 0, N'AQAAAAEAACcQAAAAECes4zpjrlRnvwR3lEzQZeIVgrUulAT1cOwt4RB7LaZfbeSJCUQtBaRxMvTyEFY3sg==', N'OFWGRWLSJVTSY2ZL65WXE5ALSTES57HD', N'fa3a4622-88ea-480a-8ede-551d102237ef', NULL, 0, 0, NULL, 1, 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [PhotoPath], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'82ff7d50-f90a-450d-ae5e-208ad1abc0c0', N'/Images/UserPhotos/82ff7d50-f90a-450d-ae5e-208ad1abc0c0.jpg', N'Admin', N'ADMIN', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEAA/rd+QpJknXilt32U+Ub1uk129/SndX87xG+xqkcqMELEZOzbHsP3C476yA7PcDg==', N'4DEGBVWBBD5SHKG57ZJBZEDNN4EJSQM4', N'fdb467b1-7dae-48a2-95b0-554662a05623', NULL, 0, 0, NULL, 1, 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [PhotoPath], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd78907cd-6527-4ccf-830a-eb0c4bc855fa', N'/Images/UserPhotos/d78907cd-6527-4ccf-830a-eb0c4bc855fa.jpg', N'Ivan', N'IVAN', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEJLpLF0CuqClAEfJVYY1pbkUjjbejcAyTnMpzvBbk6psw6C9xvwgQkz9zCyiieDFxQ==', N'YKCXAMGVR5FHUNY5D76MLTD772SXLLQ4', N'20265455-eadf-435a-9662-336b0d3e0453', NULL, 0, 0, NULL, 1, 0)");

            migrationBuilder.Sql(@"
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7aba73cc-f1fd-42fa-9332-f4914b2f0cbf', N'43acb328-0a20-4022-ae15-d580f5d7f419')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd78907cd-6527-4ccf-830a-eb0c4bc855fa', N'43acb328-0a20-4022-ae15-d580f5d7f419')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'82ff7d50-f90a-450d-ae5e-208ad1abc0c0', N'aaafe402-35a9-47d6-b8bb-3981160a36d6')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
