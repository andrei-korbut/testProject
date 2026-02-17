# 02 – Create Product Page

## 📌 Use Case Description

Provide a form for creating a new product.
User enters Name, Description, and selects Type (Drink/Food).
Validate all fields before submission.
On success, navigate to the product list page.
On error, display error notification with backend message.

---

## 🔗 Backend Dependency

**POST /products**

Depends on:
- /backend/02-create-product.md

---

## 🧱 Scope of Work

**Components to Create:**
- CreateProductPage component
- ProductForm component (reusable for Create and Edit)

**Form Fields:**
- **Name:**
  - Text input
  - Max length: 200 characters
  - Required
  - Error message: "Product name is required"

- **Description:**
  - Textarea
  - Max length: 1000 characters
  - Required
  - Error message: "Product description is required"

- **Type:**
  - Dropdown/Select with options: "Drink", "Food"
  - Required
  - Error message: "Product type is required"

**Form Actions:**
- **Save Button:**
  - Validates all fields
  - Sends POST /products request
  - On success: navigate to main page (/)
  - On failure: show error notification with backend message
  - Disabled while request in progress

- **Cancel Button:**
  - Navigate back to main page (/) without saving
  - No confirmation needed if form is empty
  - (Optional) Show confirmation if user has entered data

**State Management:**
- Form field values (name, description, type)
- Field errors (name, description, type)
- Form submission state (loading)
- API error state

**API Integration:**
- POST /products → create new product

---

## 🎨 UX Requirements

**Form Layout:**
- Clear, vertical layout
- Each field has label
- Input fields with appropriate sizing
- Textarea for description (multiline)
- Dropdown for type selection

**Validation:**
- Real-time validation on blur or change
- Show error message below field when invalid
- Mark required fields with asterisk (*)
- Disable Save button if form invalid

**Loading State:**
- Show loading indicator on Save button during submission
- Disable all form fields during submission
- Disable Cancel button during submission

**Success:**
- Navigate to main page (/)
- (Optional) Show success notification: "Product created successfully"
- Clear form data after navigation

**Error Handling:**
- Display error notification for API errors
- Show backend error message if available
- Show field-specific errors if backend returns validation errors
- Generic error message for network failures
- Error notification dismissible by user

**Field Validation Messages:**
- Name required: "Product name is required"
- Name too long: "Product name cannot exceed 200 characters"
- Description required: "Product description is required"
- Description too long: "Product description cannot exceed 1000 characters"
- Type required: "Product type is required"
- Duplicate name (from backend): "A product with this name already exists"

**Visual Feedback:**
- Invalid fields highlighted with red border
- Valid fields with green checkmark (optional)
- Character count for Name and Description fields
- Clear focus states on all inputs

---

## 🔐 Validation / Edge Cases

**Client-Side Validation:**
- Name: required, max 200 characters
- Description: required, max 1000 characters
- Type: required, must be selected

**Backend Validation:**
- Handle 400 Bad Request for validation errors
- Display backend error messages
- Map backend field errors to form fields

**Edge Cases:**
- Empty form submission → show all required field errors
- Whitespace-only input → treat as empty
- Network failure → show error notification
- Duplicate product name → show backend error message
- User navigates away during submission → cancel request
- Browser back button → confirm if form has data

**Data Handling:**
- Trim whitespace from Name and Description before submission
- Validate Type is one of the allowed enum values

---

## 🔗 Dependencies

**Backend:**
- 02 – Create Product (POST /products endpoint)

**Frontend:**
- 01 – Display Products List (navigation target after success)

**External Libraries:**
- React Router (for navigation)
- Form library (optional: React Hook Form, Formik)
- API client/service for HTTP requests

---

## 🚫 Out of Scope

- Image upload
- Rich text editor for description
- Auto-save/draft functionality
- Product preview
- Multi-step form
- Additional fields (price, category, etc.)
- Duplicate product functionality
- Import from template

---

## ✅ Acceptance Criteria

- Page renders create product form
- All fields present: Name, Description, Type
- Type dropdown has "Drink" and "Food" options
- Required field validation works
- Max length validation works (200 for Name, 1000 for Description)
- Save button triggers validation
- Valid form sends POST /products request
- Success navigates to main page (/)
- Error shows notification with backend message
- Backend validation errors displayed to user
- Duplicate name error handled properly
- Cancel button navigates to main page
- Form disabled during submission
- Loading indicator shown during submission
- No console errors
- No TypeScript errors
- Responsive layout on mobile and desktop
- Accessible form (labels, ARIA attributes)

---

## 🧪 Testing Requirements

**Component Tests:**
- CreateProductPage component renders
- Form renders all fields correctly
- Type dropdown has correct options
- Save and Cancel buttons present

**Validation Tests:**
- Empty Name shows error
- Empty Description shows error
- No Type selected shows error
- Name > 200 characters shows error
- Description > 1000 characters shows error
- Valid form enables Save button

**Integration Tests:**
- POST /products called with correct data
- Success navigates to /
- 400 error displays validation messages
- Duplicate name shows specific error
- Network error shows error notification

**User Interaction Tests:**
- Fill form and click Save → API called
- Click Cancel → navigates to /
- Enter invalid data → errors shown
- Fill valid data → errors cleared
- Submit invalid form → prevented

**Edge Case Tests:**
- Whitespace-only input treated as empty
- Trimming whitespace before submission
- Form disabled during submission
- Multiple submit clicks prevented
