-- �������� ������� ���� � �����
select getdate()

-- �������� ���, �����, ���� �� ����
select year(getdate()), MONTH(getdate()), day(getdate())

select year('12-23-2012'), month('12-23-2012'), day('12-23-2012')

-- �������� ����, ������, ������� � ������������ �� DateTime
select datepart(hh, getdate()), datepart(mi, getdate()), datepart(ss, getdate()), 
datepart(ms, getdate())

-- �������� ������� ����� ������
select datediff(mi, '10-12-1990', getdate())

select title, year(pubdate) from titles

-- �������� �����, �������������� ����� 1991
select * from titles
where year(pubdate) > 1991

-- �������� �����, ������� ���� ������������ ����� � ����� 'Business'
select * from titles
where month(pubdate) in (6, 7, 8) and type = 'Business'

-- ���������� ��������� ������� � ����
select DATEADD(hh, 19900, '3-19-1978')

-- �������� � ���� ���������� ���� � ����� 'Business' 2 ������
update titles
set pubdate = dateadd(mm, 2, pubdate)
where type = 'Business'

-- len -  ����� ������
-- left (right) - ����� ������ ����� (������)
-- substring - �������� ���������
-- replace - ������ ��������� �� ������
-- upper, lower - ������� ������ � ������� � ������ �������
select title, len(title), left(title, 3), right(title, 2),
SUBSTRING(title, 2, 3), replace(title, 'a', '!'), UPPER(title), lower(title) from titles

-- LIKE
-- % - ��, ��� ������ ����� �����
-- _ ����� ������
-- [abd] - ������������ ��������

-- �������� �������� ����� 'and'
select title from titles
where title like '%and%'

-- ��������, ���������� ����� �� 3 ����
select title from titles
where title like '% ___ %'

-- ��������, ������������ �� ����������� �����
select title from titles
where title like '[abcdf]%'

-- �������� �� ������������ �� ����� � ��������� ���������
select title from titles
where title like '[^a-n]%'

-- �������� �������, ��� ����� ���������� �� ������� �����
select * from authors
where au_fname like '[aoiuye]%'