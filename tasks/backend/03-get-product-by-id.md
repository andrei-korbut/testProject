# 03 – Get Product By Id

## 📌 Use Case Description

Retrieve a single product by its unique identifier.
If the product exists, return 200 OK with product details.
If the product does not exist, return 404 Not Found.

---

## 🌐 Endpoint Specification

**Method:** GET  
**Route:** /products/{id}  

**Request:**
- Path parameter: `id` (int, required)

**Response:**
- 200 OK → Product DTO
- 404 Not Found → product does not exist
- 400 Bad Request → invalid id format
- 500 Internal Server Error → unexpected error

**Success Response (200):**
```json
{
  "id": 3,
  "name": "Cappuccino",
  "description": "Italian coffee drink",
  "type": "Drink",
  "createdAt": "2026-02-15T09:20:00"
}
```

**Error Response (404):**
```json
{
  "message": "Product with id 3 not found"
}
```

---

## 🧱 Architecture Requirements

This task must implement:

- **Repository:**
  - Use `IRepository<Product>` (from generic repository pattern)
  - Use `GetByIdAsync(int id)` method → returns product or null

- **Service Method:**
  - `GetProductByIdAsync(int id)`
  - Calls repository
  - Throws exception if product not found
  - Maps entity to DTO

- **Controller Endpoint:**
  - GET /products/{id}
  - Returns 200 OK with product data
  - Returns 404 Not Found if product doesn't exist
  - Returns 400 Bad Request for invalid id
  - Returns 500 for unexpected errors

**Must follow:**
- 3-layer architecture
- Generic repository pattern (`IRepository<Product>`)
- Async/await everywhere
- DTO usage for response
- Proper HTTP status codes
- Exception handling for not found case

---

## 🔐 Validation Rules

- `id` must be a valid integer
- `id` must be greater than 0
- If product not found, throw `NotFoundException` or similar
- Service layer throws exception, controller catches and returns 404

**Error Messages:**
- Product not found: "Product with id {id} not found"
- Invalid id: "Invalid product id"

---

## 🔗 Dependencies

**Backend:**
- 02 – Create Product (requires Product entity, repository, service, controller structure)

---

## 🚫 Out of Scope

- Related products
- Product history
- View count tracking
- Caching
- Authorization

---

## ✅ Acceptance Criteria

- GET /products/{id} returns 200 OK for existing product
- Returns 404 Not Found for non-existent product
- Returns 400 Bad Request for invalid id (e.g., negative, non-numeric)
- Response includes all product fields
- No business logic in controller
- Service layer throws exception for not found
- Controller handles exception and returns proper status
- DTOs used for response
- Build succeeds without warnings

---

## 🧪 Testing Requirements

- Unit test: Service returns product for valid id
- Unit test: Service throws exception for non-existent id
- Integration test: GET /products/{id} returns 200 for existing product
- Integration test: GET /products/{id} returns 404 for non-existent product
- Integration test: GET /products/invalid returns 400
- Integration test: GET /products/-1 returns 400
- Test case: Verify all fields returned correctly
- Test case: Verify exception handling
