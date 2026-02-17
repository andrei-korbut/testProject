# 01 – Get Products

## 📌 Use Case Description

Retrieve a list of all products from the database.
Products must be sorted by `CreatedAt` in descending order (newest first).
If no products exist, return an empty array.

---

## 🌐 Endpoint Specification

**Method:** GET  
**Route:** /products  

**Request:**
- No body or query parameters required

**Response:**
- 200 OK → array of Product DTOs
- 500 Internal Server Error → unexpected error

**Response DTO:**
```json
[
  {
    "id": 1,
    "name": "Coca Cola",
    "description": "Refreshing drink",
    "type": "Drink",
    "createdAt": "2026-02-17T10:30:00"
  }
]
```

---

## 🧱 Architecture Requirements

This task must implement:

- **Repository:**
  - Use `IRepository<Product>` (from generic repository pattern)
  - Add method or query: `GetAllAsync()` with sorting by CreatedAt desc
  - Can be implemented as extension method or LINQ query in service layer

- **Service Layer:**
  - IProductService interface
  - ProductService implementation
  - Method: `GetAllProductsAsync()`
  - Maps entities to DTOs

- **Controller:**
  - ProductsController
  - GET /products endpoint
  - Returns 200 OK with list of products
  - Handles exceptions and returns 500 on unexpected errors

**Must follow:**
- 3-layer architecture (Controller → Service → Repository)
- Generic repository pattern (`IRepository<Product>`)
- Async/await everywhere
- DTO usage for API responses
- Proper HTTP status codes
- Entity Framework Core for data access

---

## 🔐 Validation Rules

- No validation needed for this endpoint (no input parameters)
- Service layer should never return null (return empty list instead)

---

## 🔗 Dependencies

**Backend:**
- 02 – Create Product (requires Product entity, repository, service, controller structure)

---

## 🚫 Out of Scope

- Pagination
- Filtering
- Search functionality
- Sorting by other fields
- Authorization

---

## ✅ Acceptance Criteria

- GET /products returns 200 OK
- Response contains array of products
- Products sorted by CreatedAt descending
- Empty array returned when no products exist
- Repository method implemented
- Service method implemented
- DTOs used for API response
- No business logic in controller
- Build succeeds without warnings

---

## 🧪 Testing Requirements

- Unit test for Service layer (GetAllProductsAsync)
- Unit test for Repository layer (mocked DbContext or in-memory DB)
- Integration test for Controller endpoint
- Test case: Empty database returns empty array
- Test case: Multiple products returned in correct order
- Test case: Verify CreatedAt sorting
- No exceptions thrown for empty database
