import { BrowserRouter } from "react-router";
import AppRouter from "./routes/AppRouter";
import { ToastContainer } from "react-toastify";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

const queryClient = new QueryClient();

function App() {
  return (
    <>
      <QueryClientProvider client={queryClient}>
        <BrowserRouter>
          <AppRouter />
        </BrowserRouter>
        <ToastContainer></ToastContainer>
      </QueryClientProvider>
    </>
  );
}

export default App;
