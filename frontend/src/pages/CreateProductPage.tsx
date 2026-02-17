import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import ProductForm from '../components/ProductForm';
import { ProductFormData, ProductType, ApiError } from '../types/product';
import { apiService } from '../services/api-service';

const CreateProductPage: React.FC = () => {
  const navigate = useNavigate();
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string>('');

  const handleSubmit = async (formData: ProductFormData) => {
    setIsSubmitting(true);
    setErrorMessage('');

    try {
      const dto = {
        name: formData.name.trim(),
        description: formData.description.trim(),
        type: formData.type === '' ? null : (formData.type as ProductType),
      };

      await apiService.createProduct(dto);
      
      // Navigate to main page on success
      navigate('/');
    } catch (error) {
      const apiError = error as ApiError;
      
      // Extract error message
      let message = apiError.message || 'An unexpected error occurred';
      
      // If there are field-specific errors, show the first one
      if (apiError.errors) {
        const firstError = Object.values(apiError.errors)[0];
        if (firstError && firstError.length > 0) {
          message = firstError[0];
        }
      }
      
      setErrorMessage(message);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleCancel = () => {
    navigate('/');
  };

  const dismissError = () => {
    setErrorMessage('');
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100 py-12 px-4 sm:px-6 lg:px-8">
      <div className="max-w-2xl mx-auto">
        {/* Header */}
        <div className="mb-8">
          <h1 className="text-3xl font-bold text-gray-900">Create Product</h1>
          <p className="mt-2 text-gray-600">
            Fill in the form below to create a new product
          </p>
        </div>

        {/* Error Notification */}
        {errorMessage && (
          <div className="mb-6 bg-red-50 border border-red-200 rounded-lg p-4 flex items-start justify-between">
            <div className="flex items-start">
              <svg
                className="h-5 w-5 text-red-400 mt-0.5 mr-3"
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 20 20"
                fill="currentColor"
              >
                <path
                  fillRule="evenodd"
                  d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.707 7.293a1 1 0 00-1.414 1.414L8.586 10l-1.293 1.293a1 1 0 101.414 1.414L10 11.414l1.293 1.293a1 1 0 001.414-1.414L11.414 10l1.293-1.293a1 1 0 00-1.414-1.414L10 8.586 8.707 7.293z"
                  clipRule="evenodd"
                />
              </svg>
              <div>
                <h3 className="text-sm font-medium text-red-800">Error</h3>
                <p className="mt-1 text-sm text-red-700">{errorMessage}</p>
              </div>
            </div>
            <button
              onClick={dismissError}
              className="text-red-400 hover:text-red-600 focus:outline-none"
            >
              <svg
                className="h-5 w-5"
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 20 20"
                fill="currentColor"
              >
                <path
                  fillRule="evenodd"
                  d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
                  clipRule="evenodd"
                />
              </svg>
            </button>
          </div>
        )}

        {/* Form Container */}
        <div className="bg-white rounded-lg shadow-md p-8">
          <ProductForm
            onSubmit={handleSubmit}
            onCancel={handleCancel}
            isSubmitting={isSubmitting}
            submitButtonText="Create Product"
          />
        </div>
      </div>
    </div>
  );
};

export default CreateProductPage;
