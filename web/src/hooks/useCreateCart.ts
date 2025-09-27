import { createCartAsync } from "@/services/cartService";
import useCartStore from "@/store/useCartStore";
import { useMutation } from "@tanstack/react-query";

export const useCreateCart = () => {
  const { createCart: addCartToStore } = useCartStore();

  const mutation = useMutation({
    mutationFn: createCartAsync,
  });

  const handleCreateCart = async () => {
    try {
      const { data } = await mutation.mutateAsync();
      addCartToStore(data);
      return data;
    } catch (error) {
      console.error("Cart oluşturulamadı:", error);
      throw error;
    }
  };

  return {
    createCart: handleCreateCart,
    ...mutation,
  };
};
