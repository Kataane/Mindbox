CREATE EXTENSION IF NOT EXISTS "uuid-ossp"; -- For uuid/guid

CREATE TABLE Products (
    ProductID UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    ProductName VARCHAR(100)
);

CREATE TABLE Categories (
    CategoryID UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    CategoryName VARCHAR(100)
);

CREATE TABLE ProductCategory (
    ProductID UUID,
    CategoryID UUID,
    PRIMARY KEY (ProductID, CategoryID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

INSERT INTO Products (ProductID, ProductName) VALUES
('795099c1-810a-4b5d-8527-325e07178fa6', 'Product A'),
('94351434-d9a1-47d4-b42d-3e35e67b5ca7', 'Product B'),
('8474271e-6abd-492d-b50a-dc49ddc1867f', 'Product C'),
('081586e8-97d8-4a28-a37f-65a095a3468e', 'Product D');

INSERT INTO Categories (CategoryID, CategoryName) VALUES
('13043fb5-3bd6-4e96-8976-33157ae4d9ab', 'Category V'),
('f4579485-0602-453e-9ed4-2d9c530781e8', 'Category X'),
('6f03a1ee-fda7-4f21-bfdf-c47e798587a5', 'Category Y'),
('0a4f70ed-346d-4583-a08e-4a875c7d6a0a', 'Category Z');


INSERT INTO ProductCategory (ProductID, CategoryID) VALUES
('795099c1-810a-4b5d-8527-325e07178fa6', 'f4579485-0602-453e-9ed4-2d9c530781e8'), -- Product A belongs to category X
('795099c1-810a-4b5d-8527-325e07178fa6', '6f03a1ee-fda7-4f21-bfdf-c47e798587a5'), -- Product A belongs to category Y
('94351434-d9a1-47d4-b42d-3e35e67b5ca7', '6f03a1ee-fda7-4f21-bfdf-c47e798587a5'), -- Product B belongs to category Y
('8474271e-6abd-492d-b50a-dc49ddc1867f', '0a4f70ed-346d-4583-a08e-4a875c7d6a0a'); -- Product C belongs to category Z

SELECT p.ProductName, COALESCE(c.CategoryName, 'No Category') AS CategoryName
FROM Products p
LEFT JOIN ProductCategory pc ON p.ProductID = pc.ProductID
LEFT JOIN Categories c ON pc.CategoryID = c.CategoryID;

-- Еxpected
-- "Product A" "Category X"
-- "Product A" "Category Y"
-- "Product B" "Category Y"
-- "Product C" "Category Z"
-- "Product D" "No Category"