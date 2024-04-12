# Product Website

A website that displays a list of products, the user can select a product and update or delete it. Users can also create a product.<br>
Products also belong to a category, a list of available categories is provided when the user modifies or creates a product.<br>

# How to Run

1. clone the project
```
git clone https://github.com/Ben-JClark/interview-assignment.git
```
2. Open "SeekProject.sln" in Visual Studio 2022 Community Edition
3. Open "Package Manager Console" and update the database
```
update-database
```
4. Press the project start button in Visual Studio
5. To navigate to the React UI, navigate to the URL
```
https://localhost:5173/
```

## API endpoints
To get all the categories
```
/api/categories
```
To get all the products
```
GET /api/products
```
To get a single product by ID. 
```
GET /api/products/{id}
```
To add a new product
```
POST /api/products
```
To update an existing product
```
PUT /api/products/{id}
```
To Delete a product
```
DELETE /api/products/{id}
```
## Database Seeded data
```
new Category { Id = 1, Name = "Technology" },
new Category { Id = 2, Name = "Produce" },
new Category { Id = 3, Name = "Furnature" },
new Category { Id = 4, Name = "Gardening" }
```
```
new Product { Id = 1, Name = "Laptop", Description = "Portable computer", Price = 999.99M, CategoryId=1 },
new Product { Id = 2, Name = "Smartphone", Description = "Handheld device", Price = 499.99M, CategoryId = 1 }
```

## Recomended IDE
Visual Studio 2022 Community Edition
