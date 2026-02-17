import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import CreateProductPage from './pages/CreateProductPage';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/products/create" element={<CreateProductPage />} />
      </Routes>
    </Router>
  );
}

export default App;
