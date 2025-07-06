using Microsoft.EntityFrameworkCore.Migrations;

namespace Main.Migrations
{
    public partial class SeedPublishersTitles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[publishers] ([pub_id], [pub_name], [city], [state], [country]) VALUES (N'0736', N'New Moon Books', N'Boston', N'MA', N'USA')
INSERT INTO [dbo].[publishers] ([pub_id], [pub_name], [city], [state], [country]) VALUES (N'0877', N'Binnet & Hardley', N'Washington', N'DC', N'USA')
INSERT INTO [dbo].[publishers] ([pub_id], [pub_name], [city], [state], [country]) VALUES (N'1389', N'Algodata Infosystems', N'Berkeley', N'CA', N'USA')
INSERT INTO [dbo].[publishers] ([pub_id], [pub_name], [city], [state], [country]) VALUES (N'1622', N'Five Lakes Publishing', N'Chicago', N'IL', N'USA')
INSERT INTO [dbo].[publishers] ([pub_id], [pub_name], [city], [state], [country]) VALUES (N'1756', N'Ramona Publishers', N'Dallas', N'TX', N'USA')
INSERT INTO [dbo].[publishers] ([pub_id], [pub_name], [city], [state], [country]) VALUES (N'9901', N'GGG&G', N'Mnchen', NULL, N'Germany')
INSERT INTO [dbo].[publishers] ([pub_id], [pub_name], [city], [state], [country]) VALUES (N'9952', N'Scootney Books', N'New York', N'NY', N'USA')
INSERT INTO [dbo].[publishers] ([pub_id], [pub_name], [city], [state], [country]) VALUES (N'9999', N'Lucerne Publishing', N'Paris', NULL, N'France')");

            migrationBuilder.Sql(@"
set language english
INSERT INTO [dbo].[titles] ([title_id], [title], [type], [pub_id], [price], [advance], [royalty], [ytd_sales], [notes], [pubdate]) VALUES (N'BU1032', N'The Busy Executive''s Database Guide', N'business    ', N'1389', CAST(19.9900 AS Money), CAST(5000.0000 AS Money), 10, 4095, N'An overview of available database systems with emphasis on common business applications. Illustrated.', N'1991-06-12 00:00:00')
INSERT INTO [dbo].[titles] ([title_id], [title], [type], [pub_id], [price], [advance], [royalty], [ytd_sales], [notes], [pubdate]) VALUES (N'BU1111', N'Cooking with Computers: Surreptitious Balance Sheets', N'business    ', N'1389', CAST(11.9500 AS Money), CAST(5000.0000 AS Money), 10, 3876, N'Helpful hints on how to use your electronic resources to the best advantage.', N'1991-06-09 00:00:00')
INSERT INTO [dbo].[titles] ([title_id], [title], [type], [pub_id], [price], [advance], [royalty], [ytd_sales], [notes], [pubdate]) VALUES (N'BU2075', N'You Can Combat Computer Stress!', N'business    ', N'0736', CAST(2.9900 AS Money), CAST(10125.0000 AS Money), 24, 18722, N'The latest medical and psychological techniques for living with the electronic office. Easy-to-understand explanations.', N'1991-06-30 00:00:00')
INSERT INTO [dbo].[titles] ([title_id], [title], [type], [pub_id], [price], [advance], [royalty], [ytd_sales], [notes], [pubdate]) VALUES (N'BU7832', N'Straight Talk About Computers', N'business    ', N'1389', CAST(19.9900 AS Money), CAST(5000.0000 AS Money), 10, 4095, N'Annotated analysis of what computers can do for you: a no-hype guide for the critical user.', N'1991-06-22 00:00:00')
INSERT INTO [dbo].[titles] ([title_id], [title], [type], [pub_id], [price], [advance], [royalty], [ytd_sales], [notes], [pubdate]) VALUES (N'MC2222', N'Silicon Valley Gastronomic Treats', N'mod_cook    ', N'0877', CAST(19.9900 AS Money), CAST(0.0000 AS Money), 12, 2032, N'Favorite recipes for quick, easy, and elegant meals.', N'1991-06-09 00:00:00')
INSERT INTO [dbo].[titles] ([title_id], [title], [type], [pub_id], [price], [advance], [royalty], [ytd_sales], [notes], [pubdate]) VALUES (N'MC3021', N'The Gourmet Microwave', N'mod_cook    ', N'0877', CAST(2.9900 AS Money), CAST(15000.0000 AS Money), 24, 22246, N'Traditional French gourmet recipes adapted for modern microwave cooking.', N'1991-06-18 00:00:00')
INSERT INTO [dbo].[titles] ([title_id], [title], [type], [pub_id], [price], [advance], [royalty], [ytd_sales], [notes], [pubdate]) VALUES (N'MC3026', N'The Psychology of Computer Cooking', N'UNDECIDED   ', N'0877', NULL, NULL, NULL, NULL, NULL, N'2004-12-13 16:11:36')
INSERT INTO [dbo].[titles] ([title_id], [title], [type], [pub_id], [price], [advance], [royalty], [ytd_sales], [notes], [pubdate]) VALUES (N'PC1035', N'But Is It User Friendly?', N'popular_comp', N'1389', CAST(22.9500 AS Money), CAST(7000.0000 AS Money), 16, 8780, N'A survey of software for the naive user, focusing on the ''friendliness'' of each.', N'1991-06-30 00:00:00')
INSERT INTO [dbo].[titles] ([title_id], [title], [type], [pub_id], [price], [advance], [royalty], [ytd_sales], [notes], [pubdate]) VALUES (N'PC8888', N'Secrets of Silicon Valley', N'popular_comp', N'1389', CAST(20.0000 AS Money), CAST(8000.0000 AS Money), 10, 4095, N'Muckraking reporting on the world''s largest computer hardware and software manufacturers.', N'1994-06-12 00:00:00')
INSERT INTO [dbo].[titles] ([title_id], [title], [type], [pub_id], [price], [advance], [royalty], [ytd_sales], [notes], [pubdate]) VALUES (N'PC9999', N'Net Etiquette', N'popular_comp', N'1389', NULL, NULL, NULL, NULL, N'A must-read for computer conferencing.', N'2004-12-13 16:11:36')
INSERT INTO [dbo].[titles] ([title_id], [title], [type], [pub_id], [price], [advance], [royalty], [ytd_sales], [notes], [pubdate]) VALUES (N'PS1372', N'Computer Phobic AND Non-Phobic Individuals: Behavior Variations', N'psychology  ', N'0877', CAST(21.5900 AS Money), CAST(7000.0000 AS Money), 10, 375, N'A must for the specialist, this book examines the difference between those who hate and fear computers and those who don''t.', N'1991-10-21 00:00:00')
INSERT INTO [dbo].[titles] ([title_id], [title], [type], [pub_id], [price], [advance], [royalty], [ytd_sales], [notes], [pubdate]) VALUES (N'PS2091', N'Is Anger the Enemy?', N'psychology  ', N'0736', CAST(10.9500 AS Money), CAST(2275.0000 AS Money), 12, 2045, N'Carefully researched study of the effects of strong emotions on the body. Metabolic charts included.', N'1991-06-15 00:00:00')
INSERT INTO [dbo].[titles] ([title_id], [title], [type], [pub_id], [price], [advance], [royalty], [ytd_sales], [notes], [pubdate]) VALUES (N'PS2106', N'Life Without Fear', N'psychology  ', N'0736', CAST(7.0000 AS Money), CAST(6000.0000 AS Money), 10, 111, N'New exercise, meditation, and nutritional techniques that can reduce the shock of daily interactions. Popular audience. Sample menus included, exercise video available separately.', N'1991-10-05 00:00:00')
INSERT INTO [dbo].[titles] ([title_id], [title], [type], [pub_id], [price], [advance], [royalty], [ytd_sales], [notes], [pubdate]) VALUES (N'PS3333', N'Prolonged Data Deprivation: Four Case Studies', N'psychology  ', N'0736', CAST(19.9900 AS Money), CAST(2000.0000 AS Money), 10, 4072, N'What happens when the data runs dry?  Searching evaluations of information-shortage effects.', N'1991-06-12 00:00:00')
INSERT INTO [dbo].[titles] ([title_id], [title], [type], [pub_id], [price], [advance], [royalty], [ytd_sales], [notes], [pubdate]) VALUES (N'PS7777', N'Emotional Security: A New Algorithm', N'psychology  ', N'0736', CAST(7.9900 AS Money), CAST(4000.0000 AS Money), 10, 3336, N'Protecting yourself and your loved ones from undue emotional stress in the modern world. Use of computer and nutritional aids emphasized.', N'1991-06-12 00:00:00')
INSERT INTO [dbo].[titles] ([title_id], [title], [type], [pub_id], [price], [advance], [royalty], [ytd_sales], [notes], [pubdate]) VALUES (N'TC3218', N'Onions, Leeks, and Garlic: Cooking Secrets of the Mediterranean', N'trad_cook   ', N'0877', CAST(20.9500 AS Money), CAST(7000.0000 AS Money), 10, 375, N'Profusely illustrated in color, this makes a wonderful gift book for a cuisine-oriented friend.', N'1991-10-21 00:00:00')
INSERT INTO [dbo].[titles] ([title_id], [title], [type], [pub_id], [price], [advance], [royalty], [ytd_sales], [notes], [pubdate]) VALUES (N'TC4203', N'Fifty Years in Buckingham Palace Kitchens', N'trad_cook   ', N'0877', CAST(11.9500 AS Money), CAST(4000.0000 AS Money), 14, 15096, N'More anecdotes from the Queen''s favorite cook describing life among English royalty. Recipes, techniques, tender vignettes.', N'1991-06-12 00:00:00')
INSERT INTO [dbo].[titles] ([title_id], [title], [type], [pub_id], [price], [advance], [royalty], [ytd_sales], [notes], [pubdate]) VALUES (N'TC7777', N'Sushi, Anyone?', N'trad_cook   ', N'0877', CAST(14.9900 AS Money), CAST(8000.0000 AS Money), 10, 4095, N'Detailed instructions on how to make authentic Japanese sushi in your spare time.', N'1991-06-12 00:00:00')");
            

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
