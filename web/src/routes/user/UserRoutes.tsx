import ScannedProductList from "@/pages/user/ScannedProductList";
import { Route, Routes } from "react-router";

const UserRoutes = () => {
  return (
    <Routes>
      <Route
        path="/scanned-product-list"
        element={<ScannedProductList></ScannedProductList>}
      ></Route>
    </Routes>
  );
};

export default UserRoutes;
