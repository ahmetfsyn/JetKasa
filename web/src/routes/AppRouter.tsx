import { Route, Routes } from "react-router";
import AuthRoutes from "./auth/AuthRoutes";
import NotFound from "@/pages/NotFound";
import CheckerRoutes from "./checker/CheckerRoutes";
import PrivateRoute from "./PrivateRoute";
import UserRoutes from "./user/UserRoutes";
import Welcome from "@/pages/Welcome";
import MainLayout from "@/layouts/MainLayout";

const AppRouter = () => {
  return (
    <Routes>
      <Route path="/auth/*" element={<AuthRoutes />} />
      <Route
        path="/checker/*"
        element={
          <PrivateRoute>
            <CheckerRoutes />
          </PrivateRoute>
        }
      />
      <Route
        path="/user/*"
        element={
          <MainLayout>
            <UserRoutes />
          </MainLayout>
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
