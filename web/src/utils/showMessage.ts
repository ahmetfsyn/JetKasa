import { toast, type ToastOptions } from "react-toastify";

export type ShowMessageParams = {
  message: string;
  options?: ToastOptions;
};

export const showMessage = ({ message, options }: ShowMessageParams) =>
  toast(message, {
    ...options,
    theme: "dark",
    autoClose: 3000,
    pauseOnHover: false,
    draggable: false,
  });
