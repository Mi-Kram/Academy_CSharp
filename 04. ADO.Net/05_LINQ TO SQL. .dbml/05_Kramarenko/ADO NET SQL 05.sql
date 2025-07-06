use pubs

create table owners (own_id varchar(20) primary key, pathImg varchar(100), name varchar(50), address varchar(50), phone varchar(20))
create table cars (car_id varchar(20) primary key, pathImg varchar(100), brand varchar(50), body_type varchar(50), registrDate varchar(20), own_id varchar(20))

insert into cars (car_id, pathImg, brand, body_type, registrDate, own_id)
values 
('cur01', 'Resources\Curs Images\cur01.jpg', 'Audi',	 'Sedan',		'2005-03-24', 'own02'),
('cur02', 'Resources\Curs Images\cur03.jpg', 'BMW',		 'Minivan',		'1998-09-21', 'own05'),
('cur03', 'Resources\Curs Images\cur05.jpg', 'Bentley',	 'Coupe',		'2016-06-08', 'own01'),
('cur04', 'Resources\Curs Images\cur02.jpg', 'Cadillac', 'Hatchback',	'2020-12-26', 'own03'),
('cur05', 'Resources\Curs Images\cur07.jpg', 'Alfa ',	 'Pickup',		'2013-09-17', 'own01')

insert into owners (own_id, pathImg, name, address, phone)
values 
('own01', 'Resources/Owners Images/person01.jpg', 'Johnson',	'10932 Bigge Rd.',		'408 496-7223'),
('own02', 'Resources/Owners Images/person06.jpg', 'Cheryl',		'309 63rd St. #411',	'415 986-7020'),
('own03', 'Resources/Owners Images/person03.jpg', 'Michael',	'589 Darwin Ln.',		'415 548-7723'),
('own04', 'Resources/Owners Images/person04.jpg', 'Ann',		'22 Cleveland Av. #14', '408 286-2428'),
('own05', 'Resources/Owners Images/person02.jpg', 'Petr',		'5420 College Av.',		'415 834-2919')

select * from owners
select * from cars

delete from owners
delete from cars

select *
from information_schema.columns
where table_catalog = 'pubs'
and table_name = 'authors'

create proc GetColumns(@table_catalog varchar(max), @table_name varchar(max))
as
begin
	select COLUMN_NAME
	from information_schema.columns
	where table_catalog = @table_catalog
	and table_name = @table_name
end

create proc GetTables(@table_catalog varchar(max))
as
begin
	select distinct TABLE_NAME
	from information_schema.columns
	where table_catalog = @table_catalog
end

create proc GetCars(@own_id varchar(max))
as
begin
	select car_id, pathImg, brand, model from cars
	where own_id = @own_id
end


drop table owners
drop table cars

drop proc GetColumns
drop proc GetTables
drop proc GetCars

