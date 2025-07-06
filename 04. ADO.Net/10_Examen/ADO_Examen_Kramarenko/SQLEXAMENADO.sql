-- TASK1

create database ADO_EXAM01
use ADO_EXAM01

create table Manufactures(ManufacturerId varchar(32) primary key not null, BrandTitle varchar(50) not null, Address varchar(50), Phone varchar(20))
create table Planes(Id varchar(32) primary key not null, Model varchar(50), Price float, Speed float, ManufacturerId varchar(32))

insert into Manufactures(ManufacturerId, BrandTitle, Address, Phone)
values
('6b85fceb43d74a84b60fdc7e986f4a90', 'Manufacture01', '10932 Bigge Rd.',		'408-496-7223'),
('9cd18d2271ba4649a00b0eafdf771c16', 'Manufacture02', '309 63rd St. #411',		'415-986-7020'),
('c276ef5bc1e84557811d200ac2234a18', 'Manufacture03', '589 Darwin Ln.',			'415-548-7723'),
('ce76be39d97e4d118dc121fd9fda46ec', 'Manufacture04', '22 Cleveland Av. #14',	'408-286-2428'),
('a72ad8e3077849fcb0fc24b503282648', 'Manufacture05', '5420 College Av.',		'415-834-2919'),
('69002541c0a342bb8495f1becf4fed3d', 'Manufacture06', '10 Mississippi Dr.',		'913-843-0462'),
('410405781d03484cadda3e762815a08c', 'Manufacture07', '6223 Bateman St.',		'415-658-9932')

insert into Planes(Id, Model, Price, Speed, ManufacturerId)
values
('c267b579466d4c16aa6e457befae054d','PlaneModel01', 6183, 660,'6b85fceb43d74a84b60fdc7e986f4a90'),
('61beb40206844067a7689f726175c1a5','PlaneModel02', 6987, 890,'ce76be39d97e4d118dc121fd9fda46ec'),
('d7d4a781edcc4912832aef3295342e66','PlaneModel03', 7195, 620,'9cd18d2271ba4649a00b0eafdf771c16'),
('705576679dcb4f218b55be0944d9c8b3','PlaneModel04', 4404, 820,'a72ad8e3077849fcb0fc24b503282648'),
('32c77b2dfbc24fb9b24712a2bcdee968','PlaneModel05', 5709, 780,'ce76be39d97e4d118dc121fd9fda46ec'),
('5592eddb9c9845018ced735cd19021f7','PlaneModel06', 5701, 760,'6b85fceb43d74a84b60fdc7e986f4a90'),
('c00d86ed375f42c1a976b191bb34a782','PlaneModel07', 7865, 570,'a72ad8e3077849fcb0fc24b503282648'),
('b5681891b10449278db0b323f4983c33','PlaneModel08', 4810, 690,'c276ef5bc1e84557811d200ac2234a18'),
('d72b121070284f9e9a7bf9888bf93a14','PlaneModel09', 4300, 510,'c276ef5bc1e84557811d200ac2234a18'),
('7f3aa57f15b2451bab6bfd9bad01b2a1','PlaneModel10', 6135, 660,'c276ef5bc1e84557811d200ac2234a18')

create proc GetManufactures
as
begin
	select * from Manufactures
end

create proc GetPlanes
as
begin
	select * from Planes
end

create proc AddManufacture(@ManufacturerId varchar(32), @BrandTitle varchar(50), @Address varchar(50), @Phone varchar(20))
as
begin
	insert into Manufactures(ManufacturerId, BrandTitle, Address, Phone)
	values (@ManufacturerId, @BrandTitle, @Address, @Phone)
end

create proc AddPlane(@Id varchar(32), @Model varchar(50), @Price float, @Speed float, @ManufacturerId varchar(32))
as
begin
	insert into Planes(Id, Model, Price, Speed, ManufacturerId)
	values (@Id, @Model, @Price, @Speed, @ManufacturerId)
end

create proc UpdateManufacture(@ManufacturerId varchar(32), @BrandTitle varchar(50), @Address varchar(50), @Phone varchar(20))
as
begin
	update Manufactures
	set
	BrandTitle = @BrandTitle,
	Address = @Address,
	Phone = @Phone
	where ManufacturerId = @ManufacturerId
end

create proc UpdatePlane(@Id varchar(32), @Model varchar(50), @Price float, @Speed float, @ManufacturerId varchar(32))
as
begin
	update Planes
	set
	Model = @Model,
	Price = @Price,
	Speed = @Speed,
	ManufacturerId = @ManufacturerId
	where Id = @Id
end

create proc RemoveManufacture(@ManufacturerId varchar(32))
as
begin
	delete from Manufactures
	where ManufacturerId = @ManufacturerId
end

create proc RemovePlane(@Id varchar(32))
as
begin
	delete from Planes
	where Id = @Id
end





select * from Manufactures
select * from Planes

-- drops

drop proc GetManufactures
drop proc GetPlanes

drop proc AddManufacture
drop proc AddPlane

drop proc UpdateManufacture
drop proc UpdatePlane

drop proc RemoveManufacture
drop proc RemovePlane


drop table Planes
drop table Manufactures

drop database ADO_EXAM01



-- TASK2

create database ADO_EXAM02
use ADO_EXAM02

create table Departments(DepartmentId varchar(32) primary key not null, Title varchar(50) not null, HeadId varchar(32), Address varchar(50), PhoneNumber varchar(20))
create table Employees(Employee_id varchar(32) primary key not null, FirstName varchar(20), LastName varchar(30), Age int, Address varchar(32), PhotoPath varchar(200) default 'Resources\peopleImages\unknown.jpg')
create table DepartmentsEmployees(DepartmentId varchar(32) not null, EmployeeId varchar(32) not null, primary key(DepartmentId, EmployeeId))

insert into Departments(DepartmentId, Title, HeadId, Address, PhoneNumber)
values
('38a76fe0dc9343f88c3e468cb7892229','Department01','67aa03b3049f4169b631b889e0cc0785','10932 Bigge Rd.',		'408-496-7223'),
('70b5694021574184a7239c11a53aea6b','Department02','43ba6dab726844d48968e2b64b65e083','309 63rd St. #411',		'415-986-7020'),
('2729826abbdc4cbdb38d2b73d42d2eb1','Department03','','589 Darwin Ln.',			'415-548-7723'),
('695be0f3f1a44ea2a9162641df60f321','Department04','','22 Cleveland Av. #14',	'408-286-2428')

insert into Employees(Employee_id, FirstName, LastName, Age, Address, PhotoPath)
values
('67aa03b3049f4169b631b889e0cc0785','Abraham'		,'Bennet'			,'24','10 Mississippi Dr.'		,'person02.jpg'),
('673585a9a88b4a0a8a98b6d047fad45a','Reginald'		,'Blotchet-Halls'	,'22','1028 Long St.'			,'person06.jpg'),
('f196ca553f1641c7a9181457e0439999','Cheryl'		,'Carson'			,'38','18 Broadway Av.'			,'person01.jpg'),
('43ba6dab726844d48968e2b64b65e083','Michel'		,'DeFrance'			,'36','1956 Arlington Pl.'		,'person03.jpg'),
('da76638fc9db4b48a809372fe76167a3','Innes'			,'del Castillo'		,'31','22 Graybar House Rd.'	,'person04.jpg'),
('6dd8a3a6122b4abd8a3f70374a43a463','Ann'			,'Dull'				,'32','2286 Cram Pl. #86'		,'person05.jpg'),
('c80a7b0163c446c9b6c231bf3a7f5b0b','Marjorie'		,'Green'			,'24','3 Balding Pl.'			,'unknown.jpg'),
('f88496caac48417b84ce0af56162a866','Morningstar'	,'Greene'			,'30','3 Silver Ct.'			,'unknown.jpg'),
('89c27e500e8b421088287a55c3170ffc','Burt'			,'Gringlesby'		,'39','301 Putnam'				,'unknown.jpg'),
('9c3f2f2ceaaa499eb28ba475869ca270','Sheryl'		,'Hunter'			,'37','3410 Blonde St.'			,'unknown.jpg')

insert into DepartmentsEmployees(DepartmentId, EmployeeId)
values 
('38a76fe0dc9343f88c3e468cb7892229','67aa03b3049f4169b631b889e0cc0785'),
('38a76fe0dc9343f88c3e468cb7892229','f196ca553f1641c7a9181457e0439999'),
('38a76fe0dc9343f88c3e468cb7892229','89c27e500e8b421088287a55c3170ffc'),
('38a76fe0dc9343f88c3e468cb7892229','f88496caac48417b84ce0af56162a866'),
('70b5694021574184a7239c11a53aea6b','43ba6dab726844d48968e2b64b65e083'),
('70b5694021574184a7239c11a53aea6b','9c3f2f2ceaaa499eb28ba475869ca270'),
('70b5694021574184a7239c11a53aea6b','c80a7b0163c446c9b6c231bf3a7f5b0b')

delete from Employees

select * from Departments
select * from Employees
select * from DepartmentsEmployees

create proc GetEmployeesWithDepartmentTitle
as 
begin
	select FirstName, LastName, Title
	from Departments right outer join DepartmentsEmployees
	on Departments.DepartmentId = DepartmentsEmployees.DepartmentId
	full outer join Employees
	on Employees.Employee_id = DepartmentsEmployees.EmployeeId
end





-- drops



drop proc GetEmployeesWithDepartmentTitle

drop table Departments
drop table Employees
drop table DepartmentsEmployees

drop database ADO_EXAM02