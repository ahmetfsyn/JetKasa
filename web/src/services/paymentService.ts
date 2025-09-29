import { api } from "@/configs/axiosConfig";

export type CreatePaymentParams = {
  cartId: string;
  email: string;
  username: string;
};

export const createPaymentAsync = async ({
  cartId,
  email,
  username,
}: CreatePaymentParams) => {
  try {
    const res = await api.post(
      `/payment/create/${cartId}?UserName=${username}&UserEmail=${email}`
    );

    console.log(res.data);

    return res.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};
