# 02 – Create Product

## 📌 Use Case Description

Create a new product with Name, Description, and Type.
Validate that Name is unique, all required fields are present, and Type is valid.
On success, return the created product with 201 Created status.
CreatedAt timestamp is automatically set by the system.

---

## 🌐 Endpoint Specification

**Method:** POST  
**Route:** /products  

**Request Body:**
```json
{
  "name": "Sprite",
  "description": "Lemon-lime flavored drink",
  "type": "Drink"
}
```

**Response:**
- 201 Created → returns created Product DTO with Location header
- 400 Bad Request → validation failed (missing fields, invalid type, duplicate name)
- 500 Internal Server Error → unexpected error

**Success Response (201):**
```json
{
  "id": 5,
  "name": "Sprite",
  "description": "Lemon-lime flavored drink",
  "type": "Drink",
  "createdAt": "2026-02-17T14:25:00"
}
```

**Error Response (400):**
```json
{
  "message": "Product name must be unique",
  "errors": {
    "Name": ["A product with this name already exists"]
  }
}
```

---

## 🧱 Architecture Requirements

This task must implement:

- **Product Entity:**
  - Id: int, PK, auto-increment
  - Name: string, max 200, required, unique
  - Description: string, max 1000, required
  - Type: enum (Drink/Food), stored as string in DB
  - CreatedAt: DateTime, automatically set on creation

- **Request DTO:**
  - CreateProductDto with Name, Description, Type

- **Repository Layer:**
  - `IRepository<TEntity>` generic interface with common CRUD operations:
    - `GetByIdAsync(int id)`
    - `GetAllAsync()`
    - `AddAsync(TEntity entity)`
    - `UpdateAsync(TEntity entity)`
    - `DeleteAsync(int id)`
    - `SaveChangesAsync()`
  - `Repository<TEntity>` generic base class implementation
  - Use `IRepository<Product>` for product operations
  - Add product-specific extension/method:
    - `ProductExistsByNameAsync(string name)` → checks name uniqueness

- **Service Method:**
  - `CreateProductAsync(CreateProductDto dto)`
  - Validates required fields
  - Validates Type enum value
  - Checks name uniqueness
  - Maps DTO to entity
  - Saves product
  - Returns created product DTO

- **Controller Endpoint:**
  - POST /products
  - Returns 201 Created with Location header
  - Returns 400 Bad Request for validation errors
  - Returns 500 for unexpected errors

**Must follow:**
- 3-layer architecture (Controller → Service → Repository)
- Repository pattern
- Async/await everywhere
- DTO usage for request/response
- Proper HTTP status codes
- Service layer handles all validation
- Entity Framework Core for data access

**This task creates the foundation:**
- DbContext configuration
- Product entity
- Database connection setup
- Generic repository pattern (`IRepository<TEntity>` and `Repository<TEntity>`)
- Base service structure
- Controller setup

---

## 🔐 Validation Rules

**Service Layer Validations:**
- Name: required, max 200 characters, must be unique
- Description: required, max 1000 characters
- Type: required, must be valid enum value ("Drink" or "Food")

**Validation Error Messages:**
- Name required: "Product name is required"
- Name too long: "Product name cannot exceed 200 characters"
- Name duplicate: "A product with this name already exists"
- Description required: "Product description is required"
- Description too long: "Product description cannot exceed 1000 characters"
- Type required: "Product type is required"
- Type invalid: "Product type must be either 'Drink' or 'Food'"

**Business Rules:**
- CreatedAt is set automatically and cannot be provided in request
- Id is auto-generated and cannot be provided in request

---

## 🔗 Dependencies

**None** – This is the foundation task that creates the base architecture.

All other backend tasks depend on this task.

---

## 🚫 Out of Scope

- Image upload
- Bulk creation
- Draft/publish workflow
- User/creator tracking
- Authorization

---

## ✅ Acceptance Criteria

- POST /products creates new product
- Returns 201 Created with product data
- Location header points to new resource (/products/{id})
- CreatedAt automatically set
- Returns 400 for missing required fields
- Returns 400 for duplicate name
- Returns 400 for invalid Type
- Name uniqueness enforced
- Product saved to database
- Product entity created with all required fields
- Repository pattern implemented
- Service layer implemented
- Controller implemented
- No business logic in controller
- Validation happens in service layer
- DTOs used for request/response
- Build succeeds without warnings
- EF Core migrations created and applied
- Database connection configured

---

## 🧪 Testing Requirements

- Unit test: Service validates required fields
- Unit test: Service validates name uniqueness
- Unit test: Service validates Type enum
- Unit test: Service creates product successfully
- Integration test: POST /products returns 201
- Integration test: Duplicate name returns 400
- Integration test: Missing fields return 400
- Integration test: Invalid Type returns 400
- Test case: Verify CreatedAt is set automatically
- Test case: Verify Location header in response
