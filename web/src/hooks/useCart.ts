import { products } from "@/mock/mockData";
import type { Product } from "@/types/entities";
import { showMessage } from "@/utils/showMessage";
import { useCallback, useState } from "react";

export const useCart = (initial: Product[] = []) => {
  const [cartItems, setCartItems] = useState<Product[]>(initial);

  const findProductByBarcode = useCallback((barcode: string) => {
    return products.find((prod) => prod.barcode === barcode);
  }, []);

  const addProductByBarcode = useCallback(
    (barcode: string) => {
      const product = findProductByBarcode(barcode);
      if (!product)
        return showMessage({
          message: "Girilen barkod numarasına ait ürün bulunamadı",
          options: {
            type: "error",
          },
        });

      setCartItems((prev) => {
        const existing = prev.find((p) => p.id === product.id);

        if (existing) {
          return prev.map((p) =>
            p.id === product.id ? { ...p, quantity: p.quantity + 1 } : p
          );
        }

        return [...prev, { ...product, quantity: 1 }];
      });
    },
    [findProductByBarcode]
  );

  const subTotal = cartItems.reduce(
    (acc, { price, quantity }) => acc + price * quantity,
    0
  );

  //  ? indirimi hesaplamak istemiyoruz bu yüzden aşağıdaki kod satırını yorumladım

  const totalDiscount = cartItems.reduce(
    (acc, { price, discount = 0, quantity }) =>
      acc + price * discount * quantity,
    0
  );

  // ? indirim tutarını hesaplamak istemiyoruz bu yüzden aşağıdaki totalı hesaplarken direkt subTotal a eşitledim

  const total = subTotal - totalDiscount;

  return { cartItems, addProductByBarcode, total, totalDiscount, subTotal };
};
