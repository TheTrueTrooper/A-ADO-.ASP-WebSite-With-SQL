
select * from AngeloSanches_Northwind.dbo.Orders


drop procedure GetCustomers 
go

create procedure GetCustomers 
@Filter varchar(25)
as
select 
CustomerID,
CompanyName
from AngeloSanches_Northwind.dbo.Customers
where CompanyName like '%'+ @Filter + '%'
go

execute GetCustomers 'ue'
go

drop procedure CustCatSummary 
go

create procedure CustCatSummary 
@CustomerID nchar(5)
as
select 
CategoryName,
Sum(Quantity) as 'Total',
'$' + CONVERT(varchar(9), Cast(Sum(Quantity * d.UnitPrice) as smallmoney)) as 'Cost'
from AngeloSanches_Northwind.dbo.Customers as c
inner join AngeloSanches_Northwind.dbo.Orders as o
on c.CustomerID = o.CustomerID 
inner join AngeloSanches_Northwind.dbo.[Order Details] as d
on o.OrderID = d.OrderID 
inner join AngeloSanches_Northwind.dbo.Products as p
on p.ProductID = d.ProductID 
inner join AngeloSanches_Northwind.dbo.Categories  as ca
on p.CategoryID = ca.CategoryID 
where c.CustomerID = @CustomerID
group by CategoryName
order by Sum(Quantity) DESC
go

execute  CustCatSummary 'CHOPS'
go 

drop procedure DeleteOrderDetails 
go

create procedure DeleteOrderDetails
@OrderID int, 
@ProductID int,
@status varchar(80) output
as
delete 
from AngeloSanches_Northwind.dbo.[Order Details]
where OrderID = @OrderID and ProductID = @ProductID
if @@ROWCOUNT < 0
begin
	set @status = 'No Records deleted, possible error'
	return
end
else
begin
	set @status = 'Record Deleted'
	return
end
go

execute  CustCatSummary 'CHOPS'
go 

drop procedure InsertOrderDetails
go

create procedure InsertOrderDetails
@OrderID int, 
@ProductID int,
@Quantity smallint,
@status varchar(80) output
as
If not exists( select ProductID from AngeloSanches_Northwind.dbo.Products where ProductID = @ProductID )
 return 0
If not exists( select OrderID from AngeloSanches_Northwind.dbo.Orders where OrderID = @OrderID )
 return 0
If exists( select OrderID from AngeloSanches_Northwind.dbo.[Order Details] where OrderID = @OrderID and ProductID = @ProductID)
 return 0
INSERT INTO AngeloSanches_Northwind.dbo.[Order Details] 
(ProductID,OrderID,Quantity,Discount,UnitPrice)
VALUES 
(@ProductID,@OrderID,@Quantity,0, 0); 
update AngeloSanches_Northwind.dbo.[Order Details]
set UnitPrice = (select UnitPrice from AngeloSanches_Northwind.dbo.Products where ProductID = @ProductID)
where ProductID = @ProductID and OrderID = @OrderID
set @status = @@ROWCOUNT
go

select * from AngeloSanches_Northwind.dbo.Orders
select * from AngeloSanches_Northwind.dbo.Products

declare @OrderID as int = 10254
declare @ProductID as int = 1
declare @Quantity as smallint = 45


select * from AngeloSanches_Northwind.dbo.[Order Details]
where OrderID = @OrderID and ProductID = @ProductID

execute  InsertOrderDetails @OrderID, @ProductID, @Quantity

select * from AngeloSanches_Northwind.dbo.[Order Details]
where OrderID = @OrderID and ProductID = @ProductID
