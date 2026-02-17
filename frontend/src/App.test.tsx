import React from 'react';
import { render, screen } from '@testing-library/react';
import App from './App';

test('renders Product Management App', () => {
  render(<App />);
  const headingElement = screen.getByText(/product management app/i);
  expect(headingElement).toBeInTheDocument();
});

test('renders Create Product button', () => {
  render(<App />);
  const buttonElement = screen.getByText(/create product/i);
  expect(buttonElement).toBeInTheDocument();
});
