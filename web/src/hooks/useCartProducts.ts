import { useQuery } from "@tanstack/react-query";
import { getCartProductsAsync } from "@/services/cartService";

export const useCartProducts = (cartId: string) => {
  const { data, error, isLoading, refetch } = useQuery({
    queryKey: ["cartProducts", cartId],
    queryFn: () => getCartProductsAsync(cartId),
    enabled: false,
  });

  return { data, error, isLoading, refetch };
};
