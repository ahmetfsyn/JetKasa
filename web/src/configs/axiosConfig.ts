import axios from "axios";

export const api = axios.create({
  baseURL: "https://jetkasa-backend-production.up.railway.app",
  headers: {
    "Content-Type": "application/json",
  },
});
