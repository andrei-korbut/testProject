# 01 – Display Products List

## 📌 Use Case Description

Display a list of all products on the main page.
Products are sorted by creation date (newest first).
Each product shows Name, Type, and Description.
Include action buttons: View, Edit, and Delete.
If no products exist, display: "No products available."
Delete action requires confirmation before executing.

---

## 🔗 Backend Dependency

**GET /products**

Depends on:
- /backend/01-get-products.md

**DELETE /products/{id}**

Depends on:
- /backend/05-delete-product.md

---

## 🧱 Scope of Work

**Components to Create:**
- ProductListPage component
- ProductList component
- ProductCard/ProductItem component
- EmptyState component
- DeleteConfirmationDialog component

**Features:**
- Fetch products on page load via GET /products
- Render product list with:
  - Product Name
  - Product Type (Drink/Food)
  - Product Description
- Action buttons for each product:
  - **View button** → navigates to /products/{id}
  - **Edit button** → navigates to /products/edit/{id}
  - **Delete button** → shows confirmation dialog
- **Create Product button** (always visible) → navigates to /products/create
- Delete confirmation dialog:
  - Message: "Are you sure you want to delete this product?"
  - Confirm button → sends DELETE /products/{id}
  - Cancel button → closes dialog
- Loading state while fetching data
- Error state if API request fails
- Empty state if no products exist

**State Management:**
- Products list state
- Loading state
- Error state
- Selected product for deletion
- Delete confirmation dialog open/closed state

**API Integration:**
- GET /products → fetch all products
- DELETE /products/{id} → delete specific product

---

## 🎨 UX Requirements

**Loading State:**
- Show loading spinner/skeleton while fetching products
- Disable all actions during loading

**Empty State:**
- Display component with text: "No products available."
- Show "Create Product" button prominently
- Use friendly, centered layout

**Error State:**
- Display error notification if GET /products fails
- Show error message from backend or generic message
- Provide "Retry" button to refetch data
- Error notification should be dismissible

**Delete Confirmation:**
- Modal/dialog overlay
- Clear message: "Are you sure you want to delete this product?"
- Two buttons: "Cancel" and "Delete"
- "Delete" button should be visually distinct (e.g., red color)

**Delete Success:**
- Show success notification: "Product deleted successfully"
- Remove deleted product from list immediately (optimistic UI update)
- Auto-dismiss notification after 3 seconds

**Delete Error:**
- Show error notification with backend error message
- Keep product in list
- Error notification should be dismissible

**Product Display:**
- Cards or list items with clear visual separation
- Responsive layout (grid on desktop, stack on mobile)
- Action buttons clearly visible
- Type badge/tag with distinct styling for Drink vs Food

**Interactions:**
- Disable delete button while delete request is in progress
- Prevent duplicate delete requests
- Show loading indicator on delete button during request
- Smooth animations for list updates

---

## 🔐 Validation / Edge Cases

**Network Errors:**
- Handle network failure gracefully
- Display user-friendly error message
- Provide retry mechanism

**Backend Errors:**
- Handle 500 error from backend
- Display error message from backend response
- Handle 404 error if product already deleted by another user

**Concurrent Actions:**
- Prevent multiple simultaneous delete requests
- Disable delete button during delete operation
- Handle case where product was deleted by another user (404 response)

**Edge Cases:**
- Empty product list → show empty state
- Single product → still show as list/grid
- Very long product names or descriptions → truncate or wrap text appropriately
- Product deleted by another user → handle 404 gracefully, remove from UI

---

## 🔗 Dependencies

**Backend:**
- 01 – Get Products (GET /products endpoint)
- 05 – Delete Product (DELETE /products/{id} endpoint)

**Frontend:**
- None (this is the first frontend task)

**External Libraries:**
- React Router (for navigation)
- API client/service for HTTP requests
- (Optional) UI library for notifications (e.g., react-toastify)
- (Optional) UI library for modals/dialogs

---

## 🚫 Out of Scope

- Pagination
- Infinite scroll
- Search/filter functionality
- Sorting options (backend sorts by CreatedAt desc)
- Bulk delete
- Bulk actions
- Product selection with checkboxes
- Undo delete functionality
- Export functionality
- Print functionality

---

## ✅ Acceptance Criteria

- Page loads and automatically fetches products
- Products render correctly with Name, Type, Description
- Products sorted by creation date (newest first)
- All action buttons present: View, Edit, Delete, Create Product
- View button navigates to /products/{id}
- Edit button navigates to /products/edit/{id}
- Create Product button navigates to /products/create
- Delete button shows confirmation dialog
- Confirmation dialog has correct message and buttons
- Cancel button closes dialog without deleting
- Confirm button sends DELETE request and removes product
- Empty state shown when no products exist
- Loading spinner shown while fetching data
- Success notification shown after successful delete
- Error notification shown on API failure
- Delete button disabled during delete operation
- No duplicate delete requests possible
- No console errors
- No TypeScript errors
- Responsive layout works on mobile and desktop
- Proper error handling for all scenarios

---

## 🧪 Testing Requirements

**Component Tests:**
- ProductListPage component renders
- Empty state shown when products array empty
- Loading state shown while fetching
- Products render correctly from mock data
- Action buttons render for each product

**Integration Tests:**
- API call made on component mount (GET /products)
- Delete flow: button → dialog → confirm → API call → UI update
- Navigation triggered correctly for View/Edit/Create buttons
- Error handling when API fails

**User Interaction Tests:**
- Click delete button → dialog opens
- Click cancel → dialog closes
- Click confirm → DELETE request sent
- Success → product removed from list
- Error → product remains in list

**Edge Case Tests:**
- Empty product list displays empty state
- Network error displays error message
- Product already deleted (404) handled gracefully
- Multiple rapid delete clicks prevented
