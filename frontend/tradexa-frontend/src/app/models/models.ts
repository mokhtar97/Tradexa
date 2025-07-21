export interface Category {
  id: string;
  englishName: string;
  arabicName: string;
}

export interface User {
  id: string;
  username: string;
  email: string;
  roles: string[];
}

export interface InvoiceItem {
  productId: string;
  quantity: number;
  unitPrice: number;
}

export interface Invoice {
  id: string;
  number: string;
  customerName: string;
  date: string;
  total: number;
  items: InvoiceItem[];
}

export interface Product {
  id: string;
  name: string;
  description: string;
  price: number;
  categoryId: string;
  imageUrl?: string;
}