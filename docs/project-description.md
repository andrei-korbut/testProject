# Project Description: Product Management App

## 2️⃣ Pages & Features

### 2.1 Main Page / Product List (`/`)
- **Features:**
  - UI/frontend on page load will display list of existing products or empty component with text `"No products available."`
  - Products are **sorted by `CreatedAt` descending** by default.
  - For each product, UI renders: Name, Type (Drink/Food), Description
  - Each product has **View / Edit / Delete buttons**:
    - **Delete action:** shows confirmation prompt `"Are you sure you want to delete this product?"`  
      On confirmation, sends `DELETE /products/{id}` request to backend.  
      On success, removes product from list; on failure, shows error notification with backend message.
  - Always visible **Create Product button** navigates to `/products/create`.
- **Endpoints:**
  - `GET /products` → list all products  
  - `DELETE /products/{id}` → delete product  

---

### 2.2 Create Product Page (`/products/create`)
- **Features:**
  - UI/frontend renders form fields for new product:
    - Name (string, max 200) – required, error text: `"Product name is required"`  
    - Description (string, max 1000) – required, error text: `"Product description is required"`  
    - Type (dropdown: `Drink` / `Food`) – required, error text: `"Product type is required"`  
  - **Buttons / Actions:**
    - **Save:** validates fields, sends `POST /products` to backend  
      - On success: redirects to main/list page  
      - On failure: shows error notification with backend message  
    - **Cancel:** redirects back to main/list page without saving
- **Backend Validations:**
  - Name required, max 200, must be **unique**
  - Description required, max 1000
  - Type required, must be valid enum (Drink/Food)
- **Endpoints:**
  - `POST /products` → create new product  

---

### 2.3 Edit Product Page (`/products/edit/{id}`)
- **Features:**
  - UI/frontend renders form fields **pre-filled** with selected product data
  - Fields same as Create Product with validation rules:
    - Validation triggered if user deletes characters or clears fields
    - User cannot save invalid data
  - **Buttons / Actions:**
    - **Save:** validates fields, sends `PUT /products/{id}` to backend  
      - On success: redirects to main/list page  
      - On failure: shows error notification with backend message  
    - **Cancel:** redirects to main/list page without saving changes
- **Backend Validations:**
  - Name required, max 200, must be unique (excluding current product)
  - Description required, max 1000
  - Type required, valid enum
- **Endpoints:**
  - `PUT /products/{id}` → update product  

---

### 2.4 Product Details Page (`/products/{id}`)
- **Features:**
  - UI/frontend displays read-only view of product: Name, Description, Type
  - **Back button:** navigates back to main/list page
- **Endpoints:**
  - `GET /products/{id}` → fetch product details  

---

### 2.5 Delete Product
- **Features:**
  - Triggered from main/list page
  - Shows confirmation prompt `"Are you sure you want to delete this product?"`  
  - On confirm: sends `DELETE /products/{id}` to backend  
  - On success: refreshes product list  
  - On failure: shows error notification with backend message
- **Endpoints:**
  - `DELETE /products/{id}` → delete product  

---

## 3️⃣ Backend Implementation Details

- **Entity:** `Product`
  - Id: int, PK, auto-increment  
  - Name: string, max 200, required, unique  
  - Description: string, max 1000, required  
  - Type: enum in code (Drink/Food), stored as string in DB  
  - CreatedAt: DateTime, automatically set on creation  

- **Repository Layer:**
  - CRUD methods:
    - `GetAllProductsAsync()` → returns products sorted by CreatedAt desc  
    - `GetProductByIdAsync(int id)`  
    - `CreateProductAsync(Product product)`  
    - `UpdateProductAsync(Product product)`  
    - `DeleteProductAsync(int id)`  

- **Service Layer:**
  - Handles validation (required fields, unique Name, valid Type)  
  - Throws exceptions if validation fails or product not found  
  - Delegates DB operations to repository  

- **Controller Layer:**
  - Handles HTTP requests  
  - Returns proper HTTP status codes:
    - 200 OK
    - 201 Created
    - 204 No Content
    - 400 BadRequest
    - 404 NotFound
    - 500 InternalServerError
  - Maps entities/DTOs as needed  

- **Automatic Sorting:**  
  - GET /products → products sorted by `CreatedAt` descending  

- **Validations:**  
  - Enforced at both service layer and EF model annotations  
  - Unique Name validation at service layer  

- **Testing Recommendations:**
  - Unit tests for Service layer  
  - Integration tests for Controller endpoints  
  - Edge case tests (invalid input, duplicate names, not found)  

---

## 4️⃣ Tech Stack

- **Frontend:**  
  - React + TypeScript  
  - Tailwind CSS for styling  
  - React Router or Next.js pages for routing  

- **Backend:**  
  - .NET 8 Web API (C#)  
  - Entity Framework Core  
  - SQL Server (Dockerized)  
  - Automatic EF migrations on startup  

- **DevOps / Containers:**  
  - Docker + Docker Compose for local setup  
  - Environment variables for database connection  
  - Startup sequence: SQL Server → Backend (apply migrations) → Frontend  
