using Microsoft.EntityFrameworkCore.Migrations;

namespace Main.Migrations
{
    public partial class SeedData : Migration
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

            migrationBuilder.Sql(@"
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'52acf8c2-9ab1-4980-a5b4-82792530f4c1', N'Admin', N'ADMIN', N'ed93845c-a3ab-4df7-a0d2-6ec8a220207a')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'95529ff5-cbc4-4596-bb62-73379eb123fc', N'User', N'USER', N'c4a45dc4-27f2-4367-a6f4-63057ddca5f9')");
            
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1edb50c3-7e0e-447f-9248-a84432744480', N'Anna', N'ANNA', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEFHsFTJBPG7zLy7EIH2RYVhbwymNwEaTJRe5tFNfV4vYOXenYbLe6kRC5Kc028u3+Q==', N'X6JRWK565ZY7BZGOE7SWSMFAOXO62IUB', N'8b01b50d-0751-4444-a0e3-1fb8fd6db282', NULL, 0, 0, NULL, 1, 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'6ef04f7b-03a3-4936-9161-7432aefe520b', N'Admin', N'ADMIN', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEKKbvotqZCmrqNqjK1zfRRBKCe0zgklEpcnFANbL5FFhBqLj1eMpzKsSEgh36IW/zQ==', N'4AWMGRM3BB2CVN5NISD4SUHXD4OF24NU', N'0a956096-8296-482a-bfd5-581ae31f1866', NULL, 0, 0, NULL, 1, 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd38a399d-55b7-40c0-acc1-0bed29096259', N'Ivan', N'IVAN', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEPM2PNhmnLyDJhoIERtApD2fVf303MJqtcfMwk9ctIVT1POg2hpka0Z7GcjROdA2bw==', N'32VCYEDCWONTIC55AU5JSBHKA4XL6G5E', N'2d144e87-bfc8-4777-a7b6-7b5f2ccd9831', NULL, 0, 0, NULL, 1, 0)");

            migrationBuilder.Sql(@"
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6ef04f7b-03a3-4936-9161-7432aefe520b', N'52acf8c2-9ab1-4980-a5b4-82792530f4c1')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1edb50c3-7e0e-447f-9248-a84432744480', N'95529ff5-cbc4-4596-bb62-73379eb123fc')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6ef04f7b-03a3-4936-9161-7432aefe520b', N'95529ff5-cbc4-4596-bb62-73379eb123fc')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd38a399d-55b7-40c0-acc1-0bed29096259', N'95529ff5-cbc4-4596-bb62-73379eb123fc')");

            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[Categories] ON
INSERT INTO [dbo].[Categories] ([ID], [Value]) VALUES (1, N'Пейзаж')
INSERT INTO [dbo].[Categories] ([ID], [Value]) VALUES (2, N'Портрет')
INSERT INTO [dbo].[Categories] ([ID], [Value]) VALUES (3, N'Натюрморт')
INSERT INTO [dbo].[Categories] ([ID], [Value]) VALUES (4, N'Автомобили')
SET IDENTITY_INSERT [dbo].[Categories] OFF");
            
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[Pictures] ON
INSERT INTO [dbo].[Pictures] ([ID], [CreationDate], [Description], [Caption], [ImgSrc], [CategoryID], [UserID]) VALUES (1, N'2021-08-25 00:00:00', N'Задача организации, в особенности же сложившаяся структура организации обеспечивает широкому кругу(специалистов) участие в формировании новых предложений. Идейные соображения высшего порядка позволяют оценить значение соответствующий условий активизации.', N'Прекрасный луг', N'/Images/1.jpg', 1, N'6ef04f7b-03a3-4936-9161-7432aefe520b')
INSERT INTO [dbo].[Pictures] ([ID], [CreationDate], [Description], [Caption], [ImgSrc], [CategoryID], [UserID]) VALUES (2, N'2021-08-20 00:00:00', N'С другой стороны новая модель организационной деятельности влечет за собой процесс внедрения и модернизации форм развития. Задача организации, в особенности же сложившаяся структура организации представляет собой интересный эксперимент проверки модели развития.', N'Розовый закат', N'/Images/2.jpg', 1, N'1edb50c3-7e0e-447f-9248-a84432744480')
INSERT INTO [dbo].[Pictures] ([ID], [CreationDate], [Description], [Caption], [ImgSrc], [CategoryID], [UserID]) VALUES (3, N'2015-02-25 00:00:00', N'Не следует, однако забывать, что сложившаяся структура организации позволяет оценить значение существенных финансовых и административных условий.', N'Мощный джип', N'/Images/3.jpg', 4, N'1edb50c3-7e0e-447f-9248-a84432744480')
INSERT INTO [dbo].[Pictures] ([ID], [CreationDate], [Description], [Caption], [ImgSrc], [CategoryID], [UserID]) VALUES (4, N'2020-10-12 00:00:00', N'Разнообразный и богатый опыт постоянный количественный рост и сфера нашей активности обеспечивает широкому кругу (специалистов) участие в формировании системы обучения кадров, соответствует насущным потребностям.', N'Мона Лиза', N'/Images/4.jpg', 2, N'd38a399d-55b7-40c0-acc1-0bed29096259')
INSERT INTO [dbo].[Pictures] ([ID], [CreationDate], [Description], [Caption], [ImgSrc], [CategoryID], [UserID]) VALUES (5, N'2017-08-26 00:00:00', N'Не следует, однако забывать, что сложившаяся структура организации представляет собой интересный эксперимент проверки системы обучения кадров, соответствует насущным потребностям.', N'Вкусные фрукты', N'/Images/5.jpg', 3, N'd38a399d-55b7-40c0-acc1-0bed29096259')
INSERT INTO [dbo].[Pictures] ([ID], [CreationDate], [Description], [Caption], [ImgSrc], [CategoryID], [UserID]) VALUES (6, N'2021-08-23 00:00:00', N'Не следует, однако забывать, что консультация с широким активом влечет за собой процесс внедрения и модернизации дальнейших направлений развития. Таким образом рамки и место обучения кадров обеспечивает широкому кругу (специалистов) участие в формировании системы обучения кадров.', N'Изящный натюрморт', N'/Images/6.jpg', 3, N'1edb50c3-7e0e-447f-9248-a84432744480')
INSERT INTO [dbo].[Pictures] ([ID], [CreationDate], [Description], [Caption], [ImgSrc], [CategoryID], [UserID]) VALUES (7, N'2021-05-21 00:00:00', N'Идейные соображения высшего порядка, а также укрепление и развитие структуры обеспечивает широкому кругу (специалистов) участие в формировании системы обучения кадров, соответствует насущным потребностям.', N'Яблоки и бутылка', N'/Images/7.jpg', 3, N'6ef04f7b-03a3-4936-9161-7432aefe520b')
INSERT INTO [dbo].[Pictures] ([ID], [CreationDate], [Description], [Caption], [ImgSrc], [CategoryID], [UserID]) VALUES (8, N'2021-08-25 00:00:00', N'Задача организации, в особенности же сложившаяся структура организации способствует подготовки и реализации системы обучения кадров, соответствует насущным потребностям.', N'Опасный бандит', N'/Images/8.jpg', 2, N'd38a399d-55b7-40c0-acc1-0bed29096259')
INSERT INTO [dbo].[Pictures] ([ID], [CreationDate], [Description], [Caption], [ImgSrc], [CategoryID], [UserID]) VALUES (9, N'2021-03-20 00:00:00', N'Идейные соображения высшего порядка, а также новая модель организационной деятельности играет важную роль в формировании систем массового участия.', N'Ягодки', N'/Images/9.jpg', 3, N'd38a399d-55b7-40c0-acc1-0bed29096259')
SET IDENTITY_INSERT [dbo].[Pictures] OFF");
            
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[Comments] ON" +
$" INSERT INTO [dbo].[Comments] ([ID], [Content], [Date], [PictureID], [UserID]) VALUES (1, N'Классный закат!!!', N'2021-08-28 22:46:11', 2, N'd38a399d-55b7-40c0-acc1-0bed29096259')" +
$" INSERT INTO [dbo].[Comments] ([ID], [Content], [Date], [PictureID], [UserID]) VALUES (2, N'У этого комментария\nнесколько строк!\n:)', N'2021-08-28 22:46:36', 2, N'd38a399d-55b7-40c0-acc1-0bed29096259')" +
$" INSERT INTO [dbo].[Comments] ([ID], [Content], [Date], [PictureID], [UserID]) VALUES (3, N'максимально длинный комментарий максимально длинный комментарий максимально длинный комментарий максимально длинный комментарий максимально длинный комментарий максимально длинный комментарий\n(200)...', N'2021-08-28 22:48:23', 2, N'1edb50c3-7e0e-447f-9248-a84432744480')" +
$" INSERT INTO [dbo].[Comments] ([ID], [Content], [Date], [PictureID], [UserID]) VALUES (4, N'максимально длинный комментарий\nмаксимально длинный комментарий\nмаксимально длинный комментарий\nмаксимально длинный комментарий\nмаксимально длинный комментарий\nмаксимально длинный комментарий\nконец...', N'2021-08-28 22:49:12', 2, N'1edb50c3-7e0e-447f-9248-a84432744480')" +
$" SET IDENTITY_INSERT [dbo].[Comments] OFF");
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
