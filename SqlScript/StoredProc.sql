CREATE PROCEDURE pr_GetOrderSummary
    @StartDate DATE = NULL,
    @EndDate DATE = NULL,
    @EmployeeID INT = NULL,
    @CustomerID NCHAR(5) = NULL
AS
BEGIN
    SELECT 
        (emp.TitleOfCourtesy + ' ' + emp.FirstName + ' ' + emp.LastName) AS EmployeeFullName,
        ship.CompanyName AS ShipperCompanyName,
        cust.CompanyName AS CustomerCompanyName,
        COUNT(ord.OrderID) AS NumberOfOrders,
        CONVERT(DATE, ord.OrderDate) AS [Date],
        SUM(ord.Freight) AS TotalFreightCost,
        COUNT(DISTINCT orddet.ProductID) AS NumberOfDifferentProducts,
        SUM(orddet.UnitPrice * orddet.Quantity) AS TotalOrderValue
    FROM 
        Orders ord
    INNER JOIN 
        [Order Details] orddet ON ord.OrderID = orddet.OrderID
    INNER JOIN 
        Employees emp ON ord.EmployeeID = emp.EmployeeID
    INNER JOIN 
        Shippers ship ON ord.ShipVia = ship.ShipperID
    INNER JOIN 
        Customers cust ON ord.CustomerID = cust.CustomerID
    WHERE 
        (@StartDate IS NULL OR ord.OrderDate >= @StartDate)
        AND (@EndDate IS NULL OR ord.OrderDate <= @EndDate)
        AND (@EmployeeID IS NULL OR ord.EmployeeID = @EmployeeID)
        AND (@CustomerID IS NULL OR ord.CustomerID = @CustomerID)
    GROUP BY 
        CONVERT(DATE, ord.OrderDate), 
        emp.TitleOfCourtesy, emp.FirstName, emp.LastName,
        ship.CompanyName,
        cust.CompanyName
    ORDER BY 
        [Date], EmployeeFullName, CustomerCompanyName, ShipperCompanyName;
END;