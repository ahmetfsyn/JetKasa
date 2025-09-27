import useCartStore from "@/store/useCartStore";
import { showMessage } from "@/utils/showMessage";
import { useMutation } from "@tanstack/react-query";
import { useSound } from "./useSound";
import { addProductToCartAsync } from "@/services/cartService";
import { useCartProducts } from "./useCartProducts";

export const useAddProductToCart = () => {
  const { cartId, setCartItems } = useCartStore();
  const playSound = useSound();
  const { refetch: getCartProducts } = useCartProducts(cartId);

  const mutation = useMutation({
    mutationFn: addProductToCartAsync,
  });

  const handleAddToCart = async (barcode: string) => {
    try {
      await mutation.mutateAsync({
        barcode,
        cartId,
        quantity: 1,
      });

      const { data: products } = await getCartProducts();
      setCartItems(products);

      playSound();
    } catch (error) {
      showMessage({
        message: "Girilen barkod numarasına ait ürün bulunamadı",
        options: {
          type: "error",
        },
      });
      console.error(error);
    }
  };

  return {
    handleAddToCart,
    ...mutation,
  };
};
