import { api } from "@/configs/axiosConfig";

export const createCartAsync = async () => {
  try {
    const res = await api.post("/cart/create", {});

    // console.log(res.data);

    return res.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

export type AddProductToCartParam = {
  cartId: string;
  quantity: number;
  barcode: string;
};

export const addProductToCartAsync = async (
  cartItem: AddProductToCartParam
) => {
  try {
    const res = await api.post("/cart/addItem", cartItem);

    // console.log(res.data);
    return res.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

export const getCartProductsAsync = async (cartId: string) => {
  try {
    const res = await api.get(`/cart/${cartId}/items`);

    // console.log(res.data.data);

    return res.data.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};
