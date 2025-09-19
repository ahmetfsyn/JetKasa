import { createRoot } from "react-dom/client";

import { BrowserRouter } from "react-router";
import AppRouter from "./routes/AppRouter.tsx";

createRoot(document.getElementById("root")!).render(
  <BrowserRouter>
    <AppRouter />
  </BrowserRouter>
);
