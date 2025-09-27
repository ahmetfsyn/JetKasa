import axios from "axios";

export const api = axios.create({
  baseURL: "https://resilient-tranquility-production.up.railway.app",
  headers: {
    "Content-Type": "application/json",
  },
});
