import Login from "@/pages/auth/Login";
import { Route, Routes } from "react-router";

const AuthRoutes = () => {
  return (
    <Routes>
      <Route path="login" element={<Login />}></Route>
    </Routes>
  );
};

export default AuthRoutes;
