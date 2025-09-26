import useCartStore from "@/store/useCartStore";

export const useCart = () => {
  const { cartItems } = useCartStore();

  const subTotal = cartItems.reduce(
    (acc, { price, quantity }) => acc + price * quantity,
    0
  );

  const totalDiscount = cartItems.reduce(
    (acc, { price, discount = 0, quantity }) =>
      acc + price * discount * quantity,
    0
  );

  const total = subTotal - totalDiscount;

  return { cartItems, total, totalDiscount, subTotal };
};
