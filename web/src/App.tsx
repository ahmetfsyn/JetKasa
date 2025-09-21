import { BrowserRouter } from "react-router";
import AppRouter from "./routes/AppRouter";
import { ToastContainer } from "react-toastify";

const App = () => {
  return (
    <>
      <BrowserRouter>
        <AppRouter />
      </BrowserRouter>
      <ToastContainer />
    </>
  );
};

export default App;
