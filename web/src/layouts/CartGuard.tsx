import { Navigate } from "react-router";
import useCartStore from "@/store/useCartStore";
import type { JSX } from "react";

interface CartGuardProps {
  children: JSX.Element;
}

export const CartGuard = ({ children }: CartGuardProps) => {
  const { cartItems } = useCartStore();

  if (cartItems.length === 0) {
    return <Navigate to="/" replace />;
  }

  return children;
};
