namespace Main.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class SeedData1 : DbMigration
  {
    public override void Up()
    {
      Sql(@"
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'User')
INSERT INTO[dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'2', N'Admin')"
);

      Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ImgSrc]) VALUES (N'308dc668-4c55-45ab-a1d8-6bc02f68d4e1', N'Roma', 0, N'ADmSHnIH275GSMOH7wRWjvfQiaoNR1T4KRfeYJSH88pPPh/i8IGZQU8XBRUCRSzHTg==', N'dc686af6-70d6-42f2-a78f-ce3acea90962', NULL, 0, 0, NULL, 1, 0, N'Roma', N'Images/People/308dc668-4c55-45ab-a1d8-6bc02f68d4e1.jpg')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ImgSrc]) VALUES (N'3cbfc109-b196-4bce-af96-3e693fb4a6f7', N'Ivan', 0, N'AIP2+sXep6yMiH0mjkuGEX3xKD5OSe0Z1qGeeqrlZhb3AaeWHfO0I8rTkR+CqD1LWg==', N'adf51162-2813-44a1-8be0-549eef6fdaf2', NULL, 0, 0, NULL, 1, 0, N'Ivan', N'Images/People/3cbfc109-b196-4bce-af96-3e693fb4a6f7.jpg')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ImgSrc]) VALUES (N'924a5686-e930-4853-bad1-c7a6b9d7755f', N'Julia', 0, N'ABoYnie7wkL4btaihhkG9DjGT2DQTvy5ekqN55ObadIeo8mRBsuTJCOvwSktbJ0ABQ==', N'8d9acc3a-3e3b-4ba1-a8e5-44f09a37fd1b', NULL, 0, 0, NULL, 1, 0, N'Julia', N'Images/People/924a5686-e930-4853-bad1-c7a6b9d7755f.jpg')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ImgSrc]) VALUES (N'd24c0599-5e80-48e5-8927-e3d43d04c32b', N'Petr', 0, N'AKdrcb5gv85Km7OPN+Rna/jtbuxpBY931+hGX7jVBsZxDCOtH8TdTikrPBBai5j2kA==', N'c475b9d8-91b3-456b-8b75-f1ea77469a4e', NULL, 0, 0, NULL, 1, 0, N'Petr', N'Images/People/d24c0599-5e80-48e5-8927-e3d43d04c32b.jpg')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ImgSrc]) VALUES (N'e62dabe8-fbb0-48b5-b7bb-da4851c3fff9', N'Alex', 0, N'ANef47yi1l2dJ2EGvdPZyr07ZwmCkKyxgx1QXrx8qRvgl+0qaE1rm7uDsTk6jJNEXA==', N'f216d467-dc4d-487a-9bf4-505afe3b9f1d', NULL, 0, 0, NULL, 1, 0, N'Alex', N'Images/People/e62dabe8-fbb0-48b5-b7bb-da4851c3fff9.jpg')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ImgSrc]) VALUES (N'fc1a4e91-78c4-48df-8b34-16148ee634bd', N'Anna', 0, N'AI5PX+xHCyaI8nUnoytJO2rOTopxaprSc3+kYWGNO0fRhUxDAYcwIEzyu0iMJiEH/Q==', N'4a6b9a26-cd2c-47e9-8ec4-4d388480aa06', NULL, 0, 0, NULL, 1, 0, N'Anna', N'Images/People/fc1a4e91-78c4-48df-8b34-16148ee634bd.jpg')
");

      Sql(@"
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'308dc668-4c55-45ab-a1d8-6bc02f68d4e1', N'1')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3cbfc109-b196-4bce-af96-3e693fb4a6f7', N'1')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'924a5686-e930-4853-bad1-c7a6b9d7755f', N'1')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd24c0599-5e80-48e5-8927-e3d43d04c32b', N'1')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e62dabe8-fbb0-48b5-b7bb-da4851c3fff9', N'1')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fc1a4e91-78c4-48df-8b34-16148ee634bd', N'1')
");

      Sql(@"
SET IDENTITY_INSERT [dbo].[Chats] ON
INSERT INTO [dbo].[Chats] ([ID], [Title], [IsGroupChat]) VALUES (1, N'', 0)
INSERT INTO [dbo].[Chats] ([ID], [Title], [IsGroupChat]) VALUES (2, N'', 0)
INSERT INTO [dbo].[Chats] ([ID], [Title], [IsGroupChat]) VALUES (3, N'', 0)
INSERT INTO [dbo].[Chats] ([ID], [Title], [IsGroupChat]) VALUES (4, N'', 0)
INSERT INTO [dbo].[Chats] ([ID], [Title], [IsGroupChat]) VALUES (5, N'', 0)
INSERT INTO [dbo].[Chats] ([ID], [Title], [IsGroupChat]) VALUES (6, N'', 0)
INSERT INTO [dbo].[Chats] ([ID], [Title], [IsGroupChat]) VALUES (7, N'', 0)
INSERT INTO [dbo].[Chats] ([ID], [Title], [IsGroupChat]) VALUES (8, N'', 0)
INSERT INTO [dbo].[Chats] ([ID], [Title], [IsGroupChat]) VALUES (9, N'', 0)
INSERT INTO [dbo].[Chats] ([ID], [Title], [IsGroupChat]) VALUES (10, N'', 0)
INSERT INTO [dbo].[Chats] ([ID], [Title], [IsGroupChat]) VALUES (11, N'', 0)
INSERT INTO [dbo].[Chats] ([ID], [Title], [IsGroupChat]) VALUES (12, N'', 0)
INSERT INTO [dbo].[Chats] ([ID], [Title], [IsGroupChat]) VALUES (13, N'', 0)
INSERT INTO [dbo].[Chats] ([ID], [Title], [IsGroupChat]) VALUES (14, N'', 0)
INSERT INTO [dbo].[Chats] ([ID], [Title], [IsGroupChat]) VALUES (15, N'', 0)
INSERT INTO [dbo].[Chats] ([ID], [Title], [IsGroupChat]) VALUES (16, N'My Group', 1)
INSERT INTO [dbo].[Chats] ([ID], [Title], [IsGroupChat]) VALUES (17, N'World Group', 1)
SET IDENTITY_INSERT [dbo].[Chats] OFF
");

      Sql(@"
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'e62dabe8-fbb0-48b5-b7bb-da4851c3fff9', 1)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'fc1a4e91-78c4-48df-8b34-16148ee634bd', 1)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'3cbfc109-b196-4bce-af96-3e693fb4a6f7', 2)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'e62dabe8-fbb0-48b5-b7bb-da4851c3fff9', 2)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'924a5686-e930-4853-bad1-c7a6b9d7755f', 3)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'e62dabe8-fbb0-48b5-b7bb-da4851c3fff9', 3)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'd24c0599-5e80-48e5-8927-e3d43d04c32b', 4)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'e62dabe8-fbb0-48b5-b7bb-da4851c3fff9', 4)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'308dc668-4c55-45ab-a1d8-6bc02f68d4e1', 5)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'e62dabe8-fbb0-48b5-b7bb-da4851c3fff9', 5)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'3cbfc109-b196-4bce-af96-3e693fb4a6f7', 6)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'fc1a4e91-78c4-48df-8b34-16148ee634bd', 6)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'924a5686-e930-4853-bad1-c7a6b9d7755f', 7)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'fc1a4e91-78c4-48df-8b34-16148ee634bd', 7)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'd24c0599-5e80-48e5-8927-e3d43d04c32b', 8)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'fc1a4e91-78c4-48df-8b34-16148ee634bd', 8)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'308dc668-4c55-45ab-a1d8-6bc02f68d4e1', 9)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'fc1a4e91-78c4-48df-8b34-16148ee634bd', 9)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'3cbfc109-b196-4bce-af96-3e693fb4a6f7', 10)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'924a5686-e930-4853-bad1-c7a6b9d7755f', 10)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'3cbfc109-b196-4bce-af96-3e693fb4a6f7', 11)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'd24c0599-5e80-48e5-8927-e3d43d04c32b', 11)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'308dc668-4c55-45ab-a1d8-6bc02f68d4e1', 12)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'3cbfc109-b196-4bce-af96-3e693fb4a6f7', 12)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'924a5686-e930-4853-bad1-c7a6b9d7755f', 13)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'd24c0599-5e80-48e5-8927-e3d43d04c32b', 13)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'308dc668-4c55-45ab-a1d8-6bc02f68d4e1', 14)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'924a5686-e930-4853-bad1-c7a6b9d7755f', 14)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'308dc668-4c55-45ab-a1d8-6bc02f68d4e1', 15)
INSERT INTO [dbo].[ApplicationUserChats] ([ApplicationUser_Id], [Chat_ID]) VALUES (N'd24c0599-5e80-48e5-8927-e3d43d04c32b', 15)
");

      Sql(@"
set language english;
SET IDENTITY_INSERT [dbo].[Messages] ON
INSERT INTO [dbo].[Messages] ([ID], [Content], [SendTime], [SenderID], [ChatID]) VALUES (1, N'Привет!', N'2021-07-23 11:50:16', N'3cbfc109-b196-4bce-af96-3e693fb4a6f7', 11)
INSERT INTO [dbo].[Messages] ([ID], [Content], [SendTime], [SenderID], [ChatID]) VALUES (2, N'Привет!', N'2021-07-23 11:50:20', N'd24c0599-5e80-48e5-8927-e3d43d04c32b', 11)
INSERT INTO [dbo].[Messages] ([ID], [Content], [SendTime], [SenderID], [ChatID]) VALUES (3, N'Как дела?', N'2021-07-23 11:50:26', N'd24c0599-5e80-48e5-8927-e3d43d04c32b', 11)
INSERT INTO [dbo].[Messages] ([ID], [Content], [SendTime], [SenderID], [ChatID]) VALUES (4, N'Сайт рыбатекст поможет дизайнеру, верстальщику, вебмастеру сгенерировать несколько абзацев более менее осмысленного текста рыбы на русском языке, а начинающему оратору отточить навык публичных.', N'2021-07-23 11:51:17', N'3cbfc109-b196-4bce-af96-3e693fb4a6f7', 11)
INSERT INTO [dbo].[Messages] ([ID], [Content], [SendTime], [SenderID], [ChatID]) VALUES (5, N'Сайт рыбатекст поможет дизайнеру, верстальщику, вебмастеру сгенерировать несколько абзацев более менее осмысленного текста рыбы на русском языке, а начинающему оратору отточить навык публичных.', N'2021-07-23 11:51:52', N'd24c0599-5e80-48e5-8927-e3d43d04c32b', 11)
INSERT INTO [dbo].[Messages] ([ID], [Content], [SendTime], [SenderID], [ChatID]) VALUES (6, N'По своей сути рыбатекст является альтернативой традиционному lorem ipsum, который вызывает у некторых людей недоумение при попытках прочитать рыбу текст. В отличии от lorem ipsum, текст рыба.', N'2021-07-23 11:52:17', N'd24c0599-5e80-48e5-8927-e3d43d04c32b', 11)
INSERT INTO [dbo].[Messages] ([ID], [Content], [SendTime], [SenderID], [ChatID]) VALUES (7, N'Пока', N'2021-07-23 11:52:31', N'3cbfc109-b196-4bce-af96-3e693fb4a6f7', 11)
INSERT INTO [dbo].[Messages] ([ID], [Content], [SendTime], [SenderID], [ChatID]) VALUES (8, N'Пока', N'2021-07-23 11:52:58', N'd24c0599-5e80-48e5-8927-e3d43d04c32b', 11)
INSERT INTO [dbo].[Messages] ([ID], [Content], [SendTime], [SenderID], [ChatID]) VALUES (9, N'Привет', N'2021-07-23 11:53:19', N'd24c0599-5e80-48e5-8927-e3d43d04c32b', 16)
INSERT INTO [dbo].[Messages] ([ID], [Content], [SendTime], [SenderID], [ChatID]) VALUES (10, N'По своей сути рыбатекст является альтернативой традиционному lorem ipsum, который вызывает у некторых людей недоумение при попытках прочитать рыбу текст. В отличии от lorem ipsum, текст рыба.', N'2021-07-23 11:53:33', N'3cbfc109-b196-4bce-af96-3e693fb4a6f7', 16)
INSERT INTO [dbo].[Messages] ([ID], [Content], [SendTime], [SenderID], [ChatID]) VALUES (11, N'С другой стороны новая модель организационной деятельности требуют определения и уточнения позиций, занимаемых участниками в отношении поставленных задач. Задача организации, в особенности.', N'2021-07-23 11:54:03', N'd24c0599-5e80-48e5-8927-e3d43d04c32b', 16)
INSERT INTO [dbo].[Messages] ([ID], [Content], [SendTime], [SenderID], [ChatID]) VALUES (12, N'Таким образом сложившаяся структура организации позволяет выполнять важные задания по разработке системы обучения кадров, соответствует насущным потребностям. Разнообразный и богатый опыт.', N'2021-07-23 11:54:19', N'd24c0599-5e80-48e5-8927-e3d43d04c32b', 16)
INSERT INTO [dbo].[Messages] ([ID], [Content], [SendTime], [SenderID], [ChatID]) VALUES (13, N'Мне пора', N'2021-07-23 11:54:30', N'3cbfc109-b196-4bce-af96-3e693fb4a6f7', 16)
INSERT INTO [dbo].[Messages] ([ID], [Content], [SendTime], [SenderID], [ChatID]) VALUES (14, N'Всем пока!', N'2021-07-23 11:54:34', N'3cbfc109-b196-4bce-af96-3e693fb4a6f7', 16)
INSERT INTO [dbo].[Messages] ([ID], [Content], [SendTime], [SenderID], [ChatID]) VALUES (15, N'Проверка группы', N'2021-07-23 11:55:35', N'fc1a4e91-78c4-48df-8b34-16148ee634bd', 16)
SET IDENTITY_INSERT [dbo].[Messages] OFF
");
    }

    public override void Down()
    {
    }
  }
}
