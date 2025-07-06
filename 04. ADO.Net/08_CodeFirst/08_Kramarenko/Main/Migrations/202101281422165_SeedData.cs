namespace Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedData : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[Owners] ([own_id], [pathImg], [name], [address], [phone]) VALUES (N'own01', N'Resources/Owners Images/person01.jpg', N'Johnson', N'10932 Bigge Rd.', N'408 496-7223')
                INSERT INTO [dbo].[Owners] ([own_id], [pathImg], [name], [address], [phone]) VALUES (N'own02', N'Resources/Owners Images/person06.jpg', N'Cheryl', N'309 63rd St. #411', N'415 986-7020')
                INSERT INTO [dbo].[Owners] ([own_id], [pathImg], [name], [address], [phone]) VALUES (N'own03', N'Resources/Owners Images/person03.jpg', N'Michael', N'589 Darwin Ln.', N'415 548-7723')
                INSERT INTO [dbo].[Owners] ([own_id], [pathImg], [name], [address], [phone]) VALUES (N'own04', N'Resources/Owners Images/person04.jpg', N'Ann', N'22 Cleveland Av. #14', N'408 286-2428')
                INSERT INTO [dbo].[Owners] ([own_id], [pathImg], [name], [address], [phone]) VALUES (N'own05', N'Resources/Owners Images/person02.jpg', N'Petr', N'5420 College Av.', N'415 834-2919')
            ");

            Sql(@"
                INSERT INTO [dbo].[Cars] ([car_id], [pathImg], [brand], [body_type], [registrDate], [own_own_id]) VALUES (N'car01', N'Resources\Curs Images\cur01.jpg', N'Audi', N'Sedan', N'2005-03-24', N'own02')
                INSERT INTO [dbo].[Cars] ([car_id], [pathImg], [brand], [body_type], [registrDate], [own_own_id]) VALUES (N'car02', N'Resources\Curs Images\cur03.jpg', N'BMW', N'Minivan', N'1998-09-21', N'own05')
                INSERT INTO [dbo].[Cars] ([car_id], [pathImg], [brand], [body_type], [registrDate], [own_own_id]) VALUES (N'car03', N'Resources\Curs Images\cur05.jpg', N'Bentley', N'Coupe', N'2016-06-08', N'own01')
                INSERT INTO [dbo].[Cars] ([car_id], [pathImg], [brand], [body_type], [registrDate], [own_own_id]) VALUES (N'car04', N'Resources\Curs Images\cur02.jpg', N'Cadillac', N'Hatchback', N'2020-12-26', N'own03')
                INSERT INTO [dbo].[Cars] ([car_id], [pathImg], [brand], [body_type], [registrDate], [own_own_id]) VALUES (N'car05', N'Resources\Curs Images\cur07.jpg', N'Alfa', N'Pickup', N'2013-09-17', N'own01')
            ");
        }

        public override void Down()
        {
        }
    }
}
