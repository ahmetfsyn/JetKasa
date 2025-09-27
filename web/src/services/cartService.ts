import { api } from "@/configs/axiosConfig";

export const createCartAsync = async () => {
  try {
    const res = await api.post("/cart/create");

    console.log(res.data);

    return res.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};
