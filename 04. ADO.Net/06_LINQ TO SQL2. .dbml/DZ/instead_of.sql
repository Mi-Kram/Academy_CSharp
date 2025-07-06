-- триггер разрешает удалаять только автора с au_id = '999777'
ALTER TRIGGER tr1
ON authors INSTEAD OF DELETE
AS
BEGIN
	PRINT 'Instead of delete TRIGGER!'
	delete from authors where au_id in
	(select au_id from deleted where au_id = '999777')
END


select * from authors

delete from authors
where au_id='999777'

delete from authors
where au_id='263-54-2034'

-- instead of триггер разрешает добавлять только тех авторов у которых город
-- не 'Donetsk'
create TRIGGER insert_tr
ON authors INSTEAD OF insert
AS
BEGIN
	PRINT 'Instead of insert TRIGGER!'
	insert into authors
	select * from inserted
	where city != 'Donetsk'
END

insert into authors(au_id, au_fname, au_lname, city, state, phone, contract)
values
('999777', 'Big', 'Lebowsky', 'Donetsk', 'DN', '322223', 1)

select * from authors