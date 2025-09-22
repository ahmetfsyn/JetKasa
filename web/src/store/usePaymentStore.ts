import { create } from "zustand";

export type PaymentMethod = -1 | 1 | 2 | 3;

export type PaymentStore = {
  selectedPaymentMethod: PaymentMethod;
  setSelectedPaymentMethod: (paymentMethod: PaymentMethod) => void;
};

const usePaymentStore = create<PaymentStore>((set) => ({
  selectedPaymentMethod: -1,
  setSelectedPaymentMethod: (paymentMethod) =>
    set({ selectedPaymentMethod: paymentMethod }),
}));

export default usePaymentStore;
