import type { Product } from "@/types/entities";
import { create } from "zustand";

export type CartStore = {
  cartItems: Product[];
  addProductToCart: (product: Product) => void;
};

const useCartStore = create<CartStore>((set) => ({
  cartItems: [],
  addProductToCart: (product) =>
    set((state) => {
      const isProductExists = state.cartItems.find(
        (p) => p.barcode === product.barcode
      );

      if (isProductExists) {
        return {
          cartItems: state.cartItems.map((p) =>
            p.id === product.id
              ? {
                  ...p,
                  quantity: p.quantity + 1,
                }
              : p
          ),
        };
      }
      return {
        cartItems: [...state.cartItems, { ...product, quantity: 1 }],
      };
    }),
}));

export default useCartStore;
