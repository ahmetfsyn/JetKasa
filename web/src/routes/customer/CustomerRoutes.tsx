import ScannedProductList from "@/pages/customer/ScannedProductList";
import { Route, Routes } from "react-router";

const CustomerRoutes = () => {
  return (
    <Routes>
      <Route
        path="/scanned-product-list"
        element={<ScannedProductList></ScannedProductList>}
      ></Route>
    </Routes>
  );
};

export default CustomerRoutes;
