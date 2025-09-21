import { BrowserRouter } from "react-router";
import AppRouter from "./routes/AppRouter";
import { ToastContainer } from "react-toastify";
function App() {
  return (
    <>
      <BrowserRouter>
        <AppRouter />
      </BrowserRouter>
      <ToastContainer></ToastContainer>
    </>
  );
}

export default App;
