import { Route, Routes } from "react-router";
import NotFound from "@/pages/NotFound";
import Welcome from "@/pages/Welcome";
import MainLayout from "@/layouts/MainLayout";
import CustomerRoutes from "./customer/CustomerRoutes";
import PaymentRoutes from "./payment/PaymentRoutes";
import { CartGuard } from "@/layouts/CartGuard";

const AppRouter = () => {
  return (
    <Routes>
      <Route
        path="/customer/*"
        element={
          <MainLayout>
            <CustomerRoutes />
          </MainLayout>
        }
      />

      <Route
        path="/payment/*"
        element={
          <CartGuard>
            <MainLayout>
              <PaymentRoutes />
            </MainLayout>
          </CartGuard>
        }
      />
      <Route
        path="/"
        element={
          <MainLayout>
            <Welcome />
          </MainLayout>
        }
      />

      <Route path="*" element={<NotFound />} />
    </Routes>
  );
};

export default AppRouter;
