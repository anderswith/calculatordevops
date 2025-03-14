CREATE TABLE Calculations (
      CalculationId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
      CalcString NVARCHAR(100),
      Result INT,
      A INT,
      B INT,
      Operation NVARCHAR(1)
);