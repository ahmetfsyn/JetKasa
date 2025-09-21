import { Route, Routes } from "react-router";
import PaymentMethods from "@/pages/customer/PaymentMethods";
import PayPass from "@/pages/customer/Paypass";

const PaymentRoutes = () => {
  return (
    <Routes>
      <Route index element={<PaymentMethods />} />

      <Route path="paypass" element={<PayPass />} />
    </Routes>
  );
};

export default PaymentRoutes;
