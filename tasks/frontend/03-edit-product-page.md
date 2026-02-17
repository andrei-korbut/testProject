# 03 – Edit Product Page

## 📌 Use Case Description

Provide a form for editing an existing product.
Form is pre-filled with current product data.
User can update Name, Description, and/or Type.
Validate all fields before submission.
On success, navigate to the product list page.
On error, display error notification with backend message.

---

## 🔗 Backend Dependency

**GET /products/{id}**

Depends on:
- /backend/03-get-product-by-id.md

**PUT /products/{id}**

Depends on:
- /backend/04-update-product.md

---

## 🧱 Scope of Work

**Components to Create/Reuse:**
- EditProductPage component
- Reuse ProductForm component from Create Product (with edit mode)

**Features:**
- Extract product ID from URL route parameter (/products/edit/{id})
- Fetch product data on page load via GET /products/{id}
- Pre-fill form fields with fetched product data
- Allow user to edit any field
- Validate fields when user makes changes

**Form Fields:**
- **Name:**
  - Text input, pre-filled
  - Max length: 200 characters
  - Required
  - Error message: "Product name is required"

- **Description:**
  - Textarea, pre-filled
  - Max length: 1000 characters
  - Required
  - Error message: "Product description is required"

- **Type:**
  - Dropdown/Select, pre-selected
  - Options: "Drink", "Food"
  - Required
  - Error message: "Product type is required"

**Form Actions:**
- **Save Button:**
  - Validates all fields
  - Sends PUT /products/{id} request
  - On success: navigate to main page (/)
  - On failure: show error notification with backend message
  - Disabled while request in progress

- **Cancel Button:**
  - Navigate back to main page (/) without saving
  - (Optional) Show confirmation if user has modified data

**State Management:**
- Product ID from route
- Form field values (name, description, type)
- Original product data (for comparison)
- Field errors
- Loading state for initial fetch
- Submission state (loading)
- API error state

**API Integration:**
- GET /products/{id} → fetch product data
- PUT /products/{id} → update product

---

## 🎨 UX Requirements

**Loading State (Initial):**
- Show loading spinner while fetching product data
- Disable form fields until data loaded
- Handle 404 if product not found

**Form Pre-filling:**
- Populate all fields with current product data
- Type dropdown shows current type as selected
- Form should appear "pristine" (not dirty) initially

**Validation:**
- Trigger validation when user modifies fields
- Show error message below field when invalid
- Mark required fields with asterisk (*)
- Disable Save button if form invalid or unchanged

**Save Button State:**
- Disabled if form invalid
- (Optional) Disabled if form unchanged
- Show loading indicator during submission
- Text: "Save" or "Saving..." during submission

**Loading State (Submission):**
- Show loading indicator on Save button
- Disable all form fields during submission
- Disable Cancel button during submission

**Success:**
- Navigate to main page (/)
- (Optional) Show success notification: "Product updated successfully"

**Error Handling:**
- Display error notification for API errors
- Show backend error message if available
- Show field-specific errors if backend returns validation errors
- Generic error message for network failures
- Error notification dismissible by user

**Product Not Found (404):**
- Show error message: "Product not found"
- Provide button to navigate back to main page
- (Optional) Show notification and auto-redirect

**Field Validation Messages:**
- Same as Create Product page
- Additional: "A product with this name already exists" for duplicate names

**Visual Feedback:**
- Invalid fields highlighted with red border
- Character count for Name and Description
- Clear focus states on all inputs
- Indicate unsaved changes (optional)

---

## 🔐 Validation / Edge Cases

**Client-Side Validation:**
- Name: required, max 200 characters
- Description: required, max 1000 characters
- Type: required, must be selected

**Backend Validation:**
- Handle 400 Bad Request for validation errors
- Handle 404 Not Found if product deleted by another user
- Display backend error messages

**Edge Cases:**
- Product not found (404) → show error and redirect
- Product deleted during edit → handle 404 gracefully
- Network failure → show error notification
- Duplicate name → show backend error
- User clears required field → show validation error
- Whitespace-only input → treat as empty
- No changes made → optionally disable Save button
- User navigates away during fetch → cancel request
- User navigates away during submission → cancel request

**Data Handling:**
- Trim whitespace from Name and Description before submission
- Validate Type is one of allowed enum values
- Allow saving with same name (current product)

---

## 🔗 Dependencies

**Backend:**
- 03 – Get Product By Id (GET /products/{id} endpoint)
- 04 – Update Product (PUT /products/{id} endpoint)

**Frontend:**
- 01 – Display Products List (navigation source and target)
- 02 – Create Product Page (reuse ProductForm component)

**External Libraries:**
- React Router (for route parameters and navigation)
- Form library (optional)
- API client/service for HTTP requests

---

## 🚫 Out of Scope

- Edit history/audit trail
- Compare changes before saving
- Image upload
- Rich text editor
- Auto-save
- Optimistic locking/version control
- Undo/redo functionality
- Side-by-side comparison with original

---

## ✅ Acceptance Criteria

- Page extracts product ID from route
- GET /products/{id} called on page load
- Form pre-filled with fetched product data
- All fields editable: Name, Description, Type
- Required field validation works
- Max length validation works
- Save button triggers validation
- Valid form sends PUT /products/{id} request
- Success navigates to main page (/)
- Error shows notification with backend message
- 404 error handled (product not found)
- Duplicate name error handled
- Cancel button navigates to main page
- Form disabled during initial load
- Form disabled during submission
- Loading indicator shown during fetch and submission
- No console errors
- No TypeScript errors
- Responsive layout
- Accessible form

---

## 🧪 Testing Requirements

**Component Tests:**
- EditProductPage component renders
- Loading state shown initially
- Form renders after data loaded
- Form pre-filled correctly
- Type dropdown shows current type

**Validation Tests:**
- Empty Name shows error
- Empty Description shows error
- No Type selected shows error
- Max length validation works
- Valid form enables Save button

**Integration Tests:**
- GET /products/{id} called on mount
- Form populated with fetched data
- PUT /products/{id} called with updated data
- Success navigates to /
- 400 error displays validation messages
- 404 error handled gracefully
- Duplicate name shows specific error
- Network error shows notification

**User Interaction Tests:**
- Edit fields and click Save → API called
- Click Cancel → navigates to /
- Enter invalid data → errors shown
- Modify then revert → (optionally) Save disabled
- Submit invalid form → prevented

**Edge Case Tests:**
- Product not found → error displayed
- Product deleted during edit → 404 handled
- Whitespace trimming
- Form disabled during operations
- Multiple submit clicks prevented
- Same name as current product allowed
