# 02 – Frontend Project Setup

## 📌 Priority
**HIGH** – Should be completed after backend + database setup

---

## 📝 Description

Set up the frontend React + TypeScript project in Docker with Tailwind CSS styling and a minimal placeholder home page. This task ensures the frontend container can build and run successfully, providing the foundation for all UI development.

---

## 🛠️ Technology Stack

| Component | Technology | Version |
|-----------|-----------|---------|
| Frontend Framework | React | 18.x |
| Language | TypeScript | 5.x |
| Styling | Tailwind CSS | 3.x |
| Build Tool | Create React App | 5.x |
| Container | Docker | Latest |
| Node Runtime | Node.js | 20.x LTS |

---

## 📋 Implementation Steps

### 1️⃣ Frontend Project Initialization

- Navigate to `frontend/` folder
- Initialize React + TypeScript project:
  ```bash
  npx create-react-app . --template typescript
  ```
- Verify project structure created successfully
- Test local development server: `npm start` (should open on port 3000)

### 2️⃣ Tailwind CSS Configuration

- Install Tailwind CSS and dependencies:
  ```bash
  npm install -D tailwindcss postcss autoprefixer
  npx tailwindcss init -p
  ```
- Configure `tailwind.config.js`:
  ```js
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ]
  ```
- Update `src/index.css` to include Tailwind directives:
  ```css
  @tailwind base;
  @tailwind components;
  @tailwind utilities;
  ```
- Remove default Create React App styles
- Verify Tailwind classes work in development

### 3️⃣ Create Default Home Page

- Create `src/pages/Home.tsx`:
  - Display centered title: "Product Management App"
  - Use Tailwind CSS classes for styling
  - Modern, clean design with responsive layout
- Update `src/App.tsx`:
  - Import and render Home component
  - Remove default Create React App content
  - Add basic layout structure
- Test that home page renders correctly

### 4️⃣ Environment Configuration

- Create `.env` file in frontend folder:
  ```
  REACT_APP_API_URL=http://localhost:5000
  ```
- Add `.env` to `.gitignore`
- Create `.env.example` with template values (to be committed)
- Document environment variables in README (optional at this stage)

### 5️⃣ Docker Setup

Create `Dockerfile` in frontend folder:
- **Development Stage:**
  - Base image: `node:20-alpine`
  - Working directory: `/app`
  - Copy `package*.json` and run `npm install`
  - Copy source code
  - Expose port `3000`
  - Command: `npm start`
- Use multi-stage build for future production optimization

Update `docker-compose.yml` in project root:
- Add **frontend** service:
  - Build from `./frontend`
  - Port mapping: `3000:3000`
  - Environment variable: `REACT_APP_API_URL=http://localhost:5000`
  - Volume mounts for hot reload:
    - `./frontend/src:/app/src`
    - `./frontend/public:/app/public`
  - Depends on `backend` service (optional)

---

## 🌐 URLs & Access Points

### Frontend URLs
- **External (Host):** `http://localhost:3000`
- **Internal (Docker):** `http://frontend:3000`

### API Configuration
- **Backend API URL:** `http://localhost:5000` (from host)
- **Environment Variable:** `REACT_APP_API_URL`

---

## 🔗 Dependencies

**Prerequisites:**
- Node.js 20.x installed (for local development)
- Docker Desktop running
- Task 01 (Backend & Database Setup) **completed**

**Depends On:**
- 01 – Backend Project & Database Setup (should be completed first)

---

## ✅ Acceptance Criteria

- [ ] React + TypeScript project created successfully
- [ ] Tailwind CSS installed and configured
- [ ] Home page component created with "Product Management App" title
- [ ] Home page displays correctly when running locally (`npm start`)
- [ ] Environment variable file (`.env`) exists and configured
- [ ] `.env.example` created with template values
- [ ] `Dockerfile` for frontend builds successfully
- [ ] `docker-compose.yml` updated with frontend service
- [ ] Docker containers (frontend + backend + database) all start: `docker-compose up -d`
- [ ] Can access `http://localhost:3000` and see home page
- [ ] Hot reload works (make change in `Home.tsx`, see update without restart)
- [ ] No console errors in browser
- [ ] No TypeScript compilation errors
- [ ] Build completes without warnings
- [ ] Frontend can build for production: `npm run build`

---

## 🧪 Testing & Validation

### Manual Testing Steps:
1. Navigate to `frontend/` folder
2. Run `npm install` to verify dependencies
3. Run `npm start` to test local development server
4. Open browser to `http://localhost:3000`
5. Verify "Product Management App" title displays
6. Verify Tailwind styles are applied correctly
7. Stop local server (Ctrl+C)
8. From project root, run `docker-compose build frontend`
9. Run `docker-compose up -d` to start all containers
10. Open browser to `http://localhost:3000`
11. Verify home page renders in Docker container
12. Check browser console for errors (should be none)
13. Modify `Home.tsx` title and verify hot reload works
14. Run `npm run build` to test production build

### Expected Behavior:
- Frontend container starts within 60 seconds
- Home page loads in < 2 seconds
- Hot reload triggers within 1-2 seconds of file change
- No 404 errors or broken assets
- No TypeScript errors in development console
- Production build completes successfully

---

## 📐 Home Page Design Spec

### Basic Layout:
```tsx
// src/pages/Home.tsx
<div className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100 flex items-center justify-center">
  <div className="text-center">
    <h1 className="text-5xl font-bold text-gray-800 mb-4">
      Product Management App
    </h1>
    <p className="text-gray-600 text-lg">
      Your products, managed efficiently
    </p>
  </div>
</div>
```

This is a placeholder - future tasks will add navigation, product list, etc.

---

## 📌 Notes

- Create React App includes hot reload by default
- Volume mounts in docker-compose enable hot reload in container
- Tailwind CSS may require container restart to detect new classes
- Consider setting up React Router in future tasks
- API calls will be implemented in future feature tasks
- TypeScript strict mode is enabled by default (keep it enabled)
- The frontend can start even if backend is not ready (show error gracefully later)

---

## 🚫 Out of Scope

- React Router setup (will be added when creating pages)
- API integration / HTTP client setup (will be added in feature tasks)
- State management (Redux/Context) - will be added when needed
- Authentication UI
- Error boundaries
- Component library setup
- Testing setup (Jest/React Testing Library)
- Production deployment optimization
- SEO optimization
- PWA configuration
- Analytics integration

---

## 📦 Project Structure After Completion

```
frontend/
├── public/
│   ├── index.html
│   └── ...
├── src/
│   ├── pages/
│   │   └── Home.tsx          # ← Created in this task
│   ├── App.tsx               # ← Modified in this task
│   ├── App.css               # ← Can be removed
│   ├── index.tsx
│   ├── index.css             # ← Modified for Tailwind
│   └── ...
├── .env                       # ← Created (not committed)
├── .env.example               # ← Created (committed)
├── Dockerfile                 # ← Created
├── tailwind.config.js         # ← Created
├── postcss.config.js          # ← Created
├── package.json
├── tsconfig.json
└── .gitignore                 # ← Updated
```
