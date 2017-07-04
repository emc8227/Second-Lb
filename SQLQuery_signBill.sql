create database Tim001
go

use Tim001
go
create table SignBill (
  billID int not null primary key,
  company varchar(20),
  adate varchar(20),
  customer varchar(20),
  driver varchar(20),
  product varchar(20),
  standards varchar(20),
  expenses int,
  freight int,
  number int,
  overtime int,
  timehour int,
  bill_userID varchar(20)
)
go

create table Member(
userID int,
useremail varchar(20),
userpassword varchar(20)
)