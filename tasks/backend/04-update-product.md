# 04 – Update Product

## 📌 Use Case Description

Update an existing product's Name, Description, and/or Type.
Validate that the product exists, all required fields are provided, and Name is unique (excluding the current product being updated).
On success, return 200 OK with updated product data.
CreatedAt remains unchanged.

---

## 🌐 Endpoint Specification

**Method:** PUT  
**Route:** /products/{id}  

**Request:**
- Path parameter: `id` (int, required)
- Body: UpdateProductDto

**Request Body:**
```json
{
  "name": "Coca Cola Zero",
  "description": "Zero sugar cola drink",
  "type": "Drink"
}
```

**Response:**
- 200 OK → returns updated Product DTO
- 400 Bad Request → validation failed (missing fields, invalid type, duplicate name)
- 404 Not Found → product does not exist
- 500 Internal Server Error → unexpected error

**Success Response (200):**
```json
{
  "id": 1,
  "name": "Coca Cola Zero",
  "description": "Zero sugar cola drink",
  "type": "Drink",
  "createdAt": "2026-02-10T08:15:00"
}
```

**Error Response (400):**
```json
{
  "message": "Validation failed",
  "errors": {
    "Name": ["A product with this name already exists"]
  }
}
```

**Error Response (404):**
```json
{
  "message": "Product with id 1 not found"
}
```

---

## 🧱 Architecture Requirements

This task must implement:

- **Request DTO:**
  - UpdateProductDto with Name, Description, Type

- **Repository:**
  - Use `IRepository<Product>` (from generic repository pattern)
  - Use `GetByIdAsync(int id)` → fetches existing product
  - Use `UpdateAsync(Product product)` → saves changes
  - Use `SaveChangesAsync()` → persists changes
  - Add product-specific method:
    - `ProductExistsByNameAsync(string name, int excludeId)` → checks uniqueness excluding current product

- **Service Method:**
  - `UpdateProductAsync(int id, UpdateProductDto dto)`
  - Validates product exists
  - Validates required fields
  - Validates Type enum
  - Checks name uniqueness (excluding current product)
  - Updates product properties
  - Saves changes
  - Returns updated product DTO

- **Controller Endpoint:**
  - PUT /products/{id}
  - Returns 200 OK with updated product
  - Returns 400 Bad Request for validation errors
  - Returns 404 Not Found if product doesn't exist
  - Returns 500 for unexpected errors

**Must follow:**
- 3-layer architecture
- Generic repository pattern (`IRepository<Product>`)
- Async/await everywhere
- DTO usage for request/response
- Proper HTTP status codes
- Service layer handles all validation
- CreatedAt must not be modified

---

## 🔐 Validation Rules

**Service Layer Validations:**
- Product must exist (return 404 if not)
- Name: required, max 200 characters, must be unique (excluding current product)
- Description: required, max 1000 characters
- Type: required, must be valid enum value ("Drink" or "Food")

**Validation Error Messages:**
- Product not found: "Product with id {id} not found"
- Name required: "Product name is required"
- Name too long: "Product name cannot exceed 200 characters"
- Name duplicate: "A product with this name already exists"
- Description required: "Product description is required"
- Description too long: "Product description cannot exceed 1000 characters"
- Type required: "Product type is required"
- Type invalid: "Product type must be either 'Drink' or 'Food'"

**Business Rules:**
- CreatedAt cannot be modified
- Id cannot be changed
- When checking name uniqueness, exclude the current product being updated

---

## 🔗 Dependencies

**Backend:**
- 02 – Create Product (requires Product entity, repository, service, controller structure)

---

## 🚫 Out of Scope

- Partial updates (PATCH)
- Update history/audit trail
- Optimistic concurrency control
- Bulk updates
- Authorization

---

## ✅ Acceptance Criteria

- PUT /products/{id} updates existing product
- Returns 200 OK with updated product data
- Returns 404 Not Found for non-existent product
- Returns 400 for missing required fields
- Returns 400 for duplicate name (when name changed to existing name)
- Returns 400 for invalid Type
- Allows updating to same name (current product's name)
- CreatedAt remains unchanged after update
- All fields can be updated independently
- No business logic in controller
- Validation happens in service layer
- DTOs used for request/response
- Build succeeds without warnings

---

## 🧪 Testing Requirements

- Unit test: Service validates product exists
- Unit test: Service validates required fields
- Unit test: Service validates name uniqueness (excluding current product)
- Unit test: Service validates Type enum
- Unit test: Service updates product successfully
- Unit test: Service allows same name for current product
- Integration test: PUT /products/{id} returns 200
- Integration test: Non-existent product returns 404
- Integration test: Duplicate name returns 400
- Integration test: Missing fields return 400
- Integration test: Invalid Type returns 400
- Test case: Verify CreatedAt not modified
- Test case: Update name only
- Test case: Update all fields
- Test case: Update to same name succeeds
