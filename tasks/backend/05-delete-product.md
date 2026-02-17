# 05 – Delete Product

## 📌 Use Case Description

Delete an existing product by its unique identifier.
If the product exists, remove it from the database and return 204 No Content.
If the product does not exist, return 404 Not Found.
This is a hard delete operation (product is permanently removed).

---

## 🌐 Endpoint Specification

**Method:** DELETE  
**Route:** /products/{id}  

**Request:**
- Path parameter: `id` (int, required)

**Response:**
- 204 No Content → deletion successful (no response body)
- 404 Not Found → product does not exist
- 400 Bad Request → invalid id format
- 500 Internal Server Error → unexpected error

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
  - Use `GetByIdAsync(int id)` → checks if product exists
  - Use `DeleteAsync(int id)` → deletes product from database
  - Use `SaveChangesAsync()` → persists changes

- **Service Method:**
  - `DeleteProductAsync(int id)`
  - Validates product exists
  - Throws exception if not found
  - Calls repository to delete
  - No return value (void/Task)

- **Controller Endpoint:**
  - DELETE /products/{id}
  - Returns 204 No Content on success
  - Returns 404 Not Found if product doesn't exist
  - Returns 400 Bad Request for invalid id
  - Returns 500 for unexpected errors

**Must follow:**
- 3-layer architecture
- Generic repository pattern (`IRepository<Product>`)
- Async/await everywhere
- Proper HTTP status codes (204 for successful delete)
- No response body for 204 status
- Exception handling for not found case

---

## 🔐 Validation Rules

- `id` must be a valid integer
- `id` must be greater than 0
- Product must exist before deletion
- If product not found, throw `NotFoundException` or similar
- Service layer throws exception, controller catches and returns 404

**Error Messages:**
- Product not found: "Product with id {id} not found"
- Invalid id: "Invalid product id"

**Business Rules:**
- Hard delete (permanent removal from database)
- No soft delete
- No undo functionality

---

## 🔗 Dependencies

**Backend:**
- 02 – Create Product (requires Product entity, repository, service, controller structure)

---

## 🚫 Out of Scope

- Soft delete (IsDeleted flag)
- Delete history/audit trail
- Bulk delete
- Cascade delete of related entities (no relations in this project)
- Restore/undo functionality
- Authorization
- Delete confirmation at backend level

---

## ✅ Acceptance Criteria

- DELETE /products/{id} removes product from database
- Returns 204 No Content on success
- No response body for successful delete
- Returns 404 Not Found for non-existent product
- Returns 400 Bad Request for invalid id
- Product no longer exists in database after deletion
- Subsequent GET /products/{id} returns 404 for deleted product
- No business logic in controller
- Service layer throws exception for not found
- Controller handles exception and returns proper status
- Build succeeds without warnings

---

## 🧪 Testing Requirements

- Unit test: Service deletes product successfully
- Unit test: Service throws exception for non-existent product
- Integration test: DELETE /products/{id} returns 204
- Integration test: DELETE /products/{id} returns 404 for non-existent
- Integration test: Verify product removed from database
- Integration test: GET /products/{id} returns 404 after delete
- Integration test: DELETE /products/invalid returns 400
- Integration test: DELETE /products/-1 returns 400
- Test case: Verify product count decreases after delete
- Test case: Verify exception handling
