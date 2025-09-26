import { products } from "@/mock/mockData";

export const getProductByBarcode = async (barcode: string) => {
  try {
    if (barcode) {
      return products.find((product) => product.barcode === barcode);
    }
  } catch (error) {
    console.error(error);
    throw error;
  }
};
