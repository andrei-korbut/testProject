import { Product, CreateProductDto, ApiError } from '../types/product';

const API_BASE_URL = process.env.REACT_APP_API_URL || 'http://localhost:5000';

class ApiService {
  private async handleResponse<T>(response: Response): Promise<T> {
    if (!response.ok) {
      const errorData: ApiError = await response.json().catch(() => ({
        message: 'An unexpected error occurred',
      }));
      throw errorData;
    }
    return response.json();
  }

  async createProduct(dto: CreateProductDto): Promise<Product> {
    const response = await fetch(`${API_BASE_URL}/products`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(dto),
    });
    return this.handleResponse<Product>(response);
  }

  async getProducts(): Promise<Product[]> {
    const response = await fetch(`${API_BASE_URL}/products`);
    return this.handleResponse<Product[]>(response);
  }

  async getProductById(id: number): Promise<Product> {
    const response = await fetch(`${API_BASE_URL}/products/${id}`);
    return this.handleResponse<Product>(response);
  }

  async updateProduct(id: number, dto: CreateProductDto): Promise<Product> {
    const response = await fetch(`${API_BASE_URL}/products/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(dto),
    });
    return this.handleResponse<Product>(response);
  }

  async deleteProduct(id: number): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/products/${id}`, {
      method: 'DELETE',
    });
    if (!response.ok) {
      const errorData: ApiError = await response.json().catch(() => ({
        message: 'An unexpected error occurred',
      }));
      throw errorData;
    }
  }
}

export const apiService = new ApiService();
