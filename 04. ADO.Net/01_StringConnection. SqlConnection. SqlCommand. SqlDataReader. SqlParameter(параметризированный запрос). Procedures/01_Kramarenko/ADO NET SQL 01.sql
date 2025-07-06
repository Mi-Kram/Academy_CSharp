use pubs

create table owners (own_id varchar(20) primary key, name varchar(50), address varchar(50), phone varchar(20))
create table cars (car_id varchar(20) primary key, brand varchar(50), model varchar(50), registrDate date, own_id varchar(20))

insert into owners (own_id, name, address, phone)
values 
('own01', 'Johnson', '10932 Bigge Rd.', '408 496-7223'),
('own02', 'Cheryl', '309 63rd St. #411', '415 986-7020'),
('own03', 'Michael', '589 Darwin Ln.', '415 548-7723'),
('own04', 'Ann', '22 Cleveland Av. #14', '408 286-2428'),
('own05', 'Petr', '5420 College Av.', '415 834-2919')

insert into cars (car_id, brand, model, registrDate, own_id)
values 
('cur01', 'Audi', 'Hatchback', '2005-03-24', 'own02'),
('cur02', 'BMW', 'Sedan', '1998-09-21', 'own05'),
('cur03', 'Bentley', 'MPV', '2016-06-08', 'own01'),
('cur04', 'Cadillac', 'SUV', '2020-12-26', 'own03'),
('cur05', 'Alfa ', 'Crossover', '2013-09-17', 'own01')

select * from owners
select * from cars

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
exec GetColumns 'pubs', 'authors'

create proc GetTables(@table_catalog varchar(max))
as
begin
	select distinct TABLE_NAME
	from information_schema.columns
	where table_catalog = @table_catalog
end
exec GetTables 'pubs'

create proc GetCars(@own_id varchar(max))
as
begin
	select car_id, brand, model from cars
	where own_id = @own_id
end


drop table owners
drop table cars

drop proc GetColumns
drop proc GetTables
drop proc GetCars

