﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreMVC_Identity2.Migrations
{
    public partial class SeedAuthors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'111154234', N'Linden', N'Terry', N'242545      ', N'Lenina st, 13', N'Makeevka', N'MA', N'34232', 0, NULL)
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'172-32-1176', N'White', N'Johnson', N'408 496-7223', N'10932 Bigge Rd.', N'Menlo Park', N'TN', N'98765', 1, N'enotik.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'213-46-8915', N'Green', N'Marjorie', N'415 986-7020', N'309 63rd St. #411', N'Oakland', N'MI', N'94618', 1, N'enotik.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'238-95-7766', N'Carson', N'Cheryl', N'415 548-7723', N'589 Darwin Ln.', N'Berkeley', N'DN', N'94705', 1, N'admin.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'267-41-2394', N'O''Leary', N'Michael', N'408 286-2428', N'22 Cleveland Av. #14', N'San Jose', N'DN', N'95128', 1, N'admin.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'274-80-9391', N'Straight', N'Dean', N'415 834-2919', N'5420 College Av.', N'Donetsk', N'CA', N'94609', 1, N'enotik.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'341-22-1782', N'Smith', N'Meander', N'913 843-0462', N'10 Mississippi Dr.', N'Lawrence', N'KS', N'66044', 0, N'enotik.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'409-56-7008', N'Bennet', N'Abraham', N'415 658-9932', N'6223 Bateman St.', N'Berkeley', N'CA', N'94705', 1, N'admin.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'427-17-2319', N'Dull', N'Ann', N'415 836-7111', N'3410 Blonde St.', N'Palo Alto', N'IN', N'94301', 1, N'enotik.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'472-27-2349', N'Gringlesby', N'Burt', N'707 938-6445', N'PO Box 792', N'Covelo', N'TN', N'95428', 1, N'admin.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'486-29-1786', N'Locksley', N'Charlene', N'415 585-4620', N'18 Broadway Av.', N'San Francisco', N'TN', N'94130', 1, N'admin.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'527-72-3246', N'Greene', N'Morningstar', N'615 297-2723', N'22 Graybar House Rd.', N'Nashville', N'TN', N'37215', 0, N'enotik.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'648-92-1872', N'Blotchet-Halls', N'Reginald', N'503 745-6402', N'55 Hillsdale Bl.', N'Corvallis', N'OR', N'97330', 1, N'admin.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'672-71-3249', N'Yokomoto', N'Akiko', N'415 935-4228', N'3 Silver Ct.', N'Walnut Creek', N'CA', N'94595', 1, N'admin.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'712-45-1867', N'del Castillo', N'Innes', N'615 996-8275', N'2286 Cram Pl. #86', N'Ann Arbor', N'MI', N'48105', 1, N'enotik.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'722-51-5454', N'DeFrance', N'Michel', N'219 547-9982', N'3 Balding Pl.', N'Gary', N'IN', N'46403', 1, N'admin.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'724-08-9931', N'Stringer', N'Dirk', N'415 843-2991', N'5420 Telegraph Av.', N'Oakland', N'MD', N'94609', 0, N'admin.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'724-80-9391', N'MacFeather', N'Stearns', N'415 354-7128', N'44 Upland Hts.', N'Oakland', N'CA', N'94612', 1, N'enotik.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'756-30-7391', N'Karsen', N'Livia', N'415 534-9219', N'5720 McAuley St.', N'Oakland', N'CA', N'94609', 1, N'enotik.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'807-91-6654', N'Panteley', N'Sylvia', N'301 946-8853', N'1956 Arlington Pl.', N'Rockville', N'MD', N'20853', 1, N'admin.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'846-92-7186', N'Hunter', N'Sheryl', N'415 836-7108', N'3410 Blonde St.', N'Palo Alto', N'CA', N'94301', 1, N'enotik.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'893-72-1158', N'McBadden', N'Heather', N'707 448-4982', N'301 Putnam', N'Vacaville', N'CA', N'95688', 0, N'admin.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'899-46-2035', N'Ringer', N'Anne', N'801 826-0752', N'67 Seventh Av.', N'Salt Lake City', N'UT', N'84152', 1, N'enotik.gif')
INSERT INTO [dbo].[Authors] ([au_id], [au_lname], [au_fname], [Phone], [Address], [City], [State], [Zip], [Contract], [Photo]) VALUES (N'998-72-3567', N'Ringer', N'Albert', N'801 826-0792', N'67 Seventh Av.', N'Salt Lake City', N'UT', N'84152', 1, N'enotik.gif')
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
