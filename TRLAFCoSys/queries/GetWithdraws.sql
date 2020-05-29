/**
 x = productions 
 y = products
 z = productMaterials
*/

select * from Productions as x
	INNER JOIN Products as y ON (x.ProductID = y.ProductID)
	INNER JOIN ProductRawMaterials as z ON (x.ProductID = z.ProductID)
WHERE
		x.Date >= '20200201'
	AND
		x.Date <= '20200201'
	AND
		z.SupplyTypeID = 1;