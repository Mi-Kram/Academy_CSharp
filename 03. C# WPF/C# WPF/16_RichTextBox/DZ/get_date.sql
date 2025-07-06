-- получить текущие дату и время
select getdate()

-- получить год, месяц, день из даты
select year(getdate()), MONTH(getdate()), day(getdate())

select year('12-23-2012'), month('12-23-2012'), day('12-23-2012')

-- получить часы, минуты, секунды и миллисекунды из DateTime
select datepart(hh, getdate()), datepart(mi, getdate()), datepart(ss, getdate()), 
datepart(ms, getdate())

-- получить разницу между датами
select datediff(mi, '10-12-1990', getdate())

select title, year(pubdate) from titles

-- показать книги, опубликованные после 1991
select * from titles
where year(pubdate) > 1991

-- показать книги, которые были опубликованы летом в жанре 'Business'
select * from titles
where month(pubdate) in (6, 7, 8) and type = 'Business'

-- добавление интервала времени к дате
select DATEADD(hh, 19900, '3-19-1978')

-- добавить к дате публикации книг в жанре 'Business' 2 месяца
update titles
set pubdate = dateadd(mm, 2, pubdate)
where type = 'Business'

-- len -  длина строки
-- left (right) - часть строки слева (справа)
-- substring - получить подстроку
-- replace - замена подстроки на другую
-- upper, lower - перевод строки в верхний и нижний регистр
select title, len(title), left(title, 3), right(title, 2),
SUBSTRING(title, 2, 3), replace(title, 'a', '!'), UPPER(title), lower(title) from titles

-- LIKE
-- % - всё, что угодно любой длины
-- _ любой символ
-- [abd] - перечисление символов

-- название содержит слово 'and'
select title from titles
where title like '%and%'

-- названия, содержащие слова из 3 букв
select title from titles
where title like '% ___ %'

-- названия, начинающиеся на определённую букву
select title from titles
where title like '[abcdf]%'

-- названия НЕ начинающиеся на буквы в указанном диапазоне
select title from titles
where title like '[^a-n]%'

-- показать авторов, чьи имена начинаются на гласную букву
select * from authors
where au_fname like '[aoiuye]%'