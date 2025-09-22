import { getProductByBarcode } from "@/services/productService";
import useCartStore from "@/store/useCartStore";
import { showMessage } from "@/utils/showMessage";
import { useQuery } from "@tanstack/react-query";

export const useProductByBarcode = () => {
  const { addProductToCart } = useCartStore();

  const { data, isFetching } = useQuery({
    queryKey: ["product"],
    queryFn: () => Promise.resolve(null),
    enabled: false,
  });

  const handleAddToCart = async (barcode: string) => {
    try {
      const product = await getProductByBarcode(barcode);
      if (product) {
        return addProductToCart(product);
      }
      return showMessage({
        message: "Girilen barkod numarasına ait ürün bulunamadı",
        options: {
          type: "error",
        },
      });
    } catch (error) {
      console.error(error);
    }
  };

  return {
    product: data,
    isFetching,
    handleAddToCart,
  };
};
