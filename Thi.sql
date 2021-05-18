create database QLKH;
use QLKH;

create table khachhang(
		MAKH VARCHAR(4) PRIMARY KEY,
		TENKH VARCHAR(40),
		DIACHI VARCHAR(40)
);

CREATE TABLE Sudung(
	MASD VARCHAR(5) PRIMARY KEY,
	LOAISD VARCHAR(20),
	DONGIA FLOAT
);

CREATE TABLE Chitiet(
	MASD VARCHAR(5),
	MAKH VARCHAR(4),
	SOKW INT,
	THANHTIEN FLOAT
	--FOREIGN KEY (MASD) REFERENCES Sudung(MASD),
	--FOREIGN KEY (MAKH) REFERENCES khachhang(MAKH)
);
--DROP TABLE Chitiet;
--drop table khachhang;
--drop table Sudung;


select * from khachhang;
INSERT INTO khachhang VALUES('KH01','Nguyen Van A','TP Can Tho');
INSERT INTO khachhang VALUES('KH02','Nguyen Van B','TP Can Tho');
INSERT INTO khachhang VALUES('KH03','Nguyen Van CC','TP Can Tho');

INSERT INTO khachhang VALUES('KH04','Nguyen Van DD','TP Can Tho');

--delete from Sudung;
INSERT INTO SUDUNG VALUES('KD','KINH DOANH','750');
INSERT INTO SUDUNG VALUES('SX','SAN XUAT','100');
INSERT INTO SUDUNG VALUES('SH','SINH HOAT','500');

INSERT INTO Chitiet VALUES('SX','KH01','10','');
INSERT INTO Chitiet VALUES('SH','KH01','10','');
INSERT INTO Chitiet VALUES('SX','KH02','20','');
INSERT INTO Chitiet VALUES('KD','KH02','20','');
INSERT INTO Chitiet VALUES('SX','KH03','30','');

INSERT INTO Chitiet VALUES('SX','KH04','10','');
INSERT INTO Chitiet VALUES('SH','KH04','10','');
INSERT INTO Chitiet VALUES('KD','KH04','20','');


SELECT DISTINCT khachhang.TENKH, Chitiet.MASD, Sudung.LOAISD, Chitiet.SOKW, Sudung.DONGIA, dbo.thanhtien(Chitiet.MAKH, Chitiet.MASD) as THANHTIEN FROM khachhang JOIN Chitiet ON Khachhang.MAKH = Chitiet.MAKH JOIN Sudung ON Sudung.MASD = Chitiet.MASD

SELECT DISTINCT khachhang.TENKH, Chitiet.MASD, Sudung.LOAISD, Chitiet.SOKW, Sudung.DONGIA, dbo.thanhtien(Chitiet.MAKH, Chitiet.MASD) as THANHTIEN FROM khachhang JOIN Chitiet ON khachhang.MAKH = Chitiet.MAKH JOIN Sudung ON Sudung.MASD = Chitiet.MASD WHERE Chitiet.MAKH  = 'KH02'

select * from khachhang where MAKH = 'KH01';

go
create function thanhtien (@makh varchar(10), @masd varchar(10))
returns int
as
begin 
	declare @ketqua int
	declare @sokw int
	declare @dongia int
	select @sokw = cast(SOKW as int) from Chitiet ct
	where ct.MAKH = @makh
	select @dongia = cast(DONGIA as int) from Sudung sd
	inner join Chitiet ct on ct.MASD = sd.MASD
	where ct.MAKH = @makh and ct.MASD = @masd
	
	set @ketqua = @sokw * @dongia
	
	RETURN @ketqua;
end
go



SELECT dbo.thanhtien('KH01', 'SH');
/*
go
create function CustomerTotal (@makh varchar(10))
returns int 
as
begin 
	declare @ketqua int
	declare @sokw int
	declare @dongia1 int
	declare @dongia2 int
	
	select @sokw = cast(SOKW as int) from Chitiet ct
	where ct.MAKH = @makh
	
	select @dongia1 = cast(DONGIA as int) from Sudung sd
	inner join Chitiet ct on ct.MASD = sd.MASD
	where ct.MAKH = @makh
	
	select @dongia2 = cast(DONGIA as int) from Sudung sd
	inner join Chitiet ct on ct.MASD = sd.MASD
	where ct.MAKH = @makh and DONGIA <> @dongia1 

	set @ketqua = (@sokw * @dongia1) + (@sokw * @dongia2)
	
	RETURN @ketqua;
end
go

drop function dbo.CustomerTotal

SELECT dbo.CustomerTotal('KH04') as TongTien ;
*/
select * from khachhang;

select distinct Sum( dbo.thanhtien(Chitiet.MAKH, Chitiet.MASD)) as TongTien FROM khachhang JOIN Chitiet ON Khachhang.MAKH = Chitiet.MAKH JOIN Sudung ON Sudung.MASD = Chitiet.MASD WHERE Chitiet.MAKH  = 'KH04'
