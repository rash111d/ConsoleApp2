CREATE TABLE Books
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100),
    Author NVARCHAR(100),
    PublishYear INT,
    Quantity INT
);