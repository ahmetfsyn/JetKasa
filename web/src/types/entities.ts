export type Product = {
  id: string;
  price: number;
  productName: string;
  quantity: number;
  barcode: string;
  discount?: number;
};

export type PaymentId = 1 | 2 | 3;

export type PaymentMethod = {
  id: PaymentId;
  title: string;
  icon: React.ReactNode;
  badges?: [{ text: string; color: string }];
  disabled?: boolean;
};
