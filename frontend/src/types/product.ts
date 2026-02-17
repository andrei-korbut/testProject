export enum ProductType {
  Drink = 'Drink',
  Food = 'Food',
}

export interface Product {
  id: number;
  name: string;
  description: string;
  type: ProductType;
  createdAt: string;
}

export interface CreateProductDto {
  name: string;
  description: string;
  type: ProductType | null;
}

export interface ProductFormData {
  name: string;
  description: string;
  type: ProductType | '';
}

export interface ProductFormErrors {
  name?: string;
  description?: string;
  type?: string;
}

export interface ApiError {
  message: string;
  errors?: Record<string, string[]>;
}
