using Microsoft.EntityFrameworkCore.Migrations;

namespace Main.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[Categories] ON
INSERT INTO [dbo].[Categories] ([ID], [Value]) VALUES (1, N'Пейзаж')
INSERT INTO [dbo].[Categories] ([ID], [Value]) VALUES (2, N'Портрет')
INSERT INTO [dbo].[Categories] ([ID], [Value]) VALUES (3, N'Натюрморт')
INSERT INTO [dbo].[Categories] ([ID], [Value]) VALUES (4, N'Автомобили')
SET IDENTITY_INSERT [dbo].[Categories] OFF");

            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[Pictures] ON
INSERT INTO [dbo].[Pictures] ([ID], [CreationDate], [Description], [Caption], [ImgSrc], [CategoryID]) VALUES (1, N'2021-08-25 20:42:00', N'Задача организации, в особенности же сложившаяся структура организации обеспечивает широкому кругу(специалистов) участие в формировании новых предложений. Идейные соображения высшего порядка позволяют оценить значение соответствующий условий активизации.', N'Прекрасный луг', N'/Images/1.jpg', 1)
INSERT INTO [dbo].[Pictures] ([ID], [CreationDate], [Description], [Caption], [ImgSrc], [CategoryID]) VALUES (2, N'2021-08-20 20:42:00', N'С другой стороны новая модель организационной деятельности влечет за собой процесс внедрения и модернизации форм развития. Задача организации, в особенности же сложившаяся структура организации представляет собой интересный эксперимент проверки модели развития.', N'Розовый закат', N'/Images/2.jpg', 1)
INSERT INTO [dbo].[Pictures] ([ID], [CreationDate], [Description], [Caption], [ImgSrc], [CategoryID]) VALUES (3, N'2015-02-25 20:42:00', N'Не следует, однако забывать, что сложившаяся структура организации позволяет оценить значение существенных финансовых и административных условий.', N'Мощный джип', N'/Images/3.jpg', 4)
INSERT INTO [dbo].[Pictures] ([ID], [CreationDate], [Description], [Caption], [ImgSrc], [CategoryID]) VALUES (4, N'2020-10-12 20:42:00', N'Разнообразный и богатый опыт постоянный количественный рост и сфера нашей активности обеспечивает широкому кругу (специалистов) участие в формировании системы обучения кадров, соответствует насущным потребностям.', N'Мона Лиза', N'/Images/4.jpg', 2)
INSERT INTO [dbo].[Pictures] ([ID], [CreationDate], [Description], [Caption], [ImgSrc], [CategoryID]) VALUES (5, N'2017-08-26 20:42:00', N'Не следует, однако забывать, что сложившаяся структура организации представляет собой интересный эксперимент проверки системы обучения кадров, соответствует насущным потребностям.', N'Вкусные фрукты', N'/Images/5.jpg', 3)
INSERT INTO [dbo].[Pictures] ([ID], [CreationDate], [Description], [Caption], [ImgSrc], [CategoryID]) VALUES (6, N'2021-08-23 20:42:00', N'Не следует, однако забывать, что консультация с широким активом влечет за собой процесс внедрения и модернизации дальнейших направлений развития. Таким образом рамки и место обучения кадров обеспечивает широкому кругу (специалистов) участие в формировании системы обучения кадров.', N'Изящный натюрморт', N'/Images/6.jpg', 3)
INSERT INTO [dbo].[Pictures] ([ID], [CreationDate], [Description], [Caption], [ImgSrc], [CategoryID]) VALUES (7, N'2021-05-21 20:42:00', N'Идейные соображения высшего порядка, а также укрепление и развитие структуры обеспечивает широкому кругу (специалистов) участие в формировании системы обучения кадров, соответствует насущным потребностям.', N'Яблоки и бутылка', N'/Images/7.jpg', 3)
INSERT INTO [dbo].[Pictures] ([ID], [CreationDate], [Description], [Caption], [ImgSrc], [CategoryID]) VALUES (8, N'2021-08-25 20:42:00', N'Задача организации, в особенности же сложившаяся структура организации способствует подготовки и реализации системы обучения кадров, соответствует насущным потребностям.', N'Опасный бандит', N'/Images/8.jpg', 2)
INSERT INTO [dbo].[Pictures] ([ID], [CreationDate], [Description], [Caption], [ImgSrc], [CategoryID]) VALUES (9, N'2021-03-20 20:42:00', N'Идейные соображения высшего порядка, а также новая модель организационной деятельности играет важную роль в формировании систем массового участия.', N'Ягодки', N'/Images/9.jpg', 3)
SET IDENTITY_INSERT [dbo].[Pictures] OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
