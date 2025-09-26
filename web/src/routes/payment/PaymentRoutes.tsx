import { Route, Routes } from "react-router";
import PaymentMethods from "@/pages/customer/PaymentMethods";
import PayPass from "@/pages/customer/PayPass";
import PaymentResult from "@/pages/customer/PaymentResult";

const PaymentRoutes = () => {
  return (
    <Routes>
      <Route index element={<PaymentMethods />} />

      <Route path="payment-result" element={<PaymentResult />} />

      <Route path="paypass" element={<PayPass />} />

      {/* ! Alttaki 2 satır nakit ve mobil ödeme aktif oldugunda eklenmeli */}
      {/* <Route path="cash" element={<Cash />} />

      <Route path="mobile" element={<Mobile />} /> */}
    </Routes>
  );
};

export default PaymentRoutes;
